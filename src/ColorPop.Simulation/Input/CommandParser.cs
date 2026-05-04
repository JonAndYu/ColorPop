using ColorPop.Core.Models;

namespace ColorPop.Simulation.Input;

public sealed class CommandParser : ICommandParser
{
    /// <summary>
    /// Parses console input into a Move.
    /// Expected format: "row col"
    /// </summary>
    public Move Parse(string input, int playerId)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be empty.");

        var parts = input
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
            throw new FormatException("Input must be in format: 'row col'.");

        if (!int.TryParse(parts[0], out var row))
            throw new FormatException("Row must be a valid integer.");

        if (!int.TryParse(parts[1], out var col))
            throw new FormatException("Column must be a valid integer.");

        var position = new Position(row, col);

        return new Move(playerId, position);
    }
}
