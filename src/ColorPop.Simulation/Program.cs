using ColorPop.Core.Abstractions;
using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;
using ColorPop.Core.Rules;
using ColorPop.Core.Services;
using ColorPop.Simulation;
using ColorPop.Simulation.Input;

namespace ColorPop.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        // ----------------------------
        // 1. Infrastructure / services
        // ----------------------------
        IBoardShuffler boardShuffler = new BoardShuffler();
        IGravityEngine gravityEngine = new GravityEngine();
        IClusterFinder clusterFinder = new ClusterFinder();
        IMoveValidator moveValidator = new MoveValidator();
        IWinConditionEvaluator winCondition = new WinConditionEvaluator();
        IJokerResolver jokerResolver = new JokerResolver();

        // ----------------------------
        // 2. Game engine (now properly injected)
        // ----------------------------
        IGameEngine gameEngine = new GameEngine(
            moveValidator,
            clusterFinder,
            jokerResolver,
            gravityEngine,
            winCondition);

        // ----------------------------
        // 3. UI / input
        // ----------------------------
        ICommandParser commandParser = new CommandParser();
        IBoardRenderer boardRenderer = new BoardRenderer();

        // ----------------------------
        // 4. Simulator
        // ----------------------------
        var simulator = new GameSimulator(
            gameEngine,
            commandParser,
            boardRenderer);

        // ----------------------------
        // 5. Create initial board via shuffler
        // ----------------------------
        var settings = new GameSettings(playerCount: 2);

        var initialBoard = boardShuffler.GenerateInitialBoard(
            settings.Seed,
            settings);

        // ----------------------------
        // 6. Create players
        // ----------------------------
        var players = new List<Player>
        {
            new Player(1, "Player 1", new HashSet<TokenColor>()),
            new Player(2, "Player 2", new HashSet<TokenColor>())
        };

        // ----------------------------
        // 7. Create initial state
        // ----------------------------
        var initialState = new GameState(
            initialBoard,
            players,
            currentPlayerIndex: 0,
            status: GameStatus.InProgress,
            turnNumber: 0);

        // ----------------------------
        // 8. Run game
        // ----------------------------
        simulator.Run(initialState);
    }
}
