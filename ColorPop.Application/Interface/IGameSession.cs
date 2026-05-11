using ColorPop.Core.Enums;
using ColorPop.Core.Models;

namespace ColorPop.Application.Interface;

public interface IGameSession
{
    public GameState State { get; }

    public event Action? OnChange;

    public void SelectJokerColor(TokenColor? color);

    public void PlayMove(Move move);
}
