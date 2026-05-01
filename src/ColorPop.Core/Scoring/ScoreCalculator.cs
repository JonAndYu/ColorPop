using ColorPop.Core.Models;

namespace ColorPop.Core.Services;

/// <summary>
/// Responsible for calculating final scores and rankings at the end of a game.
/// </summary>
/// <remarks>
/// Scoring rule (based on your design):
/// - Fewer captured OWN-color tokens is better
/// - Tie-breakers can use total captured tokens or other rules
///
/// This class is stateless and deterministic.
/// </remarks>
public sealed class ScoreCalculator
{
    /// <summary>
    /// Calculates final ranked scores for all players.
    /// Lower score is better.
    /// </summary>
    public IReadOnlyList<PlayerScore> CalculateScores(GameState state)
    {
        var scores = new List<PlayerScore>();

        foreach (var player in state.Players)
        {
            // Primary scoring rule: own-color penalty
            var ownColorPenalty = player.CapturedOwnColorCount;

            scores.Add(new PlayerScore(player, ownColorPenalty));
        }

        return scores
            .OrderBy(s => s.OwnColorPenalty)              // lower is better
            .ThenBy(s => s.Player.TotalCapturedCount)     // tie-breaker (optional)
            .ToList();
    }

    /// <summary>
    /// Returns the winning player, or null if tied.
    /// </summary>
    public Player? GetWinner(GameState state)
    {
        var ranked = CalculateScores(state);

        if (ranked.Count == 0)
            return null;

        var best = ranked[0];
        var second = ranked.Count > 1 ? ranked[1] : null;

        // If tie on best score → draw
        if (second != null &&
            best.OwnColorPenalty == second.OwnColorPenalty)
        {
            return null;
        }

        return best.Player;
    }

    /// <summary>
    /// Returns full ranking of players (best to worst).
    /// </summary>
    public IReadOnlyList<Player> GetRanking(GameState state)
    {
        return CalculateScores(state)
            .Select(s => s.Player)
            .ToList();
    }
}
