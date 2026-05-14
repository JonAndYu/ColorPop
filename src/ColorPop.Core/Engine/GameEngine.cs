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
        var cluster = _clusterFinder.FindCluster(
            state.Board,
            move.StartPosition,
            state.SelectedJokerColor);

        // 3. Joker handling is performed by ClusterFinder when SelectedJokerColor is set.
        var resolvedCluster = cluster;

        // 4. Remove tokens (pure board transformation)
        var boardAfterRemoval = state.Board.RemoveCells(resolvedCluster);

        // 5. Apply gravity
        var boardAfterGravity = _gravityEngine.ApplyGravity(boardAfterRemoval);

        // 6. Update the current player's captured colors. Jokers help form clusters,
        // but they do not score as the selected color.
        var updatedPlayers = state.Players.ToList();
        var currentPlayer = updatedPlayers[state.CurrentPlayerIndex];

        foreach (var position in resolvedCluster)
        {
            var token = state.Board.GetToken(position);

            if (token.Color is TokenColor.Empty or TokenColor.Joker)
                continue;

            currentPlayer = currentPlayer.AddCaptured(token.Color);
        }

        updatedPlayers[state.CurrentPlayerIndex] = currentPlayer;

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
            SelectedJokerColor = null,
        };
    }
}
