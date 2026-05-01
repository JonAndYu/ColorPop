using ColorPop.Core.Models;

namespace ColorPop.Core.Models;

/// <summary>
/// Represents a player's intended action on the board.
/// A Move is input to the GameEngine and does not modify state.
/// </summary>
/// <remarks>
/// The Move is validated and executed by GameEngine.
/// It is part of the command layer in the game architecture.
/// </remarks>
public sealed class Move
{
    /// <summary>
    /// The player performing the move.
    /// </summary>
    public int PlayerId { get; }

    /// <summary>
    /// The starting position used to select a cluster.
    /// The engine will expand this into a full group (flood-fill).
    /// </summary>
    public Position StartPosition { get; }

    /// <summary>
    /// Optional: explicitly selected positions (if you ever support UI selection).
    /// If null, engine will compute cluster from StartPosition.
    /// </summary>
    public IReadOnlyList<Position>? ExplicitSelection { get; }

    public Move(int playerId, Position startPosition, IReadOnlyList<Position>? explicitSelection = null)
    {
        PlayerId = playerId;
        StartPosition = startPosition;
        ExplicitSelection = explicitSelection;
    }
}
