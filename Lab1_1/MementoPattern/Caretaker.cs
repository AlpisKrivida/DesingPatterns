using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.MementoPattern
{
    public class Caretaker
    {
        private Memento _memento;

        public Memento Memento
        {
            set { _memento = value; }
            get { return _memento; }
        }
    }
}
