using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSearcher.Commands.Arguments.Implementation
{
    public class TextArgument : IArgument
    {
        public string Name { get; }

        public string? Value { get; set; }

        public bool HasValue { get; }

        public TextArgument(string name)
        {
            Name = name;
            HasValue = true;
        }

        public void ValueHandle()
        {
            return;
        }
    }
}
