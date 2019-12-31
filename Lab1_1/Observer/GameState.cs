using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_1.Observer
{
    public class GameState : Subject
    {
        public string StateGame { get; set; }
        public ConsoleColor Winner { get; set; }

        public GameState() { }
    }
}
