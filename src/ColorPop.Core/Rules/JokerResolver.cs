using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;

namespace ColorPop.Core.Rules;

internal class JokerResolver : IJokerResolver
{
    public IReadOnlySet<Position> ExpandClusterWithJokers(Board board, IReadOnlySet<Position> baseCluster)
    {
        throw new NotImplementedException();
    }

    public TokenColor ResolveColor(Board board, Position jokerPosition)
    {
        throw new NotImplementedException();
    }
}
