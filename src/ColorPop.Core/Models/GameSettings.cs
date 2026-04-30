namespace ColorPop.Core.Models;

/// <summary>
/// Defines configuration rules for a single game instance.
/// This is immutable and shared across the entire match.
/// </summary>
public sealed class GameSettings
{
    /// <summary>
    /// Number of players in the game (2–5).
    /// </summary>
    public int PlayerCount { get; }

    /// <summary>
    /// Size of the board (e.g., 10 = 10x10 grid).
    /// </summary>
    public int BoardSize { get; }

    /// <summary>
    /// Whether jokers are enabled in this match.
    /// </summary>
    public bool JokersEnabled { get; }

    /// <summary>
    /// Seed used for deterministic randomness.
    /// Important for replay systems and multiplayer sync.
    /// </summary>
    public int Seed { get; }

    public GameSettings(
        int playerCount,
        int boardSize = 10,
        bool jokersEnabled = true,
        int seed = 0)
    {
        PlayerCount = playerCount;
        BoardSize = boardSize;
        JokersEnabled = jokersEnabled;
        Seed = seed;
    }
}