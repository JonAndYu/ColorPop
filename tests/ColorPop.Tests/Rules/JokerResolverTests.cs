using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using ColorPop.Core.Rules;

namespace ColorPop.Tests.Rules;

public class JokerResolverTests
{
    private readonly JokerResolver _sut = new();

    // ExpandClusterWithJokers

    [Fact]
    public void ExpandClusterWithJokers_NoAdjacentJokers_ReturnsSameCluster()
    {
    }

    [Fact]
    public void ExpandClusterWithJokers_AdjacentJoker_IncludesJokerInResult()
    {
    }

    [Fact]
    public void ExpandClusterWithJokers_JokerChain_ExpandsTransitively()
    {
    }

    [Fact]
    public void ExpandClusterWithJokers_JokersNotAdjacent_ExcludesDistantJokers()
    {
    }

    // ResolveColor

    [Fact]
    public void ResolveColor_SingleAdjacentColor_ReturnsThatColor()
    {
    }

    [Fact]
    public void ResolveColor_MultipleAdjacentColors_ReturnsMostFrequentColor()
    {
    }

    [Fact]
    public void ResolveColor_TiedAdjacentColors_ReturnsDeterministicColor()
    {
    }

    [Fact]
    public void ResolveColor_NoAdjacentTokens_ReturnsEmpty()
    {
    }
}
