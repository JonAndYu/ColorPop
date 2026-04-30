using ColorPop.Core.Models;
using ColorPop.Core.Rules;

namespace ColorPop.Tests.Rules;

public class WinConditionEvaluatorTests
{
    private readonly WinConditionEvaluator _sut = new();

    // IsGameOver

    [Fact]
    public void IsGameOver_NoValidMovesRemain_ReturnsTrue()
    {
    }

    [Fact]
    public void IsGameOver_ValidMovesExist_ReturnsFalse()
    {
    }

    [Fact]
    public void IsGameOver_EmptyBoard_ReturnsTrue()
    {
    }

    // GetWinner

    [Fact]
    public void GetWinner_OnePlayerWithLowestPenalty_ReturnsThatPlayer()
    {
    }

    [Fact]
    public void GetWinner_AllPlayersTied_ReturnsNull()
    {
    }

    [Fact]
    public void GetWinner_NoPlayers_ReturnsNull()
    {
    }

    // Evaluate

    [Fact]
    public void Evaluate_GameInProgress_ReturnsInProgressResult()
    {
    }

    [Fact]
    public void Evaluate_GameOver_ReturnsFinishedResult()
    {
    }

    [Fact]
    public void Evaluate_GameOver_ResultContainsWinner()
    {
    }
}
