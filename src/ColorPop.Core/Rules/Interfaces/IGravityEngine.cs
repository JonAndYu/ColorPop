using ColorPop.Core.Models;

namespace ColorPop.Core.Interfaces;

/// <summary>
/// Applies gravity rules to a board after token removal.
/// </summary>
/// <remarks>
/// Responsible for collapsing tokens downward to fill empty spaces.
/// Must be deterministic for multiplayer and replay systems.
/// </remarks>
public interface IGravityEngine
{
    /// <summary>
    /// Applies gravity to the board and returns a new board state.
    /// </summary>
    Board ApplyGravity(Board board);
}