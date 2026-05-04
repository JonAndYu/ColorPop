using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;
using ColorPop.Core.Utilities;

namespace ColorPop.Core.Rules;

/// <summary>
/// Finds connected clusters of same-colored tokens on the board.
/// </summary>
/// <remarks>
/// A cluster is defined as:
/// - 2 or more orthogonally adjacent tokens (no diagonals)
/// - All tokens must share the same color
/// - Empty tokens are ignored
///
/// Uses flood-fill (BFS) for deterministic and stack-safe traversal.
/// </remarks>
public sealed class ClusterFinder : IClusterFinder
{
    /// <summary>
    /// Finds all clusters present on the board.
    /// Used for:
    /// - debugging
    /// - end-game detection
    /// - AI / analysis tools
    /// </summary>
    public IEnumerable<IReadOnlySet<Position>> FindAllClusters(Board board)
    {
        var visited = new HashSet<Position>();
        var clusters = new List<IReadOnlySet<Position>>();

        for (int row = 0; row < board.Rows; row++)
        {
            for (int col = 0; col < board.Cols; col++)
            {
                var position = new Position(row, col);

                // Skip already processed tiles
                if (visited.Contains(position))
                    continue;

                var token = board.GetToken(position);

                // Skip empty cells
                if (token.Color == TokenColor.Empty)
                    continue;

                var cluster = FloodFill(board, position, token.Color);

                // Mark all positions in this cluster as visited
                foreach (var pos in cluster)
                    visited.Add(pos);

                clusters.Add(cluster);
            }
        }

        return clusters;
    }

    /// <summary>
    /// Finds the cluster connected to a starting position.
    /// Color is inferred from the starting token.
    /// </summary>
    public IReadOnlySet<Position> FindCluster(Board board, Position start)
    {
        var token = board.GetToken(start);

        if (token.Color == TokenColor.Empty)
            return new HashSet<Position>();

        return FloodFill(board, start, token.Color);
    }

    /// <summary>
    /// Finds a cluster starting from a position but forcing a specific color.
    /// Useful for joker logic or special rules.
    /// </summary>
    public IReadOnlySet<Position> FindCluster(Board board, Position start, TokenColor color)
    {
        if (color == TokenColor.Empty)
            return new HashSet<Position>();

        return FloodFill(board, start, color);
    }

    /// <summary>
    /// Core flood-fill algorithm (BFS) used to discover connected components.
    /// </summary>
    private IReadOnlySet<Position> FloodFill(Board board, Position start, TokenColor color)
    {
        var result = new HashSet<Position>();
        var queue = new Queue<Position>();

        queue.Enqueue(start);
        result.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            // Explore orthogonal neighbors only
            foreach (var direction in Direction.Orthogonal)
            {
                var neighbor = new Position(
                    current.Row + direction.RowDelta,
                    current.Col + direction.ColDelta);

                // Skip out-of-bounds positions
                if (!IsInside(board, neighbor))
                    continue;

                // Skip already visited nodes
                if (result.Contains(neighbor))
                    continue;

                var token = board.GetToken(neighbor);

                // Only include matching color tokens
                if (token.Color != color)
                    continue;

                result.Add(neighbor);
                queue.Enqueue(neighbor);
            }
        }

        return result;
    }

    /// <summary>
    /// Checks whether a position is within board bounds.
    /// </summary>
    private static bool IsInside(Board board, Position pos)
    {
        return pos.Row >= 0 &&
               pos.Row < board.Rows &&
               pos.Col >= 0 &&
               pos.Col < board.Cols;
    }
}
