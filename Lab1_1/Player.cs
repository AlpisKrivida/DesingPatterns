using Lab1_1.CommandCommandPattern;
using Lab1_1.CommandPattern;
using Lab1_1.Streategy;
using Lab1_1.MementoPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class Player : MovementStrategy
    {
        public long id { get; set; }
        public int Money { get; set; }
        public int MoneyMultiplier { get; set; }
        public int NumberOfActions { get; set; }
        public string Name { get; set; }
        public string Faction { get; set; }
        public int Power { get; set; }

        private int previousX { get; set; }
        private int previousY { get; set; }

        private char symbol = '*';
        public ConsoleColor color { get; set; }
        public int currentX { get; set; }
        public int currentY { get; set; }



        private List<Command> _commands = new List<Command>();
        private int _curentMoveNumber = 0;

        public Player(string faction) :base("")
        {
            currentX = 0;
            currentY = 0;
            MoneyMultiplier = 1;
            Power = 1;
            Money = 0;
            NumberOfActions = 7;


            Faction = faction;
            color = ConsoleColor.Yellow;
        }
        public Player() : base("")
        {
            color = ConsoleColor.Yellow;
        }

        public void SetName(string name)
        {
            Name = name;
        }
        public void Creation()
        {
            Console.WriteLine("You chose to be a " + Faction);
        }
        public ConsoleColor GetColor()
        {
            return color;
        }
        public char GetSymbol()
        {
            return symbol;
        }
        public void SetPreviousCoordinates(int x, int y)
        {
            previousX = x;
            previousY = y;
        }
        public void ResetCommands()
        {
            _commands.Clear();
            _curentMoveNumber = 0;
        }
        public int GetPreviousX()
        {
            return previousX;
        }
        public int GetPreviousY()
        {
            return previousY;
        }

        public void Undo()
        {
            Console.WriteLine("Undoing your moves");

            for (int i = 0; i < NumberOfActions; i++)
            {
                if(_curentMoveNumber > 0)
                {
                    Command command = _commands[--_curentMoveNumber] as Command;
                    command.UnExecute();
                }
            }

            if (getAlgorithm() is Teleport)
            {
                Map.GetInstance.GetUnit(((Teleport)getAlgorithm()).GetStartingX(), ((Teleport)getAlgorithm()).GetStartingY()).symbol = symbol;
                currentX = ((Teleport)getAlgorithm()).GetStartingX();
                currentY = ((Teleport)getAlgorithm()).GetStartingY();
            }
            else
            {
                Map.GetInstance.GetUnit(currentX, currentY).symbol = symbol;
                Map.GetInstance.GetUnit(currentX, currentY).color = color;
            }

        }
        public void ExecuteMove(string commandDirection, Player player)
        {
            Command command = new ConcreateCommand(commandDirection, player);

            command.Execute();

            _commands.Add(command);
            _curentMoveNumber++;
        }

        public Player Clone()
        {
            return (Player)this.MemberwiseClone();
        }

        // Stores memento
        public Memento SaveMemento()
        {
            Console.WriteLine("\nSaving state --\n");
            return new Memento(Money, Name, Faction, previousX, previousY, currentX, currentY);
        }

        // Restores memento
        public void RestoreMemento(Memento memento)
        {
            Console.WriteLine("\nRestoring state --\n");
            this.Money = memento.Money;
            this.Name = memento.Name;
            this.Faction = memento.Faction;
            this.previousX = memento.PreviousX;
            this.previousY = memento.PreviousY;
            this.currentX = memento.CurrentX;
            this.currentY = memento.CurrentY;
        }


    }
    public class gamePlayer : Player
    {

    }
}
