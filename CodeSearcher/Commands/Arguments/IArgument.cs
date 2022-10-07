using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSearcher.Commands.Arguments
{
    public interface IArgument
    {
        string Name { get; }
        string? Value { get; set; }
        bool HasValue { get; }
        void ValueHandle();
    }
}