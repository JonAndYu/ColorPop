using ColorPop.Application.Interface;
using ColorPop.Core.Abstractions;
using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;

namespace ColorPop.Application;

public class GameSession : IGameSession
{
    private readonly IGameEngine _engine;
    private readonly IBoardShuffler _boardShuffler;
    private readonly GameSettings _settings;

    public GameState State { get; private set; }

    public event Action? OnChange;

    public GameSession(
        IGameEngine engine,
        IBoardShuffler boardShuffler,
        GameSettings settings)
    {
        _engine = engine;
        _boardShuffler = boardShuffler;
        _settings = settings;

        State = CreateInitialState(settings.Seed);
    }

    private GameState CreateInitialState(int seed)
    {
        var board = _boardShuffler.GenerateInitialBoard(seed, _settings);

        var players = new List<Player>
        {
            new Player(1, "Player 1", new HashSet<TokenColor>()),
            new Player(2, "Player 2", new HashSet<TokenColor>())
        };

        return new GameState(
            board,
            players,
            currentPlayerIndex: 0,
            status: GameStatus.InProgress,
            turnNumber: 0);
    }

    public void PlayMove(Move move)
    {
        State = _engine.ApplyMove(State, move);
        OnChange?.Invoke();
    }
}
