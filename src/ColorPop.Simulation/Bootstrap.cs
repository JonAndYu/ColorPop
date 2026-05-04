using ColorPop.Application;
using ColorPop.Core.Abstractions;
using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;
using ColorPop.Core.Rules;
using ColorPop.Core.Services;
using ColorPop.Simulation.Input;

namespace ColorPop.ConsoleApp;

public static class Bootstrap
{
    public static GameSession CreateGameSession()
    {
        // ----------------------------
        // Infrastructure
        // ----------------------------
        IBoardShuffler boardShuffler = new BoardShuffler();
        IGravityEngine gravityEngine = new GravityEngine();
        IClusterFinder clusterFinder = new ClusterFinder();
        IMoveValidator moveValidator = new MoveValidator();
        IWinConditionEvaluator winCondition = new WinConditionEvaluator();
        IJokerResolver jokerResolver = new JokerResolver();

        // ----------------------------
        // Engine
        // ----------------------------
        IGameEngine gameEngine = new GameEngine(
            moveValidator,
            clusterFinder,
            jokerResolver,
            gravityEngine,
            winCondition);

        // ----------------------------
        // Settings
        // ----------------------------
        var settings = new GameSettings(playerCount: 2);

        // ----------------------------
        // Session (NOW owns initialization)
        // ----------------------------
        return new GameSession(
            gameEngine,
            boardShuffler,
            settings);
    }
}
