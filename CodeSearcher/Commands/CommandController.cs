using CodeSearcher.Commands.Implementation;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSearcher.Commands
{
    public class CommandController
    {
        public static Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>
        {
            { "SEARCH", new SearchCommand() },
            { "EXIT", new ExitCommand() },
            { "CLEAR", new ClearCommand() }
        };
        private static ErrorCommand Error = new ErrorCommand();
        public static bool TryGetCommand(string? name, out ICommand command)
        {
            if (name == null)
            {
                command = Error;
                return false;
            }
            if (Commands.TryGetValue(name.ToUpper(), out command))
                return true;
            command = Error;
            return false;
        }
        public static string? ParseCommand(string? arg)
        {
            if (arg == null)
                return null;
            if (arg.Contains(" "))
                return arg.Substring(0, arg.IndexOf(" "));
            else
                return arg;
        }
    }
}
