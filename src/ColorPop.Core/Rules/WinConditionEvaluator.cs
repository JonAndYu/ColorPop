using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;

namespace ColorPop.Core.Rules;

public class WinConditionEvaluator : IWinConditionEvaluator
{
    public GameResult Evaluate(GameState state)
    {
        if (!IsGameOver(state))
        {
            return new GameResult(
                isDraw: false,
                winner: null,
                finalRanking: GetRanking(state),
                endReason: "Game still in progress");
        }

        var ranking = GetRanking(state);
        var winner = ranking.FirstOrDefault();

        var isDraw = ranking.Count > 1 &&
                     ranking[0].TotalCapturedCount == ranking[1].TotalCapturedCount;

        return new GameResult(
            isDraw: isDraw,
            winner: isDraw ? null : winner,
            finalRanking: ranking,
            endReason: GetEndReason(state, isDraw));
    }

    public Player? GetWinner(GameState state)
    {
        var ranking = GetRanking(state);

        if (ranking.Count == 0)
            return null;

        return ranking.First();
    }

    public bool IsGameOver(GameState state)
    {
        var activePlayers = state.Players.Count(p => p.IsActive);

        if (activePlayers <= 1)
            return true;

        foreach (var pos in state.Board.GetAllPositions())
        {
            if (!state.Board.GetToken(pos).IsEmpty)
                return false;
        }

        return true;
    }

    // ----------------------------
    // Helpers
    // ----------------------------

    private static IReadOnlyList<Player> GetRanking(GameState state)
    {
        return state.Players
            .OrderByDescending(p => p.TotalCapturedCount)
            .ToList();
    }

    private static string GetEndReason(GameState state, bool isDraw)
    {
        if (state.Players.Count(p => p.IsActive) <= 1)
            return "Only one player remaining";

        if (isDraw)
            return "Players tied on score";

        return "Board cleared";
    }
}
