using Lab1_1.Prototype;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    public class GoldMine: SuperObstacle, IPrototype
    {
        public GoldMine(int x, int y) : base(x, y)
        {
            symbol = 'G';
            //---Testavimui
            Console.WriteLine("Instantiated " + this.GetType());
            //---        
        }
        public override void ResetSymbol()
        {
            symbol = 'G';
        }

        public override void Update(Map map, (int, int) cords, List<Unit> area)
        {
            //Player has to get more gold
            //owner.MoneyMultiplier *= 2;

            if (map.GetUnit(cords.Item1, cords.Item2).GetSymbol() == this.symbol)
                if (area.TrueForAll(item => item.color == area[0].color && item.color != (ConsoleColor)15))
                {
                    map.GetUnit(cords.Item1, cords.Item2).TakeUnit('0', area[0].color);
                    map.GetUnit(cords.Item1, cords.Item2).owner = new Player { MoneyMultiplier = 1};
                }
        }

        public IPrototype ShallowCopy()
        {
            return (GoldMine)this.MemberwiseClone();
        }

        public IPrototype DeepCopy()
        {
            GoldMine copy = (GoldMine)this.MemberwiseClone();
            copy.owner = (Player)this.owner.Clone();
            return copy;
        }
    }
}
