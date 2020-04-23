using System.Collections.Generic;
using ConsoleTables;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public static class ConsolePrinter
    {
        public static void WriteCollectionAsTable<T>(this IEnumerable<T> items)
        {
            ConsoleTable.From(items).Write();
        }
    }
}