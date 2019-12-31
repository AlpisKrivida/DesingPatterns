using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.MementoPattern
{
    public class Memento
    {
        private int _money;
        private string _name;
        private string _faction;
        private int _previousX;
        private int _previousY;
        private int _currentX;
        private int _currentY;

        public Memento(int money, string name, string faction, int previousX,
            int previousY, int currentX, int currentY)
        {
            this._money = money;
            this._name = name;
            this._faction = faction;
            this._previousX = previousX;
            this._previousY = previousY;
            this._currentX = currentX;
            this._currentY = currentY;
        }
        public int Money
        {
            get { return _money; }
            set { _money = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Faction
        {
            get { return _faction; }
            set { _faction = value; }
        }
        public int PreviousX
        {
            get { return _previousX; }
            set { _previousX = value; }
        }
        public int PreviousY
        {
            get { return _previousY; }
            set { _previousY = value; }
        }
        public int CurrentX
        {
            get { return _currentX; }
            set { _currentX = value; }
        }
        public int CurrentY
        {
            get { return _currentY; }
            set { _currentY = value; }
        }
    }
}
