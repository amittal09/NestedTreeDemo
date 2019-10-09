using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NestedTreeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument document = XDocument.Load($"{AppDomain.CurrentDomain.BaseDirectory}\\labels.xml");
            LabelXMLFile labelXMLFile = new LabelXMLFile();
            var data = labelXMLFile.ReadFile();
            WriteToJson(data);
        }
        public static void WriteToJson(List<IGrouping<string, XmlElement>> data)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{AppDomain.CurrentDomain.BaseDirectory}\\Writejson.json"))
            {
                foreach (var item in data)
                {
                    file.WriteLine("{");
                    file.WriteLine(@" ""resourceKey"" :  """ + item.Key + @"""");
                    //file.WriteLine(@" ""id"" :  """ + resourceKey + @"""");
                    // file.WriteLine(@" ""modificationDate"" :  """ + DateTime.Now.ToString() + @"""");
                    file.WriteLine(@" ""translations"" : [ ");

                    foreach (var item1 in item)
                    {
                        file.WriteLine("{");
                        //file.WriteLine(@" ""id"" :  """ + languageKey++ + @"""");
                        file.WriteLine(@" ""language"" :  """ + item1.Language + @"""");
                        //file.WriteLine(@" ""resourceId"" :  """ + resourceKey + @"""");
                        file.WriteLine(@" ""value"" :  """ + item1.Value.Trim() + @"""");
                        file.WriteLine("},");
                    }
                    file.WriteLine("]");
                    file.WriteLine("},");
                }
            }

    }
}

}
