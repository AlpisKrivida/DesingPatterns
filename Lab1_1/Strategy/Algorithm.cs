using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public abstract class Algorithm
    {
        public abstract void Action(Player player, string command, Map map, bool undo);
    }
}
