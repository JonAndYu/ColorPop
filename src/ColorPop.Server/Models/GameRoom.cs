using ColorPop.Core.Models;

namespace ColorPop.Server.Models;

public sealed class GameRoom
{
    public string Code { get; }
    public List<string> ConnectionIds { get; } = new();
    public GameState State { get; set; }

    public bool IsFull => ConnectionIds.Count >= 2;

    public GameRoom(string code, GameState initialState)
    {
        Code = code;
        State = initialState;
    }
}
