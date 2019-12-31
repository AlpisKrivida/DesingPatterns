using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.CommandPattern
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }
}
