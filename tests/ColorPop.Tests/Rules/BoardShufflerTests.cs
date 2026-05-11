using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using ColorPop.Core.Services;

namespace ColorPop.Tests.Rules;

public class BoardShufflerTests
{
    private readonly BoardShuffler _sut = new();

    // GenerateInitialBoard

    [Fact]
    public void GenerateInitialBoard_CreatesCorrectBoardDimensions()
    {
    }

    [Fact]
    public void GenerateInitialBoard_SameSeed_ProducesSameBoard()
    {
    }

    [Fact]
    public void GenerateInitialBoard_DifferentSeeds_ProduceDifferentBoards()
    {
    }

    [Fact]
    public void GenerateInitialBoard_NoEmptyCells()
    {
    }

    [Fact]
    public void GenerateInitialBoard_OnlyContainsPlayableColors()
    {
    }

    [Fact]
    public void GenerateInitialBoard_CreatesFiveJokersAndEvenColorDistribution()
    {
        // Arrange
        var settings = new GameSettings(playerCount: 2, boardSize: 10, seed: 42);

        // Act
        var board = _sut.GenerateInitialBoard(settings.Seed, settings);
        var counts = CountTokensByColor(board);

        // Assert
        counts[TokenColor.Joker].Should().Be(5);
        counts[TokenColor.Yellow].Should().Be(19);
        counts[TokenColor.Green].Should().Be(19);
        counts[TokenColor.Pink].Should().Be(19);
        counts[TokenColor.Orange].Should().Be(19);
        counts[TokenColor.Blue].Should().Be(19);
    }

    // Shuffle

    [Fact]
    public void Shuffle_SameSeed_ProducesSameArrangement()
    {
    }

    [Fact]
    public void Shuffle_DifferentSeed_ProducesDifferentArrangement()
    {
    }

    [Fact]
    public void Shuffle_PreservesAllTokens()
    {
    }

    [Fact]
    public void Shuffle_PreservesBoardDimensions()
    {
    }

    private static Dictionary<TokenColor, int> CountTokensByColor(Board board)
    {
        var counts = Enum.GetValues<TokenColor>()
            .ToDictionary(color => color, _ => 0);

        foreach (var position in board.GetAllPositions())
        {
            var color = board.GetToken(position).Color;
            counts[color]++;
        }

        return counts;
    }
}
