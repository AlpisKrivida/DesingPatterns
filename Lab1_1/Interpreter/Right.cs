using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Interpreter
{
    class Right : Expression
    {
        protected Expression _topExp, _botExp = null;

        public Right(Expression topExp, Expression botExp)
        {
            _topExp = topExp;
            _botExp = botExp;
        }

        public override Tuple<int, int> Interpret()
        {
            return Tuple.Create(_botExp.Interpret().Item1 + _topExp.Interpret().Item1,
                _botExp.Interpret().Item2);
        }
    }
}
