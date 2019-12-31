using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Observer
{
    public abstract class AObserver
    {
        public abstract void Update(Map map, (int,int) cords, List<Unit> area);
    }
}
