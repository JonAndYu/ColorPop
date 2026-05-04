using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;
using ColorPop.Core.Utilities;

namespace ColorPop.Core.Rules;

public class JokerResolver : IJokerResolver
{
    private static readonly IReadOnlyList<Direction> Directions = Direction.All;

    public TokenColor ResolveColor(Board board, Position jokerPosition)
    {
        var colorCounts = new Dictionary<TokenColor, int>();

        foreach (var dir in Directions)
        {
            var neighbor = new Position(
                jokerPosition.Row + dir.RowDelta,
                jokerPosition.Col + dir.ColDelta);

            if (!board.IsInBounds(neighbor))
                continue;

            var token = board.GetToken(neighbor);

            if (token.IsEmpty || token.Color == TokenColor.Joker)
                continue;

            if (!colorCounts.TryAdd(token.Color, 1))
                colorCounts[token.Color]++;
        }

        if (colorCounts.Count == 0)
            return TokenColor.Empty;

        return colorCounts
            .OrderByDescending(x => x.Value)
            .First()
            .Key;
    }

    public IReadOnlySet<Position> ExpandClusterWithJokers(
        Board board,
        IReadOnlySet<Position> baseCluster)
    {
        var result = new HashSet<Position>(baseCluster);
        var queue = new Queue<Position>(baseCluster);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var dir in Directions)
            {
                var neighbor = new Position(
                    current.Row + dir.RowDelta,
                    current.Col + dir.ColDelta);

                if (!board.IsInBounds(neighbor))
                    continue;

                if (result.Contains(neighbor))
                    continue;

                var token = board.GetToken(neighbor);

                if (!token.IsJoker)
                    continue;

                // Joker is connected → include it
                result.Add(neighbor);
                queue.Enqueue(neighbor);
            }
        }

        return result;
    }
}
