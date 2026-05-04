using ColorPop.ConsoleApp;
using ColorPop.Simulation;
using ColorPop.Simulation.Input;

internal class Program
{
    private static void Main(string[] args)
    {
        var session = Bootstrap.CreateGameSession();

        ICommandParser commandParser = new CommandParser();
        IBoardRenderer boardRenderer = new BoardRenderer();

        var simulator = new GameSimulator(
            session,
            commandParser,
            boardRenderer);

        simulator.Run(session.State);
    }
}
