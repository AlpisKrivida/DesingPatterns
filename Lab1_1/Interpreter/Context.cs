using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Interpreter
{
    class Context
    {
        protected Stack<Expression> stack = new Stack<Expression>();
        protected Expression topExp, botExp = null;
        protected int mapSize;
        protected int x;
        protected int y;
        public Context(int x, int y, int mapSize)
        {
            this.x = x;
            this.y = y;
            this.mapSize = mapSize;
            stack.Push(new Number(x, y));
        }

        public void Parse(string text)
        {
            string[] words = text.Split();
            foreach (string word in words)
            {
                switch (word)
                {
                    case "R":
                        topExp = stack.Pop();
                        botExp = stack.Pop();
                        stack.Push(new Right(topExp, botExp));
                        break;
                    case "U":
                        topExp = stack.Pop();
                        botExp = stack.Pop();
                        stack.Push(new Up(topExp, botExp));
                        break;
                    case "D":
                        topExp = stack.Pop();
                        botExp = stack.Pop();
                        stack.Push(new Down(topExp, botExp));
                        break;
                    case "L":
                        topExp = stack.Pop();
                        botExp = stack.Pop();
                        stack.Push(new Left(topExp, botExp));
                        break;
                    default:
                        stack.Push(new Number(int.Parse(word)));
                        break;
                }
            }
        }

        public string GetCordinates()
        {
            Expression cords = stack.Pop();

            if (cords.Interpret().Item1 >= mapSize ||
                cords.Interpret().Item2 >= mapSize ||
                cords.Interpret().Item1 < 0 ||
                cords.Interpret().Item2 < 0
                )
                return string.Format("{0} {1}", x, y);
            else return string.Format("{0} {1}", cords.Interpret().Item1,
                cords.Interpret().Item2);
        }
    }
}
