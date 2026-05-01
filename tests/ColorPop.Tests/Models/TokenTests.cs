using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using FluentAssertions;

namespace ColorPop.Tests.Models;

public class TokenTests
{
    [Fact]
    public void Empty_ShouldBeEmpty_AndNotJoker()
    {
        // Act
        var token = Token.Empty;

        // Assert
        token.IsEmpty.Should().BeTrue();
        token.IsJoker.Should().BeFalse();
        token.Color.Should().Be(TokenColor.Empty);
    }

    [Fact]
    public void JokerToken_ShouldBeRecognizedCorrectly()
    {
        // Act
        var token = new Token(TokenColor.Joker);

        // Assert
        token.IsJoker.Should().BeTrue();
        token.IsEmpty.Should().BeFalse();
        token.Color.Should().Be(TokenColor.Joker);
    }

    [Fact]
    public void RegularToken_ShouldNotBeEmptyOrJoker()
    {
        // Act
        var token = new Token(TokenColor.Blue);

        // Assert
        token.IsEmpty.Should().BeFalse();
        token.IsJoker.Should().BeFalse();
        token.Color.Should().Be(TokenColor.Blue);
    }

    [Fact]
    public void Tokens_WithSameColor_ShouldBeEqual_ValueObjectBehavior()
    {
        // Arrange
        var t1 = new Token(TokenColor.Blue);
        var t2 = new Token(TokenColor.Blue);

        // Act & Assert
        t1.Should().Be(t2);
    }

    [Fact]
    public void Tokens_WithDifferentColors_ShouldNotBeEqual()
    {
        // Arrange
        var t1 = new Token(TokenColor.Blue);
        var t2 = new Token(TokenColor.Pink);

        // Act & Assert
        t1.Should().NotBe(t2);
    }
}
