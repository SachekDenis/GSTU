using System.Collections.Generic;
using ConsoleTables;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    internal class ConsolePrinter
    {
        public void WriteCollectionAsTable<T>(IEnumerable<T> items)
        {
            ConsoleTable.From(items).Write();
        }
    }
}
