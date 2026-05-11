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
    // TODO: The board should be equal amount of each color. This is just a quick implementation to get something working.
    public Board GenerateInitialBoard(int seed, GameSettings settings)
    {
        var random = new RandomProvider(seed);

        var size = settings.BoardSize;
        var tokens = CreateBalancedTokenBag(size);
        random.Shuffle(tokens);

        var grid = new Token[size, size];
        var index = 0;

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                grid[row, col] = tokens[index++];
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
            .Select(board.GetToken)
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

    private static List<Token> CreateBalancedTokenBag(int size)
    {
        const int jokerCount = 5;

        var cellCount = size * size;
        var playableColors = GetPlayableColors();
        var colorCellCount = cellCount - jokerCount;
        var baseCountPerColor = colorCellCount / playableColors.Length;
        var remainder = colorCellCount % playableColors.Length;

        var tokens = new List<Token>(cellCount);

        for (var i = 0; i < jokerCount; i++)
            tokens.Add(new Token(TokenColor.Joker));

        for (var colorIndex = 0; colorIndex < playableColors.Length; colorIndex++)
        {
            var color = playableColors[colorIndex];
            var colorCount = baseCountPerColor + (colorIndex < remainder ? 1 : 0);

            for (var i = 0; i < colorCount; i++)
                tokens.Add(new Token(color));
        }

        return tokens;
    }

    private static TokenColor[] GetPlayableColors()
    {
        return
        [
            TokenColor.Yellow,
            TokenColor.Green,
            TokenColor.Pink,
            TokenColor.Orange,
            TokenColor.Blue
        ];
    }
}
