using Lab1_1.CommandPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.CommandCommandPattern
{
    public class ConcreateCommand : Command
    {

        private Map _map;
        private string _commandDirection;
        private Player _player;

        public ConcreateCommand(string commandDirection, Player player)
        {
            _player = player;
            _commandDirection = commandDirection;
        }
        // Execute new command
        public override void Execute()
        {
            _player.move(_player, _commandDirection, Map.GetInstance, false);
        }

        // Unexecute last command
        public override void UnExecute()
        {
            _player.move(_player, ReverseDirection(_commandDirection), Map.GetInstance, true);
        }

        private string ReverseDirection(string direction)
        {
            switch (direction)
            {
                case "U":
                    return "D";
                case "D":
                    return "U";
                case "L":
                    return "R";
                case "R":
                    return "L";
                default: return direction;
            }
        }
    }
}
