using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Hunter : Player
    {
        public Hunter(string faction) : base(faction)
        {
            Money = 40;
        }
        
        public void Shoot(Player player, string command, int[][] map)
        {

        }
    }
}
