using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class MapUnit
    {
        public long Id { get; set; }
        public ConsoleColor color { get; set; }
        public char symbol { get; set; }
        public Player owner { get; set; }
        public bool isActive { get; set; }
        public int coordinateX { get; set; }
        public int coordinateY { get; set; }

        public MapUnit()
        {
        }
    }
}
