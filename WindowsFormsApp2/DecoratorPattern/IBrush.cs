using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WindowsFormsApp2.DecoratorPattern
{
    public interface IBrush
    {
        void SetBrush(TextureBrush _tb);
        TextureBrush GetBrush();
    }
}
