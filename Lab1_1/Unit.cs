using Lab1_1.Observer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Unit : AObserver
    {
        public char symbol = '0';
        public ConsoleColor color { get; set; }
        public Player owner { get; set; }
        public int coordinateX { get; set; }
        public int coordinateY { get; set; }
        public bool isActive { get; set;}

        public Unit(int x, int y)
        {
            color = ConsoleColor.White;
            owner = null;
            coordinateX = x;
            coordinateY = y;
        }

        public override void Update(Map map, (int, int) cords, List<Unit> area)
        {
            //Console.WriteLine("Unit");
        }

        public void TakeUnit(Player owner)
        {
            this.symbol = owner.GetSymbol();
            this.color = owner.GetColor();
            this.owner = owner;
            isActive = true;
        }

        public void TakeUnit(char symbol, ConsoleColor color)
        {
            this.symbol = symbol;
            this.color = color;
            isActive = true;
        }
        public virtual void ResetSymbol()
        {
            symbol = '0';
        }

        public void ResetOwner()
        {
            owner = null;
            color = ConsoleColor.White;
        }
        public ConsoleColor GetColor()
        {
            return color;
        }

        public Player GetPlayer()
        {
            return owner;
        }

        public char GetSymbol()
        {
            return symbol;
        }

        public void SetCoordinates(int x, int y)
        {
            coordinateX = x;
            coordinateY = y;
        }
    }
}
