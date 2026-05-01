namespace ColorPop.Core.Utilities;

/// <summary>
/// Provides deterministic random values based on a seed.
/// Ensures reproducible game states for multiplayer and replays.
/// </summary>
public sealed class RandomProvider
{
    private readonly Random _random;

    public RandomProvider(int seed)
    {
        _random = new Random(seed);
    }

    /// <summary>
    /// Returns a random integer in a range.
    /// </summary>
    public int Next(int min, int max)
        => _random.Next(min, max);

    /// <summary>
    /// Returns a random integer from 0 to max (exclusive).
    /// </summary>
    public int Next(int max)
        => _random.Next(max);

    /// <summary>
    /// Returns a random float between 0 and 1.
    /// </summary>
    public double NextDouble()
        => _random.NextDouble();

    /// <summary>
    /// Randomly selects an item from a list.
    /// </summary>
    public T Pick<T>(IReadOnlyList<T> items)
    {
        if (items.Count == 0)
            throw new InvalidOperationException("Cannot pick from empty list.");

        return items[_random.Next(items.Count)];
    }

    /// <summary>
    /// Shuffles a list (Fisher-Yates).
    /// </summary>
    public void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);

            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
