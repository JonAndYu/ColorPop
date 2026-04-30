using ColorPop.Core.Enums;

namespace ColorPop.Core.Models;

/// <summary>
/// Represents a single immutable snapshot of the game at a specific moment in time.
/// This is the single source of truth for the entire game state.
/// </summary>
/// <remarks>
/// GameState is intentionally immutable:
/// - No properties can be modified after construction
/// - Any change to the game produces a NEW GameState instance
/// - This enables safe multiplayer synchronization, undo/redo, and replay systems
///
/// All game rules and mutations are handled in GameEngine, NOT here.
/// </remarks>
public sealed record GameState
{
    /// <summary>
    /// The current board configuration containing all tokens.
    /// Represents a 10x10 grid (or configurable size in future versions).
    /// </summary>
    public Board Board { get; init; }

    /// <summary>
    /// The list of players participating in the game.
    /// Each player contains hidden information (e.g., secret color) and score state.
    /// </summary>
    public IReadOnlyList<Player> Players { get; init; }

    /// <summary>
    /// Index of the player whose turn it currently is.
    /// Used to determine turn order from the Players list.
    /// </summary>
    public int CurrentPlayerIndex { get; init; }

    /// <summary>
    /// Current status of the game (InProgress, Finished, etc.).
    /// </summary>
    public GameStatus Status { get; init; }

    /// <summary>
    /// Tracks the number of turns that have occurred in the game.
    /// Useful for analytics, replay systems, or debugging.
    /// </summary>
    public int TurnNumber { get; init; }

    /// <summary>
    /// Creates a new immutable GameState snapshot.
    /// </summary>
    /// <param name="board">Current board state</param>
    /// <param name="players">List of players in the game</param>
    /// <param name="currentPlayerIndex">Index of active player</param>
    /// <param name="status">Current game status</param>
    /// <param name="turnNumber">Current turn count</param>

    public GameState(
        Board board,
        IReadOnlyList<Player> players,
        int currentPlayerIndex,
        GameStatus status,
        int turnNumber = 0)
    {
        Board = board;
        Players = players;
        CurrentPlayerIndex = currentPlayerIndex;
        Status = status;
        TurnNumber = turnNumber;
    }

    /// <summary>
    /// Gets the player whose turn it currently is.
    /// </summary>
    public Player CurrentPlayer => Players[CurrentPlayerIndex];
}
