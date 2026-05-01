using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;

namespace ColorPop.Core.Rules;

internal class WinConditionEvaluator : IWinConditionEvaluator
{
    public GameResult Evaluate(GameState state)
    {
        throw new NotImplementedException();
    }

    public Player? GetWinner(GameState state)
    {
        throw new NotImplementedException();
    }

    public bool IsGameOver(GameState state)
    {
        throw new NotImplementedException();
    }
}
