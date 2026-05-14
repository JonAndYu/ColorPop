using ColorPop.Core.Enums;

namespace ColorPop.Core.Models;

/// <summary>
/// Represents a player in the game.
/// Immutable value object stored inside GameState snapshots.
/// </summary>
/// <remarks>
/// A player may control one or more secret colors depending on game mode.
/// - 5 players: each player has 1 color
/// - 2 players: each player has 2 colors
///
/// The player NEVER knows other players' colors.
/// </remarks>
public sealed class Player
{
    /// <summary>
    /// Unique identifier for the player.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Display name used for UI and multiplayer lobbies.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Set of secret colors assigned to this player.
    /// Used for scoring logic.
    /// </summary>
    public IReadOnlySet<TokenColor> SecretColors { get; }

    /// <summary>
    /// Number of tokens matching any of the player's secret colors
    /// that have been captured.
    /// </summary>
    public int CapturedOwnColorCount { get; }

    /// <summary>
    /// Total number of tokens captured regardless of color.
    /// </summary>
    public int TotalCapturedCount { get; }

    /// <summary>
    /// Number of captured tokens grouped by playable token color.
    /// Empty and joker tokens are not counted here.
    /// </summary>
    public IReadOnlyDictionary<TokenColor, int> CapturedColorCounts { get; }

    /// <summary>
    /// Whether the player is still active in the game.
    /// </summary>
    public bool IsActive { get; }

    public Player(
        int id,
        string name,
        IReadOnlySet<TokenColor> secretColors,
        int capturedOwnColorCount = 0,
        int totalCapturedCount = 0,
        bool isActive = true,
        IReadOnlyDictionary<TokenColor, int>? capturedColorCounts = null)
    {
        Id = id;
        Name = name;
        SecretColors = secretColors;
        CapturedOwnColorCount = capturedOwnColorCount;
        TotalCapturedCount = totalCapturedCount;
        IsActive = isActive;
        CapturedColorCounts = capturedColorCounts is null
            ? new Dictionary<TokenColor, int>()
            : new Dictionary<TokenColor, int>(capturedColorCounts);
    }

    /// <summary>
    /// Adds a captured token and updates scoring if it matches any secret color.
    /// </summary>
    public Player AddCaptured(TokenColor tokenColor)
    {
        if (tokenColor is TokenColor.Empty or TokenColor.Joker)
            return this;

        var newOwnCount =
            SecretColors.Contains(tokenColor)
                ? CapturedOwnColorCount + 1
                : CapturedOwnColorCount;

        var newColorCounts = new Dictionary<TokenColor, int>(CapturedColorCounts);
        newColorCounts[tokenColor] = newColorCounts.GetValueOrDefault(tokenColor) + 1;

        return new Player(
            Id,
            Name,
            SecretColors,
            newOwnCount,
            TotalCapturedCount + 1,
            IsActive,
            newColorCounts);
    }

    /// <summary>
    /// Marks the player as eliminated.
    /// </summary>
    public Player Eliminate()
        => new Player(
            Id,
            Name,
            SecretColors,
            CapturedOwnColorCount,
            TotalCapturedCount,
            false,
            CapturedColorCounts);
}
