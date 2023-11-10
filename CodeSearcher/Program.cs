using CodeSearcher.Commands;
using CodeSearcher.Commands.Arguments.Parse;
using CodeSearcher.Commands.Implementation;

using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        List<string?> consoleArgs = GetConsoleArgs();
        CommandController.TryGetCommand(CommandController.ParseCommand(consoleArgs?[0]), out ICommand command);
        while (command.GetType() != typeof(ExitCommand))
        {
            ArgumentParser parser = new ArgumentParser(command, consoleArgs?.ToArray());
            parser.Parse();
            command.Execute();
            consoleArgs!.Clear();
            consoleArgs = GetConsoleArgs();
            CommandController.TryGetCommand(CommandController.ParseCommand(consoleArgs?[0]), out command);
        }
    }

    private static List<string?> GetConsoleArgs()
    {
        Console.Write("Введите команду:");
        List<string?> consoleArgs = new List<string?>(new[] { Console.ReadLine() });
        while (consoleArgs.Last()?.EndsWith("/") ?? false)
        {
            Console.Write("Введите команду:");
            consoleArgs.Add(Console.ReadLine());
        }
        consoleArgs = consoleArgs.Where(arg => arg != null).ToList();
        return consoleArgs;
    }
}
