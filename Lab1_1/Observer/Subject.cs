using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_1.Observer
{
    public class Subject
    {
        public List<AObserver> observers = new List<AObserver>();

        public void Attach(AObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(AObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(Map map, (int, int) cords, List<Unit> area)
        {
            foreach(AObserver o in observers)
            {
                o.Update(map, cords, area);
            }
        }
    }
}
