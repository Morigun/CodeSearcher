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
/*Console.Write("Введине путь к проекту:");
var path = Console.ReadLine();
Console.Write("Введите искомый текст:");
var search = Console.ReadLine();
while (path != null && search != null)
{
   if (Directory.Exists(path))
   {
       Console.WriteLine("CS:");
       Console.WriteLine();
       WriteFiles("*.cs");
       Console.WriteLine("XML:");
       Console.WriteLine();
       WriteFiles("*.xml");
   }
   else
       Console.WriteLine("Не верный путь");
   Console.Write("Очистить консоль?(Y/N)");
   var isClearConsole = Console.ReadLine()?.ToLower() == "y";
   if (isClearConsole)
       Console.Clear();
   Console.Write("Введине путь к проекту:");
   path = Console.ReadLine();
   Console.Write("Введите искомый текст:");
   search = Console.ReadLine();
}

void WriteFiles(string ext)
{
   var files = Directory.GetFiles(path, ext, SearchOption.AllDirectories);
   int current = 1;
   foreach (var file in files)
   {
       var info = SearchInFile(file, search);
       if (!string.IsNullOrEmpty(info))
           Console.WriteLine(info);
       SetTitlePrc(current++, files.Length);
   }
}

string SearchInFile(string file, string search)
{
   StringBuilder builder = new();
   bool added = false;
   var lines = File.ReadAllLines(file);
   foreach (var line in lines)
   {
       if (line.Contains(search, StringComparison.OrdinalIgnoreCase))
       {
           if (!added)
           {
               builder.Append(file);
               builder.AppendLine(":");
               added = true;
           }
           builder.AppendLine(line);
       }

   }
   return builder.ToString();
}
void SetTitlePrc(int current, int all)
{
   Console.Title = $"{100D/all*current}%";
}*/