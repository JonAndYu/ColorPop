using System.ComponentModel.DataAnnotations;
using ColorPop.Core.Models;

namespace ColorPop.Core.Interfaces;

/// <summary>
/// Validates whether a player's move is legal in the current game state.
/// </summary>
/// <remarks>
/// This interface is responsible ONLY for validation logic.
/// It does not modify state or execute moves.
///
/// Used before applying any game transformations.
/// </remarks>
public interface IMoveValidator
{
    /// <summary>
    /// Returns true if the move is valid under current game rules.
    /// </summary>
    public bool IsValid(GameState state, Move move);

    /// <summary>
    /// Performs a detailed validation and returns reasons for failure.
    /// Useful for UI feedback and debugging.
    /// </summary>
    public ValidationResult Validate(GameState state, Move move);
}
