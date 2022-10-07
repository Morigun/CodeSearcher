using CodeSearcher.Commands.Arguments;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSearcher.Commands
{
    public interface ICommand
    {
        IArgument[] Arguments { get; }
        void Execute();
    }
}
