using ColorPop.Application;
using ColorPop.Core.Abstractions;
using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;

namespace ColorPop.Tests.Application;

public class GameSessionTests
{
    [Fact]
    public void PlayMove_WhenStateChanges_EnablesUndo()
    {
        // Arrange
        var initialBoard = CreateBoard(TokenColor.Blue);
        var nextBoard = initialBoard.UpdateCell(new Position(0, 0), Token.Empty);
        var session = CreateSession(initialBoard, (_, _) => CreateState(nextBoard, turnNumber: 1));
        var previousState = session.State;

        // Act
        session.PlayMove(CreateMove(session));

        // Assert
        session.CanUndo.Should().BeTrue();
        session.State.Should().NotBeSameAs(previousState);
    }

    [Fact]
    public void PlayMove_WhenPracticeMode_KeepsFirstPlayerTurn()
    {
        // Arrange
        var initialBoard = CreateBoard(TokenColor.Blue);
        var session = CreateSession(initialBoard, (state, _) => state with
        {
            Board = state.Board.UpdateCell(new Position(0, 0), Token.Empty),
            CurrentPlayerIndex = 1,
            TurnNumber = state.TurnNumber + 1
        });

        // Act
        session.PlayMove(CreateMove(session));

        // Assert
        session.State.CurrentPlayerIndex.Should().Be(0);
        session.State.CurrentPlayer.Name.Should().Be("Player 1");
    }

    [Fact]
    public void PlayMove_WhenNotPracticeMode_AllowsEngineToAdvanceTurn()
    {
        // Arrange
        var initialBoard = CreateBoard(TokenColor.Blue);
        var session = CreateSession(
            initialBoard,
            (state, _) => state with
            {
                Board = state.Board.UpdateCell(new Position(0, 0), Token.Empty),
                CurrentPlayerIndex = 1,
                TurnNumber = state.TurnNumber + 1
            },
            isPracticeMode: false);

        // Act
        session.PlayMove(CreateMove(session));

        // Assert
        session.State.CurrentPlayerIndex.Should().Be(1);
    }

    [Fact]
    public void UndoLastMove_WhenHistoryExists_RestoresPreviousState()
    {
        // Arrange
        var initialBoard = CreateBoard(TokenColor.Green);
        var nextBoard = initialBoard.UpdateCell(new Position(0, 0), Token.Empty);
        var session = CreateSession(initialBoard, (_, _) => CreateState(nextBoard, turnNumber: 1));
        var previousState = session.State;

        session.PlayMove(CreateMove(session));

        // Act
        session.UndoLastMove();

        // Assert
        session.State.Should().BeSameAs(previousState);
        session.CanUndo.Should().BeFalse();
    }

    [Fact]
    public void PlayMove_WhenStateDoesNotChange_DoesNotEnableUndo()
    {
        // Arrange
        var initialBoard = CreateBoard(TokenColor.Pink);
        var session = CreateSession(initialBoard, (state, _) => state);

        // Act
        session.PlayMove(CreateMove(session));

        // Assert
        session.CanUndo.Should().BeFalse();
    }

    [Fact]
    public void UndoLastMove_WhenNotPracticeMode_DoesNotRestorePreviousState()
    {
        // Arrange
        var initialBoard = CreateBoard(TokenColor.Orange);
        var nextBoard = initialBoard.UpdateCell(new Position(0, 0), Token.Empty);
        var session = CreateSession(
            initialBoard,
            (_, _) => CreateState(nextBoard, turnNumber: 1),
            isPracticeMode: false);

        var previousState = session.State;

        session.PlayMove(CreateMove(session));

        // Act
        session.UndoLastMove();

        // Assert
        session.IsPracticeMode.Should().BeFalse();
        session.CanUndo.Should().BeFalse();
        session.State.Should().NotBeSameAs(previousState);
    }

    private static GameSession CreateSession(
        Board initialBoard,
        Func<GameState, Move, GameState> applyMove,
        bool isPracticeMode = true)
    {
        var settings = new GameSettings(playerCount: 2, boardSize: 2, seed: 1);

        return new GameSession(
            new StubGameEngine(applyMove),
            new StubBoardShuffler(initialBoard),
            settings,
            isPracticeMode);
    }

    private static GameState CreateState(Board board, int turnNumber = 0)
        => new(
            board,
            new List<Player>
            {
                new(1, "Player 1", new HashSet<TokenColor>()),
                new(2, "Player 2", new HashSet<TokenColor>())
            },
            currentPlayerIndex: 0,
            status: GameStatus.InProgress,
            turnNumber: turnNumber);

    private static Move CreateMove(GameSession session)
        => new(session.State.CurrentPlayer.Id, new Position(0, 0));

    private static Board CreateBoard(TokenColor color)
        => new(new[,]
        {
            { new Token(color), new Token(color) },
            { new Token(color), new Token(color) }
        });

    private sealed class StubGameEngine(Func<GameState, Move, GameState> applyMove) : IGameEngine
    {
        public GameState ApplyMove(GameState state, Move move)
            => applyMove(state, move);
    }

    private sealed class StubBoardShuffler(Board board) : IBoardShuffler
    {
        public Board Shuffle(Board boardToShuffle, int seed)
            => boardToShuffle;

        public Board GenerateInitialBoard(int seed, GameSettings settings)
            => board;
    }
}
