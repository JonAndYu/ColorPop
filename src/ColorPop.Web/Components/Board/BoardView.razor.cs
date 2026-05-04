using ColorPop.Application;
using ColorPop.Application.Interface;
using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using Microsoft.AspNetCore.Components;

namespace ColorPop.Web.Components.Board;

public partial class BoardView : ComponentBase, IDisposable
{

    [Parameter] public IGameSession Session { get; set; } = default!;

    private ColorPop.Core.Models.Board? _previousBoard;
    public void Dispose()
    {
        Session.OnChange -= HandleStateChanged;
    }


    protected override void OnInitialized()
    {
        Session.OnChange += HandleStateChanged;
    }

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

    private void HandleStateChanged()
    {
        _previousBoard = Session.State.Board;
        InvokeAsync(StateHasChanged);
    }

    private string GetAnimationClass(Position pos, Token current)
    {
        if (_previousBoard == null)
            return "";

        var prevToken = _previousBoard.GetToken(pos);

        if (prevToken.Color == current.Color)
            return "";

        if (!current.IsEmpty)
            return "drop shift";

        return "";
    }
}
