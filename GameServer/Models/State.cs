using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class State
    {
        public long id { get; set; }
        public string StateGame { get; set; }
        public ConsoleColor Winner { get; set; }

        public State() { }
    }
}
