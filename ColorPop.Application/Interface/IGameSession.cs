using ColorPop.Core.Models;

namespace ColorPop.Application.Interface;

public interface IGameSession
{
    public GameState State { get; }

    public event Action? OnChange;

    public void PlayMove(Move move);
}
