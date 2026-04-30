using ColorPop.Core.Enums;
using ColorPop.Core.Models;

namespace ColorPop.Core.Interfaces;

/// <summary>
/// Finds connected groups of tokens on the board.
/// </summary>
/// <remarks>
/// Used to determine which tokens will be removed during a move.
/// Implements flood-fill or similar traversal logic.
/// </remarks>
public interface IClusterFinder
{
    /// <summary>
    /// Finds a connected cluster starting from a position.
    /// </summary>
    IReadOnlySet<Position> FindCluster(Board board, Position start);

    /// <summary>
    /// Finds a cluster using an explicit target color.
    /// Useful for joker resolution or override logic.
    /// </summary>
    IReadOnlySet<Position> FindCluster(Board board, Position start, TokenColor color);

    /// <summary>
    /// Finds all valid clusters on the board.
    /// Useful for detecting end-game conditions.
    /// </summary>
    IEnumerable<IReadOnlySet<Position>> FindAllClusters(Board board);
}