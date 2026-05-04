using System.ComponentModel.DataAnnotations;
using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;
using ColorPop.Core.Utilities;

namespace ColorPop.Core.Rules;

public class MoveValidator : IMoveValidator
{
    // TODO: A move is only valid if it results in a cluster of 2 or more tokens (after joker expansion). For simplicity sake, we just need to check it's direct neighbors to make sure they are
    // the same color. 
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
        var neighborCount = 0;

        foreach (var dir in Direction.Orthogonal)
        {
            var offset = move.StartPosition.Offset(dir);

            if (state.Board.IsInBounds(offset) && state.Board.GetToken(offset).Color == token.Color)
            {
                neighborCount++;
            }
        }

        if (neighborCount == 0)
            return new ValidationResult("Cannot select a cell with no same-color neighbors.");

        if (token.IsEmpty)
            return new ValidationResult("Cannot select an empty cell.");

        if (token.Color == TokenColor.Joker)
            return new ValidationResult("Cannot select a joker cell.");

        return ValidationResult.Success;
    }
}
