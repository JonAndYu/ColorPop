using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using ColorPop.Core.Rules;
using FluentAssertions;

namespace ColorPop.Tests.Rules;

public class GravityEngineTests
{
    private readonly GravityEngine _sut = new();

    // -------------------------
    // Helpers
    // -------------------------

    private static Board CreateEmptyBoard(int size)
    {
        var grid = new Token[size, size];

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                grid[r, c] = Token.Empty;
            }
        }

        return new Board(grid);
    }

    private static Board CreateBoard(int size, TokenColor color)
    {
        var grid = new Token[size, size];
        var token = new Token(color);

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                grid[r, c] = token;
            }
        }

        return new Board(grid);
    }

    // -------------------------
    // Tests
    // -------------------------

    [Fact]
    public void ApplyGravity_ShouldReturnSameBoard_WhenBoardIsEmpty()
    {
        // Arrange
        var board = CreateEmptyBoard(5);

        // Act
        var result = _sut.ApplyGravity(board);

        // Assert
        result.Should().BeEquivalentTo(board);
    }

    [Fact]
    public void ApplyGravity_ShouldReturnSameBoard_WhenBoardIsFull()
    {
        // Arrange
        var board = CreateBoard(5, TokenColor.Yellow);

        // Act
        var result = _sut.ApplyGravity(board);

        // Assert
        result.Should().BeEquivalentTo(board);
    }

    [Fact]
    public void ApplyGravity_ShouldMoveSingleTokenToBottom()
    {
        // Arrange
        var board = CreateEmptyBoard(5)
            .UpdateCell(new Position(0, 0), new Token(TokenColor.Blue));

        // Act
        var result = _sut.ApplyGravity(board);

        // Assert
        result.GetToken(new Position(4, 0)).Color.Should().Be(TokenColor.Blue);
        result.GetToken(new Position(0, 0)).Color.Should().Be(TokenColor.Empty);
    }

    [Fact]
    public void ApplyGravity_ShouldStackTokensAtBottomOfColumn()
    {
        // Arrange
        var board = CreateEmptyBoard(5)
            .UpdateCell(new Position(0, 0), new Token(TokenColor.Blue))
            .UpdateCell(new Position(2, 0), new Token(TokenColor.Green));

        // Act
        var result = _sut.ApplyGravity(board);

        // Assert
        result.GetToken(new Position(4, 0)).Color.Should().Be(TokenColor.Green);
        result.GetToken(new Position(3, 0)).Color.Should().Be(TokenColor.Blue);
    }

    [Fact]
    public void ApplyGravity_ShouldPreserveOrderWithinColumn()
    {
        // Arrange
        var board = CreateEmptyBoard(5)
            .UpdateCell(new Position(0, 0), new Token(TokenColor.Yellow))
            .UpdateCell(new Position(1, 0), new Token(TokenColor.Green))
            .UpdateCell(new Position(2, 0), new Token(TokenColor.Blue));

        // Act
        var result = _sut.ApplyGravity(board);

        // Assert
        result.GetToken(new Position(4, 0)).Color.Should().Be(TokenColor.Blue);
        result.GetToken(new Position(3, 0)).Color.Should().Be(TokenColor.Green);
        result.GetToken(new Position(2, 0)).Color.Should().Be(TokenColor.Yellow);
    }

    [Fact]
    public void ApplyGravity_ShouldHandleMultipleColumnsIndependently()
    {
        // Arrange
        var board = CreateEmptyBoard(5)
            .UpdateCell(new Position(0, 0), new Token(TokenColor.Blue))
            .UpdateCell(new Position(0, 1), new Token(TokenColor.Green));

        // Act
        var result = _sut.ApplyGravity(board);

        // Assert
        result.GetToken(new Position(4, 0)).Color.Should().Be(TokenColor.Blue);
        result.GetToken(new Position(4, 1)).Color.Should().Be(TokenColor.Green);
    }

    [Fact]
    public void ApplyGravity_ShouldNotModifyOriginalBoard()
    {
        // Arrange
        var board = CreateEmptyBoard(5)
            .UpdateCell(new Position(0, 0), new Token(TokenColor.Pink));

        // Act
        var result = _sut.ApplyGravity(board);

        // Assert
        board.GetToken(new Position(0, 0)).Color.Should().Be(TokenColor.Pink);
    }

    [Fact]
    public void ApplyGravity_ShouldHandleMixedColumnCorrectly()
    {
        // Arrange
        var board = CreateEmptyBoard(5)
            .UpdateCell(new Position(0, 0), new Token(TokenColor.Yellow))
            .UpdateCell(new Position(2, 0), new Token(TokenColor.Green))
            .UpdateCell(new Position(4, 0), new Token(TokenColor.Blue));

        // Act
        var result = _sut.ApplyGravity(board);

        // Assert
        result.GetToken(new Position(4, 0)).Color.Should().Be(TokenColor.Blue);
        result.GetToken(new Position(3, 0)).Color.Should().Be(TokenColor.Green);
        result.GetToken(new Position(2, 0)).Color.Should().Be(TokenColor.Yellow);
    }
}
