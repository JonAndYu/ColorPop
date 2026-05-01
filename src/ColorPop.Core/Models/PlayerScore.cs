namespace ColorPop.Core.Models;

/// <summary>
/// Represents a player's final score used for ranking.
/// </summary>
public sealed class PlayerScore
{
    public Player Player { get; }
    public int OwnColorPenalty { get; }

    public PlayerScore(Player player, int ownColorPenalty)
    {
        Player = player;
        OwnColorPenalty = ownColorPenalty;
    }
}
