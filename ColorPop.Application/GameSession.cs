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
    private readonly Stack<GameState> _undoStack = [];

    public GameState State { get; private set; }
    public bool IsPracticeMode { get; private set; } = true;
    public bool CanUndo => IsPracticeMode && _undoStack.Count > 0;

    public event Action? OnChange;

    public GameSession(
        IGameEngine engine,
        IBoardShuffler boardShuffler,
        GameSettings settings)
        : this(engine, boardShuffler, settings, isPracticeMode: true)
    {
    }

    public GameSession(
        IGameEngine engine,
        IBoardShuffler boardShuffler,
        GameSettings settings,
        bool isPracticeMode)
    {
        _engine = engine;
        _boardShuffler = boardShuffler;
        _settings = settings;
        IsPracticeMode = isPracticeMode;

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
        var previousState = State;
        var nextState = _engine.ApplyMove(State, move);

        if (IsPracticeMode && !ReferenceEquals(previousState, nextState))
            nextState = nextState with { CurrentPlayerIndex = 0 };

        if (!ReferenceEquals(previousState, nextState))
            _undoStack.Push(previousState);

        State = nextState;
        OnChange?.Invoke();
    }

    public void SelectJokerColor(TokenColor? color)
    {
        State = State with { SelectedJokerColor = color };
        OnChange?.Invoke();
    }

    public void UndoLastMove()
    {
        if (!CanUndo)
            return;

        State = _undoStack.Pop();
        OnChange?.Invoke();
    }
}
