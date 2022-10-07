using CodeSearcher.Commands.Arguments;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSearcher.Commands.Implementation
{
    public class ErrorCommand : ICommand
    {
        public IArgument[] Arguments { get; }
        public ErrorCommand()
        {
            Arguments = Array.Empty<IArgument>();
        }

        public void Execute()
        {
            Console.WriteLine("Не верная команда!");
        }
    }
}
