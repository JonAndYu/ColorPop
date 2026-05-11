using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using ColorPop.Core.Rules;

namespace ColorPop.Tests.Rules;

public class ClusterFinderTests
{
    private readonly ClusterFinder _sut = new();

    // FindAllClusters

    [Fact]
    public void FindAllClusters_EmptyBoard_ReturnsNoClusters()
    {
    }

    [Fact]
    public void FindAllClusters_AllSameColor_ReturnsSingleCluster()
    {
    }

    [Fact]
    public void FindAllClusters_DisconnectedGroups_ReturnsMultipleClusters()
    {
    }

    [Fact]
    public void FindAllClusters_IsolatedTokens_ReturnsEachAsSingleElementCluster()
    {
    }

    [Fact]
    public void FindAllClusters_DoesNotIncludeEmptyTokens()
    {
    }

    // FindCluster(board, start)

    [Fact]
    public void FindCluster_StartOnEmptyToken_ReturnsEmptySet()
    {
    }

    [Fact]
    public void FindCluster_IsolatedToken_ReturnsSingleElementSet()
    {
    }

    [Fact]
    public void FindCluster_ConnectedTokens_ReturnsFullCluster()
    {
    }

    [Fact]
    public void FindCluster_DoesNotCrossColorBoundary()
    {
    }

    [Fact]
    public void FindCluster_DoesNotIncludeDiagonalNeighbors()
    {
    }

    [Fact]
    public void FindCluster_NoJokerColor_DoesNotIncludeJokers()
    {
        // Arrange
        var board = CreateBoard([
            [TokenColor.Blue, TokenColor.Joker, TokenColor.Blue],
            [TokenColor.Empty, TokenColor.Empty, TokenColor.Empty],
            [TokenColor.Empty, TokenColor.Empty, TokenColor.Empty]
        ]);

        // Act
        var result = _sut.FindCluster(board, new Position(0, 0));

        // Assert
        result.Should().ContainSingle()
            .Which.Should().Be(new Position(0, 0));
    }

    [Fact]
    public void FindCluster_WithSelectedJokerColor_IncludesConnectedJokers()
    {
        // Arrange
        var board = CreateBoard([
            [TokenColor.Blue, TokenColor.Joker, TokenColor.Blue],
            [TokenColor.Empty, TokenColor.Empty, TokenColor.Empty],
            [TokenColor.Empty, TokenColor.Empty, TokenColor.Empty]
        ]);

        // Act
        var result = _sut.FindCluster(board, new Position(0, 0), (TokenColor?)TokenColor.Blue);

        // Assert
        result.Should().BeEquivalentTo([
            new Position(0, 0),
            new Position(0, 1),
            new Position(0, 2)
        ]);
    }

    // FindCluster(board, start, color)

    [Fact]
    public void FindCluster_WithEmptyColor_ReturnsEmptySet()
    {
    }

    [Fact]
    public void FindCluster_WithForcedColor_IgnoresActualTokenColor()
    {
    }

    [Fact]
    public void FindCluster_WithForcedColor_ExpandsUsingProvidedColor()
    {
    }

    private static Board CreateBoard(TokenColor[][] colors)
    {
        var rows = colors.Length;
        var cols = colors[0].Length;
        var grid = new Token[rows, cols];

        for (var row = 0; row < rows; row++)
            for (var col = 0; col < cols; col++)
                grid[row, col] = new Token(colors[row][col]);

        return new Board(grid);
    }
}
