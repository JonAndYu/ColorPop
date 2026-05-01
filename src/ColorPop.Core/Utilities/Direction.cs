namespace ColorPop.Core.Utilities;

/// <summary>
/// Represents a directional offset on a grid.
/// Used for neighbor traversal (flood-fill, cluster detection).
/// </summary>
public readonly record struct Direction(int RowDelta, int ColDelta)
{
    public static readonly Direction Up = new(-1, 0);
    public static readonly Direction Down = new(1, 0);
    public static readonly Direction Left = new(0, -1);
    public static readonly Direction Right = new(0, 1);

    public static readonly Direction UpLeft = new(-1, -1);
    public static readonly Direction UpRight = new(-1, 1);
    public static readonly Direction DownLeft = new(1, -1);
    public static readonly Direction DownRight = new(1, 1);

    /// <summary>
    /// All 8 directions (used for full adjacency checks).
    /// </summary>
    public static readonly IReadOnlyList<Direction> All = new[]
    {
        Up, Down, Left, Right,
        UpLeft, UpRight, DownLeft, DownRight
    };

    public static readonly IReadOnlyList<Direction> Orthogonal = new[]
    {
        Up, Down, Left, Right
    };
}
