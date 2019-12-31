using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Proxy
{
    public interface MapDataConvert
    {
        List<Unit> ConvertArrayToList();
        void ConvertListToArray(List<Unit> unitList);
    }
}
