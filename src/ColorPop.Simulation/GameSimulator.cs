using ColorPop.Application;
using ColorPop.Application.Interface;
using ColorPop.Core.Abstractions;
using ColorPop.Core.Enums;
using ColorPop.Core.Models;

namespace ColorPop.Simulation;

public sealed class GameSimulator
{
    private readonly IGameSession _session;
    private readonly ICommandParser _parser;
    private readonly IBoardRenderer _renderer;

    public GameSimulator(
        IGameSession session,
        ICommandParser parser,
        IBoardRenderer renderer)
    {
        _session = session;
        _parser = parser;
        _renderer = renderer;
    }

    /// <summary>
    /// Runs the main game loop until the game ends.
    /// </summary>
    public void Run(GameState initialState)
    {
        // Session already owns state, so we sync once
        var state = _session.State;

        while (state.Status == GameStatus.InProgress)
        {
            Console.Clear();

            _renderer.Render(state.Board);

            Console.WriteLine($"Player {state.CurrentPlayer.Name}'s turn");
            Console.WriteLine("Enter move (row col):");

            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            try
            {
                var move = _parser.Parse(input, state.CurrentPlayer.Id);

                // ✔ THIS is the key change
                _session.PlayMove(move);

                state = _session.State;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid move: {ex.Message}");
                Console.ReadKey();
            }
        }

        Console.Clear();
        _renderer.Render(state.Board);

        Console.WriteLine("Game Over!");
    }
}
