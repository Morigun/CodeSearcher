using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSearcher.Commands.Arguments.Implementation
{
    public class EmptyArgument : IArgument
    {
        public string Name { get; }

        public string? Value { get; set; }

        public bool HasValue { get; }
        public EmptyArgument(string name, string? value)
        {
            Name = name;
            Value = value;
            HasValue = false;
        }

        public void ValueHandle()
        {
            throw new NotImplementedException();
        }
    }
}
