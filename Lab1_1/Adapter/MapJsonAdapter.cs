using Lab1_1.Proxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Adapter
{
    public class MapJsonAdapter : MapDataConvert
    {
        MapJson map = new MapJson();
        public List<Unit> ConvertArrayToList()
        {
            return map.ConvertJsonToList();
        }
        public void ConvertListToArray(List<Unit> unitList)
        {
            map.ConvertListToJson(unitList);
        }
    }
}
