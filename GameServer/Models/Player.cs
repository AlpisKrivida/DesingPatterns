using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }
        public int NumberOfActions { get; set; }
        public int MoneyMultiplier { get; set; }
        public ConsoleColor color { get; set; }
        public int currentX { get; set; }
        public int currentY { get; set; }
        public Player()
        {
        }
    }
}
