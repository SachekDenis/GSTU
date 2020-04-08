using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public abstract class BaseConsoleService : IConsoleService
    {
        public abstract void StartConsoleLoop();
        protected abstract void PrintAll();
        protected abstract void PrintMenu();
    }
}
