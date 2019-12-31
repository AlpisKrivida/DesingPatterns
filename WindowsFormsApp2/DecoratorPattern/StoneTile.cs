using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WindowsFormsApp2.DecoratorPattern
{
    class StoneTile: Decorator
    {
        public StoneTile(Cell c) : base(c) { 
            Image image = new Bitmap("Images/rock.png");
            TextureBrush tBrush = new TextureBrush(image);
            SetBrush(tBrush);
        }

        public TextureBrush GetBrush()
        {
            return _cell.GetBrush();
        }
    }
}
