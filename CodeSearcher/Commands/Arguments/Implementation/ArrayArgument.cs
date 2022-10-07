using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSearcher.Commands.Arguments.Implementation
{
    public class ArrayArgument : IArgument
    {
        public string Name { get; }

        public string? Value { get; set; }
        public string[] Values { get; set; }

        public bool HasValue { get; }

        public ArrayArgument(string name)
        {
            Name = name;
            HasValue = true;
            Values = Array.Empty<string>();
        }

        public void ValueHandle()
        {
            Values = Value!.Replace("[", "").Replace("]", "").Split(",").Select(item => item.Trim()).ToArray();
        }
    }
}
