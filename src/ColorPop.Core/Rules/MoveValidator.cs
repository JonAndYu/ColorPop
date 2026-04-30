using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ColorPop.Core.Rules;

internal class MoveValidator : IMoveValidator
{
    public bool IsValid(GameState state, Move move)
    {
        throw new NotImplementedException();
    }

    public ValidationResult Validate(GameState state, Move move)
    {
        throw new NotImplementedException();
    }
}
