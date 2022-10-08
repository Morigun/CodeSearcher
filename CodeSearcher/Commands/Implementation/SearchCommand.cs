using CodeSearcher.Commands.Arguments;
using CodeSearcher.Commands.Arguments.Implementation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSearcher.Commands.Implementation
{
    public class SearchCommand : ICommand
    {
        public SearchCommand()
        {
            Arguments = new IArgument[]
            {
                new TextArgument("-st"),
                new ArrayArgument("-sa"),
                new TextArgument("-p"),
                new ArrayArgument("-ext")
            };
        }
        public IArgument[] Arguments { get; }

        public void Execute()
        {
            if (Arguments == null)
            {
                Console.WriteLine("Нет аргументов!");
                return;
            }
            if (!Arguments.Any(arg => arg.Name == "-p"))
            {
                Console.WriteLine("Не указан путь для поиска");
                return;
            }
            string? path = Arguments.First(b => b.Name == "-p").Value;
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Не указан путь для поиска или пуст");
                return;
            }
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Указан не существующий путь");
                return;
            }
            string? searchText = Arguments.FirstOrDefault(arg => arg.Name == "-st")?.Value;
            string[]? searches = ((ArrayArgument?)Arguments.FirstOrDefault(arg => arg.Name == "-sa"))?.Values;
            List<string> searchList = new List<string>();
            if (!string.IsNullOrEmpty(searchText))
                searchList.Add(searchText);
            if (searches?.Any() ?? false)
                searchList.AddRange(searches);
            if (!searchList.Any())
            {
                Console.WriteLine("Не указаны аргументы для поиска");
                return;
            }
            List<string> files = new List<string>();
            if (!Arguments.Any(arg => arg.Name == "-ext"))
                files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList();
            else
            {
                foreach(var ext in ((ArrayArgument)Arguments.First(arg => arg.Name == "-ext")).Values)
                {
                    files.AddRange(Directory.GetFiles(path, ext, SearchOption.AllDirectories));
                }
            }
            Console.WriteLine("Поиск");
            Console.WriteLine();
            foreach (var search in searchList)
            {
                Console.WriteLine($" по '{search}'");
                Console.WriteLine();
                int numFile = 0;
                foreach(var file in files)
                {
                    numFile++;
                    bool printFileName = false;
                    foreach(var line in File.ReadAllLines(file))
                    {
                        if (line.Contains(search, StringComparison.OrdinalIgnoreCase))
                        {
                            if (!printFileName)
                            {
                                Console.WriteLine($"  в файле '{file}'");
                                Console.WriteLine();
                            }
                            Console.WriteLine($"   {line}");
                        }
                    }
                    Console.Title = $"{Math.Round(numFile / searchList.Count * 100D, 2)}%";
                }
            }
            Console.WriteLine("Поиск завершен");
        }
    }
}
