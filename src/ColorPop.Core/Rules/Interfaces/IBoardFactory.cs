using ColorPop.Core.Models;

namespace ColorPop.Core.Abstractions;

public interface IBoardFactory
{
    Board Create(int seed);
}
