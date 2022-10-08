using CodeSearcher.Commands.Arguments;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSearcher.Commands.Implementation
{
    public class ClearCommand : ICommand
    {
        public IArgument[] Arguments { get; } = Array.Empty<IArgument>();

        public void Execute()
        {
            Console.Clear();
        }
    }
}
