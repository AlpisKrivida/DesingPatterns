using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.AbstractFactory
{
    abstract class ObstacleAbstractFactory
    {
        public abstract Unit CreateObstacle(string input);
        public abstract Unit CreateSuperObstacle(string input);

    }
}
