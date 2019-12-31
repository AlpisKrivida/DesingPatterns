using System;
using System.Collections.Generic;
using System.Text;
using Lab1_1.Proxy;
using Newtonsoft.Json;

namespace Lab1_1.Adapter
{
    class MapJson
    {
        private string mapJson;
        public List<Unit> ConvertJsonToList()
        {
            List<Unit> unitList = JsonConvert.DeserializeObject<List<Unit>>(mapJson);
            return unitList;
        }
        public void ConvertListToJson(List<Unit> unitList)
        {
            mapJson = JsonConvert.SerializeObject(unitList);
        }
    }
}
