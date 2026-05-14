using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;
using ColorPop.Core.Rules;
using ColorPop.Core.Services;

namespace ColorPop.Tests.Engine;

public class GameEngineTests
{
    private readonly IMoveValidator _moveValidator;
    private readonly IClusterFinder _clusterFinder;
    private readonly IJokerResolver _jokerResolver;
    private readonly IGravityEngine _gravityEngine;
    private readonly IWinConditionEvaluator _winConditionEvaluator;
    private readonly GameEngine _sut;

    public GameEngineTests()
    {
        // Replace with mocks/fakes when implementing tests
        _moveValidator = null!;
        _clusterFinder = null!;
        _jokerResolver = null!;
        _gravityEngine = null!;
        _winConditionEvaluator = null!;

        _sut = new GameEngine(
            _moveValidator,
            _clusterFinder,
            _jokerResolver,
            _gravityEngine,
            _winConditionEvaluator);
    }

    // ApplyMove

    [Fact]
    public void ApplyMove_InvalidMove_ReturnsUnchangedState()
    {
    }

    [Fact]
    public void ApplyMove_ValidMove_RemovesClusterFromBoard()
    {
    }

    [Fact]
    public void ApplyMove_ValidMove_AppliesGravityAfterRemoval()
    {
    }

    [Fact]
    public void ApplyMove_ValidMove_AdvancesToNextPlayerTurn()
    {
    }

    [Fact]
    public void ApplyMove_ValidMove_IncreasesTurnNumber()
    {
    }

    [Fact]
    public void ApplyMove_LastMoveEndsGame_StatusChangesToFinished()
    {
    }

    [Fact]
    public void ApplyMove_JokersEnabled_ExpandsClusterThroughJokers()
    {
    }

    [Fact]
    public void ApplyMove_WithSelectedJokerColor_DoesNotCountJokersAsCapturedColor()
    {
        // Arrange
        var sut = new GameEngine(
            new MoveValidator(),
            new ClusterFinder(),
            new JokerResolver(),
            new GravityEngine(),
            new WinConditionEvaluator());

        var board = new Board(new[,]
        {
            { new Token(TokenColor.Yellow), new Token(TokenColor.Joker) },
            { new Token(TokenColor.Blue), new Token(TokenColor.Blue) }
        });

        var players = new List<Player>
        {
            new(1, "Player 1", new HashSet<TokenColor>()),
            new(2, "Player 2", new HashSet<TokenColor>())
        };

        var state = new GameState(
            board,
            players,
            currentPlayerIndex: 0,
            status: GameStatus.InProgress,
            selectedJokerColor: TokenColor.Yellow);

        // Act
        var updated = sut.ApplyMove(state, new Move(1, new Position(0, 0)));

        // Assert
        updated.Players[0].CapturedColorCounts[TokenColor.Yellow].Should().Be(1);
        updated.Players[0].CapturedColorCounts.Should().NotContainKey(TokenColor.Joker);
        updated.Players[0].TotalCapturedCount.Should().Be(1);
    }
}
