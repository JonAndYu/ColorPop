using ColorPop.Server.Models;

namespace ColorPop.Server.Services;

public interface IRoomService
{
    GameRoom CreateRoom();
    GameRoom JoinRoom(string code, string connectionId);
    GameRoom? GetRoom(string code);
    void RemoveConnection(string connectionId);
}
