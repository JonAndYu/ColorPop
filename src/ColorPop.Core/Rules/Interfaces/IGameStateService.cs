using ColorPop.Core.Models;

namespace ColorPop.Web.State;

public interface IGameStateService
{
    /// <summary>
    /// Current immutable game state snapshot.
    /// </summary>
    public GameState State { get; }

    /// <summary>
    /// Fired whenever the state changes (used to trigger UI refresh).
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Executes a move through the game engine and updates state.
    /// </summary>
    public void PlayMove(Move move);

    /// <summary>
    /// Resets the game to a fresh state using a seed.
    /// </summary>
    public void Reset(int seed);
}
