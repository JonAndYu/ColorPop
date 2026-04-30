using ColorPop.Core.Models;
using ColorPop.Core.Rules;

namespace ColorPop.Tests.Rules;

public class GravityEngineTests
{
    private readonly GravityEngine _sut = new();

    // ApplyGravity

    [Fact]
    public void ApplyGravity_FullBoard_ReturnsBoardUnchanged()
    {
    }

    [Fact]
    public void ApplyGravity_EmptyCellsAboveTokens_TokensFallDown()
    {
    }

    [Fact]
    public void ApplyGravity_EmptyCellsAtBottom_EmptyCellsRiseToTop()
    {
    }

    [Fact]
    public void ApplyGravity_MultipleColumns_EachColumnIndependent()
    {
    }

    [Fact]
    public void ApplyGravity_AllEmptyColumn_ColumnRemainsEmpty()
    {
    }

    [Fact]
    public void ApplyGravity_PreservesTokenColors()
    {
    }
}
