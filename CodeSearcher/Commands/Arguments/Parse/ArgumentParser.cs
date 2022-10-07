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
        public string[] InputLines { get; set; }
        public ArgumentParser(ICommand command, string[] inputLines)
        {
            Command = command;
            InputLines = inputLines;
        }
        public void Parse()
        {
            foreach (var argument in Command.Arguments)
            {
                if (!argument.HasValue)
                    continue;
                ArgumentPosition startPosition = FindStartPosition(argument);
                if (startPosition.StrIndex == -1)
                    continue;
                ArgumentPosition endPosition = FindEndPosition(argument, startPosition);
                var startPath = InputLines[startPosition.StrNumber].Substring(startPosition.StrIndex);
                if (startPosition.StrNumber == endPosition.StrNumber)
                {
                    if (endPosition.StrIndex == -1)
                        argument.Value = startPath;
                    else
                        argument.Value = startPath.Substring(0, endPosition.StrIndex - startPosition.StrIndex);
                }
                else
                {
                    StringBuilder argumentValue = new StringBuilder();
                    for (int i = startPosition.StrNumber; i < InputLines.Length; i++)
                    {
                        string tmpArgumentString = string.Empty;
                        if (i == startPosition.StrNumber)
                            tmpArgumentString = InputLines[i].Substring(startPosition.StrNumber);
                        else if (i == endPosition.StrNumber && endPosition.StrIndex != -1)
                            tmpArgumentString = InputLines[i].Substring(0, endPosition.StrIndex);
                        else
                            tmpArgumentString = InputLines[i];
                        argumentValue.Append(ReplaceEndString(tmpArgumentString));
                    }
                    argument.Value = argumentValue.ToString();
                }
                //Удалим команду
                argument.Value = ReplaceCommand(argument);
                //Удалим пробел
                argument.Value = ReplaceEndSpace(argument);
                //Специфичная обработка для конкретного аргумента
                argument.ValueHandle();
            }
        }
        private static string ReplaceEndSpace(IArgument argument)
        {
            if (argument.Value!.EndsWith(' '))
                return argument.Value.Substring(0, argument.Value.Length - 1);
            return argument.Value;
        }
        private static string ReplaceCommand(IArgument argument)
        {
            return argument.Value!.Replace(argument.Name, "").Substring(1);
        }

        private static string ReplaceEndString(string tmpArgumentString)
        {
            if (tmpArgumentString.EndsWith('/'))
                return tmpArgumentString.Substring(0, tmpArgumentString.Length - 1);
            return tmpArgumentString;
        }

        private ArgumentPosition FindStartPosition(IArgument argument)
        {
            ArgumentPosition position = new ArgumentPosition();
            for (int i = 0; i < InputLines.Length; i++)
            {
                var tmpIndexOf = InputLines[i].IndexOf(argument.Name);
                if (tmpIndexOf != -1)
                {
                    position.StrIndex = tmpIndexOf;
                    position.StrNumber = i;
                    break;
                }
            }

            return position;
        }
        private ArgumentPosition FindEndPosition(IArgument argument, ArgumentPosition startPosition)
        {
            ArgumentPosition position = new ArgumentPosition();
            
            for (int i = startPosition.StrNumber; i < InputLines.Length; i++)
            {
                position.StrNumber = i;
                int minOther = -1;
                foreach (var otherArgument in Command.Arguments.Where(arg => arg.Name != argument.Name))
                {
                    var nextArgIndex = InputLines[i].Substring(startPosition.StrIndex).IndexOf(otherArgument.Name);
                    if (minOther == -1 && nextArgIndex != -1)
                        minOther = nextArgIndex;
                    else if (minOther > nextArgIndex)
                        minOther = nextArgIndex;
                }
                if (minOther != -1)
                {
                    position.StrIndex = minOther;
                    break;
                }
            }

            return position;
        }
    }
}
