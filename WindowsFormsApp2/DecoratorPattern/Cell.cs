using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WindowsFormsApp2.DecoratorPattern
{
    public class Cell : IBrush
    {
        public TextureBrush tb;

        public void SetBrush(TextureBrush _tb) { tb = _tb; }

        public TextureBrush GetBrush()
        {
            return this.tb;
        }

    }
}
