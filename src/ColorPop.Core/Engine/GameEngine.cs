using ColorPop.Core.Abstractions;
using ColorPop.Core.Enums;
using ColorPop.Core.Interfaces;
using ColorPop.Core.Models;

namespace ColorPop.Core.Services;

/// <summary>
/// Orchestrates a full game turn by coordinating rule services.
/// </summary>
/// <remarks>
/// The GameEngine does NOT contain game rules.
/// It delegates all logic to injected services and composes the result.
/// </remarks>
public sealed class GameEngine : IGameEngine
{
    private readonly IMoveValidator _moveValidator;
    private readonly IClusterFinder _clusterFinder;
    private readonly IJokerResolver _jokerResolver;
    private readonly IGravityEngine _gravityEngine;
    private readonly IWinConditionEvaluator _winConditionEvaluator;

    public GameEngine(
        IMoveValidator moveValidator,
        IClusterFinder clusterFinder,
        IJokerResolver jokerResolver,
        IGravityEngine gravityEngine,
        IWinConditionEvaluator winConditionEvaluator)
    {
        _moveValidator = moveValidator;
        _clusterFinder = clusterFinder;
        _jokerResolver = jokerResolver;
        _gravityEngine = gravityEngine;
        _winConditionEvaluator = winConditionEvaluator;
    }

    public GameState ApplyMove(GameState state, Move move)
    {
        // 1. Validate move
        if (!_moveValidator.IsValid(state, move))
            return state;

        // 2. Find cluster from starting position
        var cluster = _clusterFinder.FindCluster(state.Board, move.StartPosition);

        // 3. Apply joker expansion rules
        var resolvedCluster = _jokerResolver.ExpandClusterWithJokers(
            state.Board,
            cluster);

        // 4. Remove tokens (pure board transformation)
        var boardAfterRemoval = state.Board.RemoveCells(resolvedCluster);

        // 5. Apply gravity
        var boardAfterGravity = _gravityEngine.ApplyGravity(boardAfterRemoval);

        // 6. Update players (NOTE: keep minimal here)
        var updatedPlayers = state.Players
            .Select(p =>
            {
                // Count how many tokens were removed belonging to that player's colors
                var capturedCount = resolvedCluster
                    .Count(pos =>
                    {
                        var token = state.Board.Get(pos);
                        return p.SecretColors.Contains(token.Color);
                    });

                for (int i = 0; i < capturedCount; i++)
                {
                    p = p.AddCaptured(state.Board.Get(resolvedCluster.First()).Color);
                }

                return p;
            })
            .ToList();

        // 7. Check win condition
        var isGameOver = _winConditionEvaluator.IsGameOver(
            state with
            {
                Board = boardAfterGravity,
                Players = updatedPlayers
            });

        var nextPlayerIndex = (state.CurrentPlayerIndex + 1) % state.Players.Count;

        // 8. Return new immutable state
        return state with
        {
            Board = boardAfterGravity,
            Players = updatedPlayers,
            Status = isGameOver ? GameStatus.Finished : state.Status,
            TurnNumber = state.TurnNumber + 1,
            CurrentPlayerIndex = nextPlayerIndex,
        };
    }
}