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
}
