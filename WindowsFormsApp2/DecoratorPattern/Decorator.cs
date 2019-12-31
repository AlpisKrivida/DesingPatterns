using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WindowsFormsApp2.DecoratorPattern
{
    public class Decorator : IBrush
    {
        public IBrush _cell;
        public Decorator(IBrush c) {
            _cell = c;
        }
        public void SetBrush(TextureBrush _tb) { _cell.SetBrush(_tb); }
        public TextureBrush GetBrush() { return _cell.GetBrush(); }
    }
}
