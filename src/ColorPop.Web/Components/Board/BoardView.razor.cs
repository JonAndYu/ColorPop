using ColorPop.Application;
using ColorPop.Application.Interface;
using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using Microsoft.AspNetCore.Components;
using GameBoard = ColorPop.Core.Models.Board;

namespace ColorPop.Web.Components.Board;

public partial class BoardView : ComponentBase, IDisposable
{
    private const int CellSizePx = 50;

    [Parameter] public IGameSession Session { get; set; } = default!;

    private GameBoard? _previousBoard;
    private bool _isAnimating;
    private bool _suppressStateChange;
    private HashSet<Position> _removedPositions = [];
    private HashSet<Position> _droppingPositions = [];
    private Dictionary<Position, int> _shiftingPositions = [];
    private static string BoardStyle => $"--board-cell-size: {CellSizePx}px;";

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
        var shiftedPositions = GetShiftedPositions(_previousBoard, Session.State.Board, _removedPositions);
        _droppingPositions = GetDroppedPositions(_previousBoard, Session.State.Board, shiftedPositions.Keys);
        _isAnimating = true;

        _suppressStateChange = false;
        StateHasChanged(); // Phase 1: compress removed tokens + drop falling tokens

        await Task.Delay(500);

        // Phase 2: shift tokens left without changing the fixed 10x10 table layout.
        var removedPositions = _removedPositions;
        _removedPositions = [];
        _droppingPositions = [];
        _shiftingPositions = shiftedPositions;

        if (_shiftingPositions.Count > 0)
        {
            StateHasChanged();
            await Task.Delay(400);
        }

        _isAnimating = false;
        _shiftingPositions = [];
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

        if (_isAnimating && _shiftingPositions.ContainsKey(pos))
            return "token-shift-left";

        return "";
    }

    private string GetShiftStyle(Position pos) =>
        _shiftingPositions.TryGetValue(pos, out var shiftCells)
            ? $"--shift-x: {shiftCells * CellSizePx}px;"
            : "";

    private string GetTokenStyle(Token token, string shiftStyle)
    {
        if (token.Color != TokenColor.Joker || Session.State.SelectedJokerColor is not { } jokerColor)
            return shiftStyle;

        return $"{shiftStyle} --joker-color: {GetTokenHexColor(jokerColor)};";
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

    private static string GetTokenHexColor(TokenColor color)
    {
        return color switch
        {
            TokenColor.Blue => "#3498db",
            TokenColor.Green => "#2ecc71",
            TokenColor.Yellow => "#f1c40f",
            TokenColor.Pink => "#ff4fb8",
            TokenColor.Orange => "#e67e22",
            _ => "#f5f5f5"
        };
    }

    private static Dictionary<Position, int> GetShiftedPositions(
        GameBoard? before,
        GameBoard after,
        HashSet<Position> removedPositions)
    {
        if (before is null)
            return [];

        var sourceColumns = GetColumnsWithRemainingTokens(before, removedPositions);
        var shifted = new Dictionary<Position, int>();

        for (int destinationCol = 0; destinationCol < sourceColumns.Count; destinationCol++)
        {
            var sourceCol = sourceColumns[destinationCol];
            var shiftCells = sourceCol - destinationCol;

            if (shiftCells <= 0)
                continue;

            for (int r = 0; r < after.Rows; r++)
            {
                var pos = new Position(r, destinationCol);

                if (!after.GetToken(pos).IsEmpty)
                    shifted[pos] = shiftCells;
            }
        }

        return shifted;
    }

    private static List<int> GetColumnsWithRemainingTokens(GameBoard before, HashSet<Position> removedPositions)
    {
        var columns = new List<int>();

        for (int c = 0; c < before.Cols; c++)
        {
            if (ColumnHasRemainingTokens(before, c, removedPositions))
                columns.Add(c);
        }

        return columns;
    }

    private static bool ColumnHasRemainingTokens(GameBoard board, int col, HashSet<Position> removedPositions)
    {
        for (int r = 0; r < board.Rows; r++)
        {
            var pos = new Position(r, col);

            if (!removedPositions.Contains(pos) && !board.GetToken(pos).IsEmpty)
                return true;
        }

        return false;
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

    private HashSet<Position> GetDroppedPositions(
        GameBoard before,
        GameBoard after,
        IEnumerable<Position> shiftedPositions)
    {
        var dropped = new HashSet<Position>();
        var shifted = shiftedPositions.ToHashSet();

        for (int r = 0; r < after.Rows; r++)
            for (int c = 0; c < after.Cols; c++)
            {
                var pos = new Position(r, c);

                if (shifted.Contains(pos))
                    continue;

                var tokenBefore = before.GetToken(pos);
                var tokenAfter = after.GetToken(pos);

                if (!tokenAfter.IsEmpty && tokenAfter.Color != tokenBefore.Color)
                    dropped.Add(pos);
            }

        return dropped;
    }
}
