using ColorPop.Core.Enums;
using ColorPop.Core.Models;

namespace ColorPop.Tests.Models;

public class PlayerTests
{
    [Fact]
    public void AddCaptured_ShouldIncrementTotalCount()
    {
        // Arrange
        var player = CreatePlayer(secretColors: [TokenColor.Blue]);

        // Act
        var updated = player.AddCaptured(TokenColor.Blue);

        // Assert
        updated.TotalCapturedCount.Should().Be(1);
    }

    [Fact]
    public void AddCaptured_ShouldIncrementOwnColorCount_WhenColorMatches()
    {
        // Arrange
        var player = CreatePlayer(secretColors: [TokenColor.Green]);

        // Act
        var updated = player.AddCaptured(TokenColor.Green);

        // Assert
        updated.TotalCapturedCount.Should().Be(1);
        updated.CapturedOwnColorCount.Should().Be(1);
    }

    [Fact]
    public void AddCaptured_ShouldNotIncrementOwnColorCount_WhenColorDontMatches()
    {
        // Arrange
        var player = CreatePlayer(secretColors: [TokenColor.Green]);

        // Act
        var updated = player.AddCaptured(TokenColor.Blue);

        // Assert
        updated.TotalCapturedCount.Should().Be(1);
        updated.CapturedOwnColorCount.Should().Be(0);
    }

    [Fact]
    public void AddCaptured_ShouldReturnNewInstance()
    {
        // Arrange
        var player = CreatePlayer();

        // Act
        var updated = player.AddCaptured(TokenColor.Green);

        // Assert
        updated.Should().NotBeSameAs(player);
    }

    [Fact]
    public void AddCaptured_ShouldPreserveUnchangedFields()
    {
        // Arrange
        var player = new Player(
            id: 1,
            name: "Alice",
            secretColors: new HashSet<TokenColor> { TokenColor.Blue },
            capturedOwnColorCount: 2,
            totalCapturedCount: 5,
            isActive: true);

        // Act
        var updated = player.AddCaptured(TokenColor.Blue);

        // Assert
        updated.Id.Should().Be(player.Id);
        updated.Name.Should().Be(player.Name);
        updated.SecretColors.Should().BeEquivalentTo(player.SecretColors);
        updated.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Elimiate_ShouldSetIsActiveToFalse()
    {
        // Arrange
        var player = CreatePlayer();

        // Act
        var eliminated = player.Eliminate();

        // Assert
        eliminated.IsActive.Should().BeFalse();
    }

    [Fact]
    public void Eliminate_ShouldReturnNewInstance()
    {
        // Arrange
        var player = CreatePlayer();

        // Act
        var updated = player.Eliminate();

        // Assert
        updated.Should().NotBeSameAs(player);
    }

    [Fact]
    public void Eliminate_ShouldPreserveOtherValues()
    {
        // Arrange
        var player = new Player(
            id: 42,
            name: "Bob",
            secretColors: new HashSet<TokenColor> { TokenColor.Blue },
            capturedOwnColorCount: 3,
            totalCapturedCount: 7,
            isActive: true);

        // Act
        var eliminated = player.Eliminate();

        // Assert
        eliminated.Id.Should().Be(42);
        eliminated.Name.Should().Be("Bob");
        eliminated.SecretColors.Should().BeEquivalentTo(player.SecretColors);
        eliminated.CapturedOwnColorCount.Should().Be(3);
        eliminated.TotalCapturedCount.Should().Be(7);
        eliminated.IsActive.Should().BeFalse();
    }

    private static Player CreatePlayer(
        int id = 1,
        string name = "Test",
        IEnumerable<TokenColor>? secretColors = null)
    {
        return new Player(
            id,
            name,
            new HashSet<TokenColor>(secretColors ?? [TokenColor.Blue]));
    }
}
