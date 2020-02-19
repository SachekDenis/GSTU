using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FileWriters;

namespace BusinessLogic
{
    public class ConsoleParser
    {
        private const string inputArgument = "-i";
        private const string outputArgument = "-o";
        private const string formatArgument = "-f";
        public void ParseConsoleArguments(string[] args, ref string inputFile, ref string outputFile, ref Format format)
        {
            if(args == null)
                throw new ArgumentNullException(nameof(args));

            if (args.Length < 6
                || !args.Contains(inputArgument)
                || !args.Contains(outputArgument)
                || !args.Contains(formatArgument)
                || args.Where((argument, index) =>
                (index % 2 == 0)
                && argument != inputArgument
                && argument != outputArgument
                && argument != formatArgument)
                .Count() != 0)
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
