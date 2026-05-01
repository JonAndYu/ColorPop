using ColorPop.Core.Enums;
using ColorPop.Core.Models;

namespace ColorPop.Tests.Models;

public class BoardTests
{

    [Fact]
    public void Constructor_ShouldCloneInputArray_ToPreserveImmutability()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public void WithCell_ShouldReturnNewBoardInstance()
    {
        // Arrange
        var board = CreateBoard(10);

        // Act
        var updated = board.WithCell(new Position(0, 0), new Token(TokenColor.Yellow));

        // Assert
        updated.Should().NotBeSameAs(board);
    }

    [Fact]
    public void WithCell_ShouldNotModifyOriginalBoard()
    {
        // Arrange
        var board = CreateBoard(10);
        var position = new Position(0, 0);
        var token = new Token(TokenColor.Yellow);

        // Act
        var updated = board.WithCell(position, token);

        // Assert
        updated.Get(position).Should().Be(token);
        board.Get(position).Should().NotBeSameAs(updated.Get(position));
    }

    [Fact]
    public void Get_ShouldReturnCorrectTokenAtPosition()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public void GetAllPositions_ShouldReturnAllBoardPositions()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public void IsInBounds_ShouldReturnTrue_ForValidPositions()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public void IsInBounds_ShouldReturnFalse_ForInvalidPositions()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public void RemoveCell_ShouldReplaceTokenWithEmpty()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public void RemoveCells_ShouldRemoveAllSpecifiedPositions()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public void RemoveCells_ShouldReturnNewBoardInstance()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public void RemoveCells_ShouldNotModifyOriginalBoard()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public void WithCell_ShouldThrowOrHandleOutOfBounds_WhenPositionInvalid()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public void RemoveCells_ShouldHandleEmptyInput()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public void Board_ShouldRemainConsistentAfterMultipleSequentialUpdates()
    {
        // Arrange


        // Act


        // Assert
    }

    public static Board CreateBoard(int size)
    {
        var grid = new Token[size, size];

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                // Default fill: empty tokens
                grid[r, c] = Token.Empty;
            }
        }

        return new Board(grid);
    }
}
