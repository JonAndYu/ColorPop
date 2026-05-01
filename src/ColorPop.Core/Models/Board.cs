using ColorPop.Core.Enums;
using ColorPop.Core.Models;

namespace ColorPop.Core.Models;

/// <summary>
/// Represents the game board as an immutable 2D grid of tokens.
/// </summary>
/// <remarks>
/// The board is a snapshot of the current token layout.
/// It does not contain game logic—only state.
///
/// Any modification returns a NEW Board instance.
/// This is essential for:
/// - multiplayer synchronization
/// - undo/redo systems
/// - deterministic replay
/// </remarks>
public sealed class Board
{
    private readonly Token[,] _cells;

    /// <summary>
    /// Number of rows in the board.
    /// </summary>
    public int Rows { get; }

    /// <summary>
    /// Number of columns in the board.
    /// </summary>
    public int Cols { get; }

    /// <summary>
    /// Creates a new Board from a 2D token array.
    /// The array is cloned to preserve immutability.
    /// </summary>
    public Board(Token[,] cells)
    {
        Rows = cells.GetLength(0);
        Cols = cells.GetLength(1);

        _cells = (Token[,])cells.Clone();
    }

    /// <summary>
    /// Gets the token at a specific position.
    /// </summary>
    public Token GetToken(Position position)
    {
        if (!IsInBounds(position))
            throw new ArgumentOutOfRangeException(nameof(position), position, "Position is outside the board.");

        return _cells[position.Row, position.Col];
    }

    /// <summary>
    /// Returns a new Board with a single cell updated.
    /// </summary>
    public Board UpdateCell(Position position, Token token)
    {
        if (!IsInBounds(position))
            throw new ArgumentOutOfRangeException(nameof(position), position, "Position is outside the board.");

        var copy = (Token[,])_cells.Clone();
        copy[position.Row, position.Col] = token;
        return new Board(copy);
    }

    /// <summary>
    /// Removes a group of tokens from the board (sets it to empty/air).
    /// </summary>
    public Board RemoveCells(IEnumerable<Position> positions)
    {
        var board = this;

        foreach (var pos in positions)
        {
            if (!IsInBounds(pos))
                throw new ArgumentOutOfRangeException(nameof(positions), pos, "One or more positions are out of bounds.");

            board = board.RemoveCell(pos);
        }

        return board;
    }

    /// <summary>
    /// Returns all positions in the board.
    /// Useful for scanning clusters or debugging.
    /// </summary>
    public IEnumerable<Position> GetAllPositions()
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                yield return new Position(r, c);
            }
        }
    }

    /// <summary>
    /// Checks if a position is within board bounds.
    /// </summary>
    public bool IsInBounds(Position pos)
        => pos.Row >= 0 &&
           pos.Row < Rows &&
           pos.Col >= 0 &&
           pos.Col < Cols;

    /// <summary>
    /// Removes a token from the board (sets it to empty/air).
    /// </summary>
    private Board RemoveCell(Position position)
        => UpdateCell(position, Token.Empty);
}
