using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using ColorPop.Core.Rules;
using FluentAssertions;

namespace ColorPop.Tests.Rules;

public class JokerResolverTests
{
    private readonly JokerResolver _sut = new();

    // ----------------------------
    // Helper (INLINE ONLY)
    // ----------------------------
    private static Board CreateBoard(Token[,] grid)
        => new Board(grid);

    private static Token T(TokenColor color)
        => new Token(color);

    // ----------------------------
    // ResolveColor Tests
    // ----------------------------

    [Fact]
    public void ResolveColor_ShouldReturnEmpty_WhenNoAdjacentTokens()
    {
        // Arrange
        var grid = new Token[3, 3];

        grid[1, 1] = T(TokenColor.Joker);

        var board = CreateBoard(grid);

        var pos = new Position(1, 1);

        // Act
        var result = _sut.ResolveColor(board, pos);

        // Assert
        result.Should().Be(TokenColor.Empty);
    }

    [Fact]
    public void ResolveColor_ShouldReturnMostFrequentAdjacentColor()
    {
        // Arrange
        var grid = new Token[3, 3];

        grid[1, 1] = T(TokenColor.Joker);

        grid[0, 1] = T(TokenColor.Blue);
        grid[1, 0] = T(TokenColor.Blue);
        grid[1, 2] = T(TokenColor.Yellow);

        var board = CreateBoard(grid);

        var pos = new Position(1, 1);

        // Act
        var result = _sut.ResolveColor(board, pos);

        // Assert
        result.Should().Be(TokenColor.Blue);
    }

    [Fact]
    public void ResolveColor_ShouldIgnoreEmptyAndJokers()
    {
        // Arrange
        var grid = new Token[3, 3];

        grid[1, 1] = T(TokenColor.Joker);

        grid[0, 1] = T(TokenColor.Empty);
        grid[1, 0] = T(TokenColor.Joker);
        grid[1, 2] = T(TokenColor.Green);

        var board = CreateBoard(grid);

        // Act
        var result = _sut.ResolveColor(board, new Position(1, 1));

        // Assert
        result.Should().Be(TokenColor.Green);
    }

    // ----------------------------
    // ExpandClusterWithJokers Tests
    // ----------------------------

    [Fact]
    public void ExpandClusterWithJokers_ShouldReturnBaseCluster_WhenNoJokers()
    {
        // Arrange
        var grid = new Token[3, 3];

        grid[1, 1] = T(TokenColor.Blue);

        var board = CreateBoard(grid);

        var baseCluster = new HashSet<Position>
        {
            new Position(1, 1)
        };

        // Act
        var result = _sut.ExpandClusterWithJokers(board, baseCluster);

        // Assert
        result.Should().BeEquivalentTo(baseCluster);
    }

    [Fact]
    public void ExpandClusterWithJokers_ShouldIncludeAdjacentJokers()
    {
        // Arrange
        var grid = new Token[3, 4];

        grid[1, 1] = T(TokenColor.Blue);
        grid[1, 2] = T(TokenColor.Joker);
        grid[1, 3] = T(TokenColor.Joker);

        var board = CreateBoard(grid);

        var baseCluster = new HashSet<Position>
    {
        new Position(1, 1)
    };

        // Act
        var result = _sut.ExpandClusterWithJokers(board, baseCluster);

        // Assert
        result.Should().Contain(new Position(1, 2));
        result.Should().Contain(new Position(1, 3));
    }

    [Fact]
    public void ExpandClusterWithJokers_ShouldNotModifyBaseCluster()
    {
        // Arrange
        var grid = new Token[3, 3];

        grid[1, 1] = T(TokenColor.Blue);
        grid[1, 2] = T(TokenColor.Joker);

        var board = CreateBoard(grid);

        var baseCluster = new HashSet<Position>
        {
            new Position(1, 1)
        };

        // Act
        var result = _sut.ExpandClusterWithJokers(board, baseCluster);

        // Assert
        baseCluster.Should().Contain(new Position(1, 1));
        result.Should().NotBeSameAs(baseCluster);
    }
}
