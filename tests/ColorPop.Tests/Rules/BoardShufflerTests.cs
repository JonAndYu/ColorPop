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
}
