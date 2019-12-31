using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1
{
    public class HardWorker : Player
    {
        public HardWorker(string faction) : base(faction)
        {
            Money = 10;
            NumberOfActions = 8;
        }

        public void WorkHarder()
        {
            MoneyMultiplier = 2;
            NumberOfActions = 6;
        }
        public void GetBackToNormal()
        {
            MoneyMultiplier = 1;
            NumberOfActions = 8;
        }
    }
}
