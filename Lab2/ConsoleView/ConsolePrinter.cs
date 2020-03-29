using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.ConsoleView
{
    class ConsolePrinter
    {
        public void WriteCollectionAsTable<T>(IEnumerable<T> items)
        {
            ConsoleTable.From(items).Write();
        }
    }
}
