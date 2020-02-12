using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FileWriters;

namespace BusinessLogic
{
    public class ConsoleHandler
    {
        private const string inputArgument = "-i";
        private const string outputArgument = "-o";
        private const string formatArgument = "-f";
        public void ParseConsoleArguments(string[] args, ref string inputFile, ref string outputFile, ref Format format)
        {
            if (args.Length < 6
                || (args[0] != inputArgument
                && args[2] != inputArgument
                && args[4] != inputArgument)
                || (args[0] != outputArgument
                && args[2] != outputArgument
                && args[4] != outputArgument)
                || (args[0] != formatArgument
                && args[2] != formatArgument
                && args[4] != formatArgument))
                throw new ArgumentException("Invalid console arguments");

            inputFile = args[Array.IndexOf(args, inputArgument) + 1];
            outputFile = args[Array.IndexOf(args, outputArgument) + 1];
            string formatName = args[Array.IndexOf(args, formatArgument) + 1].ToLower();
            outputFile = string.Concat(outputFile, ".", formatName);

            if (!Enum.TryParse(formatName, true, out format))
                throw new ArgumentException("Invalid format name");

        }
    }
}
