using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class FactionFactory : Factory
    {
       
        public override Player CreatePlayerWithFaction(string input)
        {
            Console.WriteLine("Factory pattern is returning wanted faction---------------");

            switch (input)
            {
                case "H":
                    return new Hunter("Hunter");
                case "W":
                    return new Wolf("Wolf");
                case "HW":
                    return new HardWorker("Hard Worker");
                default:
                    return null;

            }
        }
    }
}
