using Lab1_1.Adapter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Proxy
{
    class Proxy : MapDataConvert
    {
        public List<Unit> ConvertArrayToList()
        {
            MapJson map = new MapJson();
            return map.ConvertJsonToList();
        }
        public void ConvertListToArray(List<Unit> unitList)
        {
            MapJson map = new MapJson();
            map.ConvertListToJson(unitList);
        }
    }
}
