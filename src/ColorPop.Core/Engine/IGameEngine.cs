using ColorPop.Core.Models;

namespace ColorPop.Core.Abstractions;

/// <summary>
/// Orchestrates game progression by applying moves to a GameState.
/// </summary>
/// <remarks>
/// The GameEngine is the ONLY place where game systems are composed.
/// It does not contain rules itself—it delegates to injected services.
/// </remarks>
public interface IGameEngine
{
    /// <summary>
    /// Applies a move and returns the next immutable game state.
    /// </summary>
    GameState ApplyMove(GameState state, Move move);
}