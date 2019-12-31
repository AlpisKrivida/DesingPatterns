using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WindowsFormsApp2.DecoratorPattern
{
    public class YellowDot:Decorator
    {

        public YellowDot(Cell c): base(c) {
            Image image = new Bitmap("Images/yellowdot.png");
            TextureBrush tBrush = new TextureBrush(image);
            SetBrush(tBrush);
        }

        public TextureBrush GetBrush()
        {
            return _cell.GetBrush();
        }
    }
}
