using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    class ShopFactory
    {
        public Unit CreateObstacle(string input, int x, int y)
        {
            //---Testavimui
            Console.WriteLine("Map factory. Create Obstacle - " + input);
            //---
            return new Tree(x, y);
        }

        public Unit CreateSuperObstacle(string input, int x, int y)
        {
            //---Testavimui
            Console.WriteLine("Map factory. Create SuperObstacle - " + input);
            //---
            return new ActionTower(x, y);
        }
    }
}
