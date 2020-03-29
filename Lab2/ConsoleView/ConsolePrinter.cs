using ConsoleTables;
using System.Collections.Generic;

namespace ConsoleApp.ConsoleView
{
    internal class ConsolePrinter
    {
        public void WriteCollectionAsTable<T>(IEnumerable<T> items)
        {
            ConsoleTable.From(items).Write();
        }
    }
}
