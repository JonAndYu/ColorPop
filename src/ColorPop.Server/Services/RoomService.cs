using System.Collections.Concurrent;
using ColorPop.Server.Models;

namespace ColorPop.Server.Services;

public class RoomService : IRoomService
{
    private readonly ConcurrentDictionary<string, GameRoom> _rooms = new();
    private readonly IGameSessionFactory _sessionFactory;

    public GameRoom CreateRoom()
    {
        throw new NotImplementedException();
    }

    public GameRoom? GetRoom(string code)
    {
        throw new NotImplementedException();
    }

    public GameRoom JoinRoom(string code, string connectionId)
    {
        throw new NotImplementedException();
    }

    public void RemoveConnection(string connectionId)
    {
        throw new NotImplementedException();
    }
}
