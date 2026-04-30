using ColorPop.Core.Models;

namespace ColorPop.Core.Interfaces;

/// <summary>
/// Evaluates whether the game has ended and determines the winner.
/// </summary>
/// <remarks>
/// This interface encapsulates all end-game logic:
/// - no moves remaining
/// - elimination
/// - scoring comparison
/// </remarks>
public interface IWinConditionEvaluator
{
    /// <summary>
    /// Returns true if the game is finished.
    /// </summary>
    bool IsGameOver(GameState state);

    /// <summary>
    /// Returns the winning player, or null if no winner yet or draw.
    /// </summary>
    Player? GetWinner(GameState state);

    /// <summary>
    /// Returns a full evaluation of the game result.
    /// </summary>
    GameResult Evaluate(GameState state);
}