using System.Drawing;
using ColorPop.Core.Enums;
using ColorPop.Core.Models;

namespace ColorPop.Tests.Models;

public class BoardTests
{

    [Fact]
    public void Constructor_ShouldCloneInputArray_ToPreserveImmutability()
    {
        // Arrange

        var grid = new Token[10, 10];

        for (int r = 0; r < grid.GetLength(0); r++)
        {
            for (int c = 0; c < grid.GetLength(1); c++)
            {
                // Default fill: empty tokens
                grid[r, c] = Token.Empty;
            }
        }

        var board = new Board(grid);

        // Act
        grid[0, 0] = new Token(TokenColor.Blue);

        var result = board.GetToken(new Position(0, 0));

        // Assert
        result.Should().Be(Token.Empty);
    }

    [Fact]
    public void UpdateCell_ShouldReturnNewBoardInstance()
    {
        // Arrange
        var board = CreateEmptyBoard(10);

        // Act
        var updated = board.UpdateCell(new Position(0, 0), new Token(TokenColor.Yellow));

        // Assert
        updated.Should().NotBeSameAs(board);
    }

    [Fact]
    public void UpdateCell_ShouldNotModifyOriginalBoard()
    {
        // Arrange
        var board = CreateEmptyBoard(10);
        var position = new Position(0, 0);
        var token = new Token(TokenColor.Yellow);

        // Act
        var updated = board.UpdateCell(position, token);

        // Assert
        updated.GetToken(position).Should().Be(token);
        board.GetToken(position).Should().NotBeSameAs(updated.GetToken(position));
    }

    [Fact]
    public void UpdateCell_ShouldUpdateSpecifiedPosition()
    {
        // Arrange
        var board = CreateEmptyBoard(10);
        var position = new Position(0, 0);
        var token = new Token(TokenColor.Yellow);

        // Act
        var updated = board.UpdateCell(position, token);

        // Assert
        updated.GetToken(position).Should().Be(token);
    }

    [Fact]
    public void WithCell_ShouldPreserveOtherCells()
    {
        // Arrange
        var board = CreateEmptyBoard(10);
        var target = new Position(0, 0);
        var other = new Position(0, 1);

        var token = new Token(TokenColor.Yellow);

        // Act
        var updated = board.UpdateCell(target, token);

        // Assert
        updated.GetToken(other).Should().Be(board.GetToken(other));
    }

    [Fact]
    public void UpdateCell_ShouldThrow_WhenPositionOutOfBounds()
    {
        // Arrange
        var board = CreateEmptyBoard(10);
        var invalid = new Position(-1, -1);
        var token = new Token(TokenColor.Blue);

        // Act
        Action act = () => board.UpdateCell(invalid, token);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();

    }

    [Fact]
    public void GetToken_ShouldReturnCorrectTokenAtPosition()
    {
        // Arrange
        var board = CreateEmptyBoard(10);
        var position = new Position(0, 0);
        var token = new Token(TokenColor.Yellow);

        // Act
        var updated = board.UpdateCell(position, token);
        var result = updated.GetToken(position);

        // Assert
        result.Color.Should().Be(TokenColor.Yellow);
        result.Should().NotBeSameAs(token);
    }

    [Fact]
    public void GetToken_ShouldThrow_WhenPositionOutOfBounds()
    {
        // Arrange
        var board = CreateEmptyBoard(10);
        var invalid = new Position(-1, -1);

        // Act
        Action act = () => board.GetToken(invalid);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void GetAllPositions_ShouldReturnAllBoardPositions()
    {
        // Arrange
        var size = 3;

        var grid = new Token[size, size];

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                grid[r, c] = Token.Empty;
            }
        }

        var board = new Board(grid);

        // Act
        var positions = board.GetAllPositions().ToList();

        // Assert
        positions.Should().HaveCount(size * size);

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                positions.Should().Contain(new Position(r, c));
            }
        }
    }

    [Fact]
    public void RemoveCell_ShouldReplaceTokenWithEmpty()
    {
        // Arrange
        var board = CreateBoard(10, TokenColor.Blue);
        var positions = new[]
        {
            new Position(0,0),
        };

        // Act
        var updated = board.RemoveCells(positions);

        // Assert
        updated.GetToken(positions.FirstOrDefault()).Color.Should().Be(TokenColor.Empty);
    }

    [Fact]
    public void RemoveCells_ShouldRemoveAllSpecifiedPositions()
    {
        // Arrange
        var board = CreateBoard(10, TokenColor.Green);

        var positions = new[]
        {
        new Position(0, 0),
        new Position(0, 1),
        new Position(1, 0)
    };

        // Act
        var updated = board.RemoveCells(positions);

        // Assert
        foreach (var pos in positions)
        {
            updated.GetToken(pos).Color.Should().Be(TokenColor.Empty);
        }
    }

    [Fact]
    public void RemoveCells_ShouldReturnNewBoardInstance()
    {
        // Arrange
        var board = CreateBoard(10, TokenColor.Blue);
        var positions = new[]
        {
        new Position(0, 0),
        new Position(0, 1)
    };

        // Act
        var updated = board.RemoveCells(positions);

        // Assert
        updated.Should().NotBeSameAs(board);
    }

    [Fact]
    public void RemoveCells_ShouldNotModifyOriginalBoard()
    {
        // Arrange
        var board = CreateBoard(10, TokenColor.Blue);
        var positions = new[]
        {
        new Position(0, 0),
        new Position(0, 1)
    };

        var originalValues = positions
            .Select(p => board.GetToken(p))
            .ToList();

        // Act
        var updated = board.RemoveCells(positions);

        // Assert
        for (int i = 0; i < positions.Length; i++)
        {
            board.GetToken(positions[i]).Should().Be(originalValues[i]);
        }
    }

    [Fact]
    public void RemoveCells_ShouldHandleEmptyInput()
    {
        // Arrange
        var board = CreateBoard(10, TokenColor.Pink);

        var originalSample = board.GetToken(new Position(0, 0));

        var positions = Enumerable.Empty<Position>();

        // Act
        var updated = board.RemoveCells(positions);

        // Assert
        updated.Should().BeSameAs(board); // <-- change here

        updated.GetToken(new Position(0, 0))
            .Should().Be(originalSample);
    }

    [Fact]
    public void Board_ShouldRemainConsistentAfterMultipleSequentialUpdates()
    {
        // Arrange
        var board = CreateBoard(10, TokenColor.Orange);

        var pos1 = new Position(0, 0);
        var pos2 = new Position(0, 1);
        var pos3 = new Position(0, 2);

        // Act
        var step1 = board.RemoveCells(new[] { pos1 });
        var step2 = step1.RemoveCells(new[] { pos2 });
        var step3 = step2.RemoveCells(new[] { pos3 });

        // Assert
        step3.GetToken(pos1).Color.Should().Be(TokenColor.Empty);
        step3.GetToken(pos2).Color.Should().Be(TokenColor.Empty);
        step3.GetToken(pos3).Color.Should().Be(TokenColor.Empty);

        // Ensure immutability chain works
        board.GetToken(pos1).Color.Should().Be(TokenColor.Orange);
    }

    [Fact]
    public void IsInBounds_ShouldReturnTrue_ForValidCenterPosition()
    {
        // Arrange
        var board = CreateEmptyBoard(10);
        var position = new Position(5, 5);

        // Act
        var result = board.IsInBounds(position);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsInBounds_ShouldReturnTrue_ForTopLeftCorner()
    {
        // Arrange
        var board = CreateEmptyBoard(10);
        var position = new Position(0, 0);

        // Act
        var result = board.IsInBounds(position);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsInBounds_ShouldReturnTrue_ForBottomRightCorner()
    {
        // Arrange
        var board = CreateEmptyBoard(10);
        var position = new Position(9, 9);

        // Act
        var result = board.IsInBounds(position);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsInBounds_ShouldReturnFalse_ForNegativeRow()
    {
        var board = CreateEmptyBoard(10);

        var result = board.IsInBounds(new Position(-1, 5));

        result.Should().BeFalse();
    }

    [Fact]
    public void IsInBounds_ShouldReturnFalse_ForRowEqualToSize()
    {
        var board = CreateEmptyBoard(10);

        var result = board.IsInBounds(new Position(10, 5));

        result.Should().BeFalse();
    }

    [Fact]
    public void IsInBounds_ShouldReturnFalse_ForNegativeColumn()
    {
        var board = CreateEmptyBoard(10);

        var result = board.IsInBounds(new Position(5, -1));

        result.Should().BeFalse();
    }

    [Fact]
    public void IsInBounds_ShouldReturnFalse_ForColumnEqualToSize()
    {
        var board = CreateEmptyBoard(10);

        var result = board.IsInBounds(new Position(5, 10));

        result.Should().BeFalse();
    }

    [Fact]
    public void IsInBounds_ShouldReturnFalse_ForCompletelyInvalidPosition()
    {
        var board = CreateEmptyBoard(10);

        var result = board.IsInBounds(new Position(-999, 999));

        result.Should().BeFalse();
    }

    public static Board CreateEmptyBoard(int size)
    {
        return CreateBoard(size, TokenColor.Empty);
    }

    public static Board CreateBoard(int size, TokenColor color)
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
}
