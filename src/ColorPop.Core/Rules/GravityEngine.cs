using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;

namespace ColorPop.Core.Rules;

public class GravityEngine : IGravityEngine
{
    public Board ApplyGravity(Board board)
    {
        var cells = Clone(board);

        for (int col = 0; col < board.Cols; col++)
        {
            int writeRow = board.Rows - 1;

            // Move non-empty tokens downward
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

            // Fill remaining with empty (safety step)
            for (int row = writeRow; row >= 0; row--)
            {
                cells[row, col] = Token.Empty;
            }
        }

        return new Board(cells);
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
