using System;
using System.Collections.Generic;
using System.Text;
using Lab1_1.AbstractFactory;
using Lab1_1.Proxy;

namespace Lab1_1
{
    public class Map : MapDataConvert
    {
        private const double CWonderThreshold = 0.99;
        private const double CGoldMineThreshold = 0.93;
        private const double CStoneThreshold = 0.85;
        public Unit[][] Grid { get; set; }
        private static int counter = 0;
        private static MapFactory mapFactory = new MapFactory();
        private static readonly object Instancelock = new object();
        private Random random = new Random();
        private GoldMine goldMinePrototype;
        private Wonder wonderPrototype;
        private Stone stonePrototype;

        public Map()
        {
            counter++;
        }
        private static Map instance = null;

        public static Map GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (Instancelock)
                    {
                        if (instance == null)
                        {
                            instance = new Map();
                            Console.WriteLine("Singleton map instance was created------------------");
                        }
                    }
                }
                return instance;
            }
        }
        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }

        public Unit[][] GenerateGrid(int xSize, int ySize)
        {
            Console.WriteLine("===Abstract Factory===");
            goldMinePrototype = (GoldMine)mapFactory.CreateSuperObstacle("Gold Mine", 0, 0);
            wonderPrototype = (Wonder)mapFactory.CreateSuperObstacle("Wonder", 0, 0);
            stonePrototype = (Stone)mapFactory.CreateObstacle("Stone", 0, 0);
            Console.WriteLine("========");
            Console.WriteLine("===Prototype===");
            //----------Testavimui
            //stonePrototype.TakeUnit(new Player());
            //goldMinePrototype.TakeUnit(new Player());
            //----------
            Grid = new Unit[ySize][];
            for (int y = 0; y < ySize; y++)
            {
                Grid[y] = new Unit[xSize];
                for (int x = 0; x < xSize; x++)
                {
                    double obstacleValue = random.NextDouble();
                    if (obstacleValue > CWonderThreshold && !IsInOuterZone(x, y) && !isNearbyObstacle(x, y))
                    {
                        Grid[y][x] = (Unit)wonderPrototype.ShallowCopy();
                        Grid[y][x].SetCoordinates(x, y);
                        //----------Testavimui
                        //Console.WriteLine("Wonder original address " + wonderPrototype.GetHashCode() + " Wonder copy address " + Grid[y][x].GetHashCode());
                        //----------
                    }
                    else if (obstacleValue > CGoldMineThreshold && !IsInOuterZone(x, y) && !isNearbyObstacle(x, y))
                    {
                        Grid[y][x] = (Unit)goldMinePrototype.ShallowCopy();
                        Grid[y][x].SetCoordinates(x, y);
                        //----------Testavimui
                        //Console.WriteLine("Gold Mine original address " + goldMinePrototype.GetHashCode() + " Gold Mine copy address " + Grid[y][x].GetHashCode());
                        //Console.WriteLine("Gold Mine Player original address " + goldMinePrototype.GetPlayer().GetHashCode() + " Gold Mine Player copy address " + Grid[y][x].GetPlayer().GetHashCode());
                        //----------
                    }
                    else if (obstacleValue > CStoneThreshold && !IsInOuterZone(x, y) && !isNearbyObstacle(x, y))
                    {
                        //----------Testavimui
                        //Grid[y][x] = (Unit)stonePrototype.DeepCopy();
                        //----------
                        Grid[y][x] = (Unit)stonePrototype.ShallowCopy();
                        Grid[y][x].SetCoordinates(x, y);
                        //----------Testavimui
                        //Console.WriteLine("Stone original address " + stonePrototype.GetHashCode() + " Stone copy address " + Grid[y][x].GetHashCode());
                        //Console.WriteLine("Stone Player original address " + stonePrototype.GetPlayer().GetHashCode() + " Stone Player copy address " + Grid[y][x].GetPlayer().GetHashCode());
                        //----------
                    }
                    else
                        Grid[y][x] = new Unit(x, y);
                }
            }
            Console.WriteLine("========");
            return Grid;
        }

        private bool IsInOuterZone(int x, int y)
        {
            return x == 0 || y == 0 || x == (GetXSize() - 1) || y == (GetYSize() - 1);
        }

        private bool isNearbyObstacle(int x, int y)
        {
            bool upperLeft = false, upper = false, upperRight = false, left = false;
            if (y > 0 && x > 0)
                upperLeft = Grid[y - 1][x - 1] is GoldMine || Grid[y - 1][x - 1] is Wonder || Grid[y - 1][x - 1] is Stone;
            if (y > 0)
                upper = Grid[y - 1][x] is GoldMine || Grid[y - 1][x] is Wonder || Grid[y - 1][x] is Stone;
            if (y > 0 && x < GetXSize() - 1)
                upperRight = Grid[y - 1][x + 1] is GoldMine || Grid[y - 1][x + 1] is Wonder || Grid[y - 1][x + 1] is Stone;
            if (x > 0)
                left = Grid[y][x - 1] is GoldMine || Grid[y][x - 1] is Wonder || Grid[y][x - 1] is Stone;
            return upperLeft || upper || upperRight || left;

        }

        public List<Unit> ConvertArrayToList()
        {
            List<Unit> unitList = new List<Unit>();
            for (int y = 0; y < GetYSize(); y++)
            {
                for (int x = 0; x < GetXSize(); x++)
                {
                    unitList.Add(GetUnit(x, y));
                }
            }
            return unitList;
        }

        public void ConvertListToArray(List<Unit> unitList)
        {
            for (int y = 0; y < GetYSize(); y++)
            {
                for (int x = 0; x < GetXSize(); x++)
                {
                    Grid[y][x] = unitList[y * GetXSize() + x];
                }
            }
        }

        public int GetYSize()
        {
            return Grid.Length;
        }

        public int GetXSize()
        {
            return Grid[0].Length;
        }

        public Unit GetUnit(int x, int y)
        {
            return Grid[y][x];
        }
        public Unit[][] GetGrid()
        {
            return Grid;
        }


    }
}
