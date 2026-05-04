using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using ColorPop.Core.Rules;
using FluentAssertions;

namespace ColorPop.Tests.Rules;

public class WinConditionEvaluatorTests
{
    private readonly WinConditionEvaluator _sut = new();

    private static Token T(TokenColor color) => new(color);

    private static Board CreateBoard(Token[,] grid) => new Board(grid);

    private static GameState CreateState(Board board, List<Player> players)
        => new GameState(board, players, 0, GameStatus.InProgress, 0);

    // ----------------------------
    // Ranking
    // ----------------------------

    [Fact]
    public void Evaluate_ShouldRankPlayersByScore()
    {
        // Arrange
        var board = CreateBoard(new Token[2, 2]);

        var low = new Player(1, "A", new HashSet<TokenColor>(), totalCapturedCount: 2);
        var high = new Player(2, "B", new HashSet<TokenColor>(), totalCapturedCount: 10);

        var state = CreateState(board, new List<Player> { low, high });

        // Act
        var result = _sut.Evaluate(state);

        // Assert
        result.FinalRanking.First().Should().Be(high);
        result.FinalRanking.Last().Should().Be(low);
    }

    // ----------------------------
    // Winner selection
    // ----------------------------

    [Fact]
    public void Evaluate_ShouldReturnWinner_WhenNotDraw()
    {
        var board = CreateBoard(new Token[2, 2]);

        var winner = new Player(1, "A", new HashSet<TokenColor>(), totalCapturedCount: 10);
        var loser = new Player(2, "B", new HashSet<TokenColor>(), totalCapturedCount: 2);

        var state = CreateState(board, new List<Player> { winner, loser });

        var result = _sut.Evaluate(state);

        result.Winner.Should().Be(winner);
        result.IsDraw.Should().BeFalse();
    }

    // ----------------------------
    // Draw detection
    // ----------------------------

    [Fact]
    public void Evaluate_ShouldDetectDraw_WhenTopScoresEqual()
    {
        var board = CreateBoard(new Token[2, 2]);

        var p1 = new Player(1, "A", new HashSet<TokenColor>(), totalCapturedCount: 5);
        var p2 = new Player(2, "B", new HashSet<TokenColor>(), totalCapturedCount: 5);

        var state = CreateState(board, new List<Player> { p1, p2 });

        var result = _sut.Evaluate(state);

        result.IsDraw.Should().BeTrue();
        result.Winner.Should().BeNull();
    }

    // ----------------------------
    // End reason
    // ----------------------------

    [Fact]
    public void Evaluate_ShouldReturnReason_WhenGameStillRunning()
    {
        var grid = new Token[2, 2];
        grid[0, 0] = T(TokenColor.Blue);

        var board = CreateBoard(grid);

        var players = new List<Player>
    {
        new Player(1, "A", new HashSet<TokenColor>()),
        new Player(2, "B", new HashSet<TokenColor>())
    };

        var state = CreateState(board, players);

        var result = _sut.Evaluate(state);

        result.EndReason.Should().Be("Game still in progress");
        result.Winner.Should().BeNull();
    }

    // ----------------------------
    // IsGameOver
    // ----------------------------

    [Fact]
    public void IsGameOver_ShouldReturnTrue_WhenOnlyOnePlayerActive()
    {
        var board = CreateBoard(new Token[2, 2]);

        var p1 = new Player(1, "A", new HashSet<TokenColor>(), isActive: true);
        var p2 = new Player(2, "B", new HashSet<TokenColor>(), isActive: false);

        var state = CreateState(board, new List<Player> { p1, p2 });

        _sut.IsGameOver(state).Should().BeTrue();
    }
}
