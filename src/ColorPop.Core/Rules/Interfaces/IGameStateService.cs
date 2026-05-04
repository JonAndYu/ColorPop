using ColorPop.Core.Models;

namespace ColorPop.Web.State;

public interface IGameStateService
{
    /// <summary>
    /// Current immutable game state snapshot.
    /// </summary>
    GameState State { get; }

    /// <summary>
    /// Fired whenever the state changes (used to trigger UI refresh).
    /// </summary>
    event Action? OnChange;

    /// <summary>
    /// Executes a move through the game engine and updates state.
    /// </summary>
    void PlayMove(Move move);

    /// <summary>
    /// Resets the game to a fresh state using a seed.
    /// </summary>
    void Reset(int seed);
}
