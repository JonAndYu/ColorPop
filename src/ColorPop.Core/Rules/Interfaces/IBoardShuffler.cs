using ColorPop.Core.Models;

namespace ColorPop.Core.Interfaces;

/// <summary>
/// Responsible for generating or randomizing board layouts.
/// </summary>
/// <remarks>
/// Used during game initialization or special shuffle events.
/// Must be deterministic when provided a seed.
/// </remarks>
public interface IBoardShuffler
{
    /// <summary>
    /// Shuffles an existing board using a seed for deterministic output.
    /// </summary>
    Board Shuffle(Board board, int seed);

    /// <summary>
    /// Generates a new starting board based on game settings.
    /// </summary>
    Board GenerateInitialBoard(int seed, GameSettings settings);
}