using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;
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
}
