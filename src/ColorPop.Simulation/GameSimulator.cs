using ColorPop.Core.Abstractions;
using ColorPop.Core.Enums;
using ColorPop.Core.Models;

namespace ColorPop.Simulation;

public sealed class GameSimulator
{
    private readonly IGameEngine _gameEngine;
    private readonly ICommandParser _parser;
    private readonly IBoardRenderer _renderer;

    public GameSimulator(
        IGameEngine gameEngine,
        ICommandParser parser,
        IBoardRenderer renderer)
    {
        _gameEngine = gameEngine;
        _parser = parser;
        _renderer = renderer;
    }

    /// <summary>
    /// Runs the main game loop until the game ends.
    /// </summary>
    public void Run(GameState initialState)
    {
        var state = initialState;

        while (state.Status == GameStatus.InProgress)
        {
            Console.Clear();

            // 1. Render board
            _renderer.Render(state.Board);

            // 2. Show player turn
            Console.WriteLine($"Player {state.CurrentPlayer.Name}'s turn");
            Console.WriteLine("Enter move (row col):");

            // 3. Read input
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            // 4. Parse move
            Move move;

            try
            {
                move = _parser.Parse(input, state.CurrentPlayer.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid move: {ex.Message}");
                Console.ReadKey();
                continue;
            }

            // 5. Execute game logic
            state = _gameEngine.ApplyMove(state, move);
        }

        // Final render
        Console.Clear();
        _renderer.Render(state.Board);

        Console.WriteLine("Game Over!");
    }
}
