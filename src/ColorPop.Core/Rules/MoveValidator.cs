using System.ComponentModel.DataAnnotations;
using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;

namespace ColorPop.Core.Rules;

public class MoveValidator : IMoveValidator
{
    public bool IsValid(GameState state, Move move)
    {
        var result = Validate(state, move);
        return result == ValidationResult.Success;
    }

    public ValidationResult Validate(GameState state, Move move)
    {
        if (state.Status != GameStatus.InProgress)
            return new ValidationResult("Game is not in progress.");

        if (move.PlayerId != state.CurrentPlayer.Id)
            return new ValidationResult("Not the player's turn.");

        if (!state.Board.IsInBounds(move.StartPosition))
            return new ValidationResult("Move is out of bounds.");

        var token = state.Board.GetToken(move.StartPosition);

        if (token.IsEmpty)
            return new ValidationResult("Cannot select an empty cell.");

        if (token.Color == TokenColor.Joker)
            return new ValidationResult("Cannot select a joker cell.");

        return ValidationResult.Success;
    }
}
