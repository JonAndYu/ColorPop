using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;
using ColorPop.Core.Utilities;

namespace ColorPop.Core.Services;

/// <summary>
/// Generates and shuffles boards using deterministic randomness.
/// </summary>
public sealed class BoardShuffler : IBoardShuffler
{
    public Board GenerateInitialBoard(int seed, GameSettings settings)
    {
        var random = new RandomProvider(seed);

        var size = settings.BoardSize;
        var colors = GetPlayableColors();

        var grid = new Token[size, size];

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                var color = random.Pick(colors);
                grid[row, col] = new Token(color);
            }
        }

        return new Board(grid);
    }

    public Board Shuffle(Board board, int seed)
    {
        var random = new RandomProvider(seed);

        var size = board.Rows;
        var positions = new List<Position>();

        // collect positions
        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                positions.Add(new Position(r, c));
            }
        }

        // shuffle positions deterministically
        random.Shuffle(positions);

        // extract tokens in shuffled order
        var tokens = positions
            .Select(board.Get)
            .ToList();

        var newGrid = new Token[size, size];

        int index = 0;
        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                newGrid[r, c] = tokens[index++];
            }
        }

        return new Board(newGrid);
    }

    private static TokenColor[] GetPlayableColors()
    {
        return new[]
        {
            TokenColor.Blue,
            TokenColor.Green,
            TokenColor.Yellow,
            TokenColor.Pink,
            TokenColor.Orange,
        };
    }
}