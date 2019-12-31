using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Interpreter
{
    public abstract class Expression
    {
        public abstract Tuple<int, int> Interpret();
    }
}
