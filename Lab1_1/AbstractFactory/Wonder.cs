using Lab1_1.Prototype;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    public class Wonder: SuperObstacle, IPrototype
    {
        public Wonder(int x, int y):base(x, y)
        {
            symbol = 'W';
            //---Testavimui
            Console.WriteLine("Instantiated " + this.GetType());
            //---
        }
        public override void ResetSymbol()
        {
            symbol = 'W';
        }
        public override void Update(Map map, (int, int) cords, List<Unit> area)
        {
            //Player has to win
            //GameController.EndGame(owner)

            if (map.GetUnit(cords.Item1, cords.Item2).GetSymbol() == this.symbol)
                if (area.TrueForAll(item => item.color == area[0].color && item.color != (ConsoleColor)15))
                {
                    map.GetUnit(cords.Item1, cords.Item2).TakeUnit('W', area[0].color);
                }
        }

        public IPrototype ShallowCopy()
        {
            return (Wonder)this.MemberwiseClone();
        }

        public IPrototype DeepCopy()
        {
            Wonder copy = (Wonder)this.MemberwiseClone();
            copy.owner = (Player)this.owner.Clone();
            return copy;
        }
    }
}
