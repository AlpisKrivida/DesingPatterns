using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public class AlgorithmFactory
    {
        public Algorithm GetDefault(string type)
        {
            switch (type)
            {
                case "Hopper":
                    return new Hopper();
                case "Tower":
                    return new Tower();
                case "Teleport":
                    return new Teleport();
                case "Standart":
                    return new Standart();
                default:
                    return null;

            }
            
        }
    }
}
