using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;

namespace ColorPop.Core.Rules;

public class GravityEngine : IGravityEngine
{
    public Board ApplyGravity(Board board)
    {
        // STEP 1: vertical gravity
        var afterGravity = ApplyVerticalGravity(board);

        // STEP 2: compress columns (left shift)
        var compressed = CompressColumns(afterGravity);

        return new Board(compressed);
    }

    private static Token[,] ApplyVerticalGravity(Board board)
    {
        var cells = Clone(board);

        for (int col = 0; col < board.Cols; col++)
        {
            int writeRow = board.Rows - 1;

            for (int row = board.Rows - 1; row >= 0; row--)
            {
                var token = board.GetToken(new Position(row, col));

                if (!token.IsEmpty)
                {
                    cells[writeRow, col] = token;

                    if (writeRow != row)
                        cells[row, col] = Token.Empty;

                    writeRow--;
                }
            }

            for (int row = writeRow; row >= 0; row--)
            {
                cells[row, col] = Token.Empty;
            }
        }

        return cells;
    }

    private static Token[,] CompressColumns(Token[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        var result = new Token[rows, cols];

        int writeCol = 0;

        for (int col = 0; col < cols; col++)
        {
            if (!IsColumnEmpty(grid, col, rows))
            {
                for (int row = 0; row < rows; row++)
                {
                    result[row, writeCol] = grid[row, col];
                }

                writeCol++;
            }
        }

        // fill remaining columns with empty
        for (int col = writeCol; col < cols; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                result[row, col] = Token.Empty;
            }
        }

        return result;
    }

    private static bool IsColumnEmpty(Token[,] grid, int col, int rows)
    {
        for (int row = 0; row < rows; row++)
        {
            if (!grid[row, col].IsEmpty)
                return false;
        }

        return true;
    }

    private static Token[,] Clone(Board board)
    {
        var grid = new Token[board.Rows, board.Cols];

        for (int r = 0; r < board.Rows; r++)
        {
            for (int c = 0; c < board.Cols; c++)
            {
                grid[r, c] = board.GetToken(new Position(r, c));
            }
        }

        return grid;
    }
}
