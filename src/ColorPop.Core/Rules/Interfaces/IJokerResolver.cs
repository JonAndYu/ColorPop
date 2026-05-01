using ColorPop.Core.Enums;
using ColorPop.Core.Models;

namespace ColorPop.Core.Interfaces;

/// <summary>
/// Resolves how joker tokens behave during cluster formation.
/// </summary>
/// <remarks>
/// Jokers can act as wildcards or adopt adjacent colors depending on rules.
/// This interface isolates that complexity from cluster logic.
/// </remarks>
public interface IJokerResolver
{
    /// <summary>
    /// Determines what color a joker should represent in context.
    /// </summary>
    public TokenColor ResolveColor(Board board, Position jokerPosition);

    /// <summary>
    /// Expands a cluster by including valid joker substitutions.
    /// </summary>
    public IReadOnlySet<Position> ExpandClusterWithJokers(
        Board board,
        IReadOnlySet<Position> baseCluster);
}
