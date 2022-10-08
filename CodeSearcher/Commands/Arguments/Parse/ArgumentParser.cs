using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSearcher.Commands.Arguments.Parse
{
    public class ArgumentParser
    {
        public ICommand Command { get; set; }
        public string?[]? InputLines { get; set; }
        private string mergedLine { get; set; }
        public ArgumentParser(ICommand command, string?[]? inputLines)
        {
            Command = command;
            InputLines = inputLines;
        }
        public void Parse()
        {
            if (InputLines == null)
                return;
            MergeInputLines();
            foreach (var argument in Command.Arguments)
            {
                if (!argument.HasValue)
                    continue;
                ArgumentPosition startPosition = FindStartPosition(argument);
                if (startPosition.StrIndex == -1)
                    continue;
                ArgumentPosition endPosition = FindEndPosition(argument, startPosition);
                var startPath = mergedLine[startPosition.StrIndex..];
                if (endPosition.StrIndex == -1)
                    argument.Value = startPath;
                else
                    argument.Value = startPath?[..(endPosition.StrIndex - startPosition.StrIndex)];
                //Удалим команду
                argument.Value = ReplaceCommand(argument);
                //Удалим пробел
                argument.Value = ReplaceEndSpace(argument);
                //Специфичная обработка для конкретного аргумента
                argument.ValueHandle();
            }
        }
        private void MergeInputLines()
        {
            var result = string.Join("", InputLines);
            mergedLine = result.Replace("/", "");
        }
        private static string ReplaceEndSpace(IArgument argument)
        {
            if (argument.Value!.EndsWith(' '))
                return argument.Value[..^1];
            return argument.Value;
        }
        private static string ReplaceCommand(IArgument argument)
        {
            return argument.Value!.Replace(argument.Name, "").Substring(1);
        }

        private ArgumentPosition FindStartPosition(IArgument argument)
        {
            ArgumentPosition position = new ArgumentPosition();
            var tmpIndexOf = mergedLine.IndexOf(argument.Name);
            if (tmpIndexOf != -1)
            {
                position.StrIndex = tmpIndexOf;
                position.StrNumber = 0;
            }
            
            return position;
        }
        private ArgumentPosition FindEndPosition(IArgument argument, ArgumentPosition startPosition)
        {
            ArgumentPosition position = new ArgumentPosition();
            
            position.StrNumber = 0;
            int minOther = -1;
            foreach (var otherArgument in Command.Arguments.Where(arg => arg.Name != argument.Name))
            {
                var nextArgIndex = mergedLine[startPosition.StrIndex..].IndexOf(otherArgument.Name);
                if (minOther == -1 && nextArgIndex != -1)
                    minOther = startPosition.StrIndex + nextArgIndex;
                else if (minOther > startPosition.StrIndex + nextArgIndex && nextArgIndex != -1)
                    minOther = startPosition.StrIndex + nextArgIndex;
            }
            if (minOther != -1)
            {
                position.StrIndex = minOther;
            }

            return position;
        }
    }
}
