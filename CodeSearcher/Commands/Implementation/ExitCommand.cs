﻿using CodeSearcher.Commands.Arguments;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSearcher.Commands.Implementation
{
    public class ExitCommand : ICommand
    {
        public IArgument[] Arguments { get; }

        public void Execute()
        {
            Environment.Exit(0);
        }
    }
}
