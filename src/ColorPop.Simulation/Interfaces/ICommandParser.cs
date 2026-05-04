using ColorPop.Core.Models;

public interface ICommandParser
{
    public Move Parse(string input, int playerId);
}
