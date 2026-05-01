namespace ColorPop.Core.Models;

/// <summary>
/// Represents the final outcome of a completed game.
/// </summary>
/// <remarks>
/// GameResult is only created when the game is finished.
/// It is immutable and does not change after creation.
/// </remarks>
public sealed class GameResult
{
    /// <summary>
    /// Indicates whether the game ended in a draw.
    /// </summary>
    public bool IsDraw { get; }

    /// <summary>
    /// The winning player, if there is one.
    /// Null if the game ended in a draw.
    /// </summary>
    public Player? Winner { get; }

    /// <summary>
    /// Final ranking of all players, ordered by score.
    /// </summary>
    public IReadOnlyList<Player> FinalRanking { get; }

    /// <summary>
    /// Optional explanation of why the game ended.
    /// Useful for UI and debugging.
    /// </summary>
    public string EndReason { get; }

    public GameResult(
        bool isDraw,
        Player? winner,
        IReadOnlyList<Player> finalRanking,
        string endReason)
    {
        IsDraw = isDraw;
        Winner = winner;
        FinalRanking = finalRanking;
        EndReason = endReason;
    }
}
