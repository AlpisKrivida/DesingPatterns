using Lab1_1.Prototype;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    class Stone: Obstacle, IPrototype
    {
        public Stone(int x, int y) : base(x, y)
        {
            symbol = 'S';
            //---Testavimui
            Console.WriteLine("Instantiated " + this.GetType());
            //---        
        }

        public override void ResetSymbol()
        {
            symbol = 'S';
        }

        public override void Update(Map map, (int, int) cords, List<Unit> area)
        {
            //Nothing happens. A stone is a stone
        }

        public IPrototype ShallowCopy()
        {
            return (Stone)this.MemberwiseClone();
        }

        public IPrototype DeepCopy()
        {
            Stone copy = (Stone)this.MemberwiseClone();
            copy.owner = (Player)this.owner.Clone();
            return copy;
        }
    }
}
