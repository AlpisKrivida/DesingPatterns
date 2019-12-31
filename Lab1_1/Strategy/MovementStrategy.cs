using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Streategy
{
    public abstract class MovementStrategy
    {
        private Algorithm _algorithm;
        private string _algorithmType;
        public MovementStrategy(string selectedType)
        {
            _algorithmType = selectedType;
            AlgorithmFactory factory = new AlgorithmFactory();
            Algorithm standart = factory.GetDefault(selectedType);
            setAlgorithm(standart);
        }

        public void move(Player player, string command, Map map, bool undo)
        {
            Console.WriteLine("Strategy pattern algorithm is being used--------------");
            _algorithm.Action(player, command, map, undo);
        }
        public void setAlgorithm(Algorithm newAlgorithm)
        {
            Console.Write("\r New Strategy algorithm was set--------------------");
            _algorithm = newAlgorithm;
        }
        public Algorithm getAlgorithm()
        {
            return _algorithm;
        }
    }
}
