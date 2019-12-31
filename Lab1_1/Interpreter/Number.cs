using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Interpreter
{
    class Number : Expression
    {
        protected Tuple<int, int> _value;

        public Number(int valueX)
        {
            _value = Tuple.Create(valueX, 0);
        }

        public Number(int valueX, int valueY)
        {
            _value = Tuple.Create(valueX, valueY);
        }
        public override Tuple<int, int> Interpret()
        {
            return _value;
        }
    }
}
