using ColorPop.Core.Models;
using ColorPop.Core.Rules;

namespace ColorPop.Tests.Rules;

public class MoveValidatorTests
{
    private readonly MoveValidator _sut = new();

    // IsValid

    [Fact]
    public void IsValid_ValidMove_ReturnsTrue()
    {
    }

    [Fact]
    public void IsValid_OutOfBoundsPosition_ReturnsFalse()
    {
    }

    [Fact]
    public void IsValid_EmptyCellSelected_ReturnsFalse()
    {
    }

    [Fact]
    public void IsValid_SingleIsolatedToken_ReturnsFalse()
    {
    }

    [Fact]
    public void IsValid_WrongPlayerTurn_ReturnsFalse()
    {
    }

    // Validate

    [Fact]
    public void Validate_ValidMove_ReturnsSuccessResult()
    {
    }

    [Fact]
    public void Validate_InvalidMove_ReturnsErrorResult()
    {
    }

    [Fact]
    public void Validate_ErrorResult_ContainsMeaningfulMessage()
    {
    }
}
