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
            List<string> files = new List<string>();
            if (!Arguments.Any(arg => arg.Name == "-ext"))
                files = Directory.GetFiles(path).ToList();
            else
            {
                foreach(var ext in ((ArrayArgument)Arguments.First(arg => arg.Name == "-ext")).Values)
                {
                    files.AddRange(Directory.GetFiles(path, ext));
                }
            }
        }
    }
}
