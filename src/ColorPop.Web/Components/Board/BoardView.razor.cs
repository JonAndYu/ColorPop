using ColorPop.Application;
using ColorPop.Application.Interface;
using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using Microsoft.AspNetCore.Components;

namespace ColorPop.Web.Components.Board;

public partial class BoardView
{

    [Parameter] public IGameSession Session { get; set; } = default!;

    private void OnCellClicked(Position pos)
    {
        var move = new Move(Session.State.CurrentPlayer.Id, pos);

        Session.PlayMove(move);
    }

    private string GetCellClass(Token token)
    {
        return token.Color switch
        {
            TokenColor.Blue => "blue",
            TokenColor.Green => "green",
            TokenColor.Yellow => "yellow",
            TokenColor.Pink => "pink",
            TokenColor.Orange => "orange",
            TokenColor.Joker => "joker",
            _ => "empty"
        };
    }
}
