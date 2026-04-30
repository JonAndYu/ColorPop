using ColorPop.Core.Models;
using ColorPop.Core.Services;

namespace ColorPop.Tests.Scoring;

public class ScoreCalculatorTests
{
    private readonly ScoreCalculator _sut = new();

    // CalculateScores

    [Fact]
    public void CalculateScores_OrdersByOwnColorPenaltyAscending()
    {
    }

    [Fact]
    public void CalculateScores_TiedPenalty_OrdersByTotalCapturedCountAscending()
    {
    }

    [Fact]
    public void CalculateScores_NoPlayers_ReturnsEmptyList()
    {
    }

    [Fact]
    public void CalculateScores_ReturnsEntryForEveryPlayer()
    {
    }

    // GetWinner

    [Fact]
    public void GetWinner_ClearWinner_ReturnsPlayerWithLowestPenalty()
    {
    }

    [Fact]
    public void GetWinner_TiedBestScore_ReturnsNull()
    {
    }

    [Fact]
    public void GetWinner_NoPlayers_ReturnsNull()
    {
    }

    // GetRanking

    [Fact]
    public void GetRanking_ReturnsPlayersInWinnerFirstOrder()
    {
    }

    [Fact]
    public void GetRanking_IncludesAllPlayers()
    {
    }
}
