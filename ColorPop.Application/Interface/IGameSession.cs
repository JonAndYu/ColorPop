using ColorPop.Core.Models;

namespace ColorPop.Application.Interface;

public interface IGameSession
{
    GameState State { get; }

    event Action? OnChange;

    void PlayMove(Move move);
}