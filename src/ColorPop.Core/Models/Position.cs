namespace ColorPop.Core.Models;

/// <summary>
/// Represents a coordinate on the game board.
/// Immutable value object used for all board operations.
/// </summary>
/// <remarks>
/// Row = vertical index (Y-axis)
/// Col = horizontal index (X-axis)
///
/// Used for:
/// - board access
/// - cluster detection
/// - move targeting
/// </remarks>
public readonly record struct Position(int Row, int Col)
{
    /// <summary>
    /// Adds a directional offset to this position.
    /// Useful for traversal (flood fill, gravity, etc.)
    /// </summary>
    public Position Offset(int rowDelta, int colDelta)
        => new(Row + rowDelta, Col + colDelta);

    /// <summary>
    /// Adds another position as a vector offset.
    /// </summary>
    public Position Offset(Position delta)
        => new(Row + delta.Row, Col + delta.Col);
}
