using ColorPop.Application;
using ColorPop.Application.Interface;
using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using Microsoft.AspNetCore.Components;
using GameBoard = ColorPop.Core.Models.Board;

namespace ColorPop.Web.Components.Board;

public partial class BoardView : ComponentBase, IDisposable
{
    [Parameter] public IGameSession Session { get; set; } = default!;

    private GameBoard? _previousBoard;
    private bool _isAnimating;
    private bool _suppressStateChange;
    private HashSet<Position> _removedPositions = [];
    private HashSet<Position> _droppingPositions = [];
    private HashSet<int> _collapsingColumns = [];

    protected override void OnInitialized()
    {
        Session.OnChange += HandleStateChanged;
    }

    public void Dispose()
    {
        Session.OnChange -= HandleStateChanged;
    }

    private async void OnCellClicked(Position pos)
    {
        if (_isAnimating)
            return;

        _suppressStateChange = true;

        _previousBoard = Session.State.Board;
        Session.PlayMove(new Move(Session.State.CurrentPlayer.Id, pos));

        _removedPositions = GetRemovedPositions(_previousBoard, Session.State.Board);
        _droppingPositions = GetDroppedPositions(_previousBoard, Session.State.Board);
        _isAnimating = true;

        _suppressStateChange = false;
        StateHasChanged(); // Phase 1: compress removed tokens + drop falling tokens

        await Task.Delay(500);

        // Phase 2: collapse any columns that are now entirely empty
        _removedPositions = [];
        _droppingPositions = [];
        _collapsingColumns = GetEmptyColumns(Session.State.Board);

        if (_collapsingColumns.Count > 0)
        {
            StateHasChanged();
            await Task.Delay(400);
        }

        _isAnimating = false;
        _collapsingColumns = [];
        StateHasChanged();
    }

    private void HandleStateChanged()
    {
        if (_suppressStateChange)
            return;

        InvokeAsync(StateHasChanged);
    }

    private string GetAnimClass(Position pos)
    {
        if (_isAnimating && _removedPositions.Contains(pos))
            return "token-compress";

        if (_isAnimating && _droppingPositions.Contains(pos))
            return "token-drop";

        return "";
    }

    private Token GetDisplayToken(Position pos)
    {
        if (_isAnimating && _removedPositions.Contains(pos) && _previousBoard is { } prevBoard)
            return prevBoard.GetToken(pos);

        return Session.State.Board.GetToken(pos);
    }

    private string GetTokenColorClass(Token token)
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

    private string GetCollapseClass(int col) =>
        _collapsingColumns.Contains(col) ? "cell-collapsing" : "";

    private HashSet<int> GetEmptyColumns(GameBoard board)
    {
        var empty = new HashSet<int>();

        for (int c = 0; c < board.Cols; c++)
        {
            var colEmpty = true;

            for (int r = 0; r < board.Rows; r++)
            {
                if (!board.GetToken(new Position(r, c)).IsEmpty)
                {
                    colEmpty = false;
                    break;
                }
            }

            if (colEmpty)
                empty.Add(c);
        }

        return empty;
    }

    private HashSet<Position> GetRemovedPositions(GameBoard before, GameBoard after)
    {
        var removed = new HashSet<Position>();

        for (int r = 0; r < before.Rows; r++)
            for (int c = 0; c < before.Cols; c++)
            {
                var pos = new Position(r, c);

                if (!before.GetToken(pos).IsEmpty && after.GetToken(pos).IsEmpty)
                    removed.Add(pos);
            }

        return removed;
    }

    private HashSet<Position> GetDroppedPositions(GameBoard before, GameBoard after)
    {
        var dropped = new HashSet<Position>();

        for (int r = 0; r < after.Rows; r++)
            for (int c = 0; c < after.Cols; c++)
            {
                var pos = new Position(r, c);
                var tokenBefore = before.GetToken(pos);
                var tokenAfter = after.GetToken(pos);

                if (!tokenAfter.IsEmpty && tokenAfter.Color != tokenBefore.Color)
                    dropped.Add(pos);
            }

        return dropped;
    }
}
