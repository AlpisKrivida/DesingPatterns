using Lab1_1.Prototype;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    public class ActionTower: SuperObstacle, IPrototype
    {
        public ActionTower(int x, int y) : base(x, y)
        {
            symbol = 'A';
            //---Testavimui
            Console.WriteLine("Instantiated " + this.GetType());
            //---

        }
        public override void ResetSymbol()
        {
            symbol = 'A';
        }

        public override void Update(Map map, (int, int) cords, List<Unit> area)
        {
            //Player has to get more moves
            //owner.NumberOfActions++;

            if (map.GetUnit(cords.Item1, cords.Item2).GetSymbol() == this.symbol)
                if (area.TrueForAll(item => item.color == area[0].color && item.color != (ConsoleColor)15))
                {
                    map.GetUnit(cords.Item1, cords.Item2).TakeUnit('0', area[0].color);
                    map.GetUnit(cords.Item1, cords.Item2).owner = new Player { NumberOfActions = 1 };
                }
        }

        public IPrototype ShallowCopy()
        {
            return (ActionTower)this.MemberwiseClone();
        }

        public IPrototype DeepCopy()
        {
            ActionTower copy = (ActionTower)this.MemberwiseClone();
            copy.owner = (Player)this.owner.Clone();
            return copy;
        }
    }
}
