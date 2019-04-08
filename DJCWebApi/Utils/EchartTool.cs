namespace DJCWebApi.Utils
{
    using PI.Core.DA;
    using System;
    using System.Collections.Generic;

    public class EchartTool
    {
        public static DBData Convert(List<DBData> datas, string colName, params string[] valueName)
        {
            DBData data = new DBData();
            List<string> list = new List<string>();
            List<object> list2 = new List<object>();
            foreach (DBData data2 in datas)
            {
                list.Add(data2.getValue(colName).ToString());
            }
            data.Add(colName, list);
            foreach (string str in valueName)
            {
                list2 = new List<object>();
                foreach (DBData data3 in datas)
                {
                    list2.Add(data3.getValue(str).ToString());
                }
                data.Add(str, list2);
            }
            return data;
        }
    }
}

