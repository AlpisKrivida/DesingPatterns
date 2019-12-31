using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public abstract class Factory
    {
        public abstract Player CreatePlayerWithFaction(string input);
    }
}
