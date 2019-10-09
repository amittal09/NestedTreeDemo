using System;
using System.Xml.Linq;

namespace NestedTreeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument document = XDocument.Load($"{AppDomain.CurrentDomain.BaseDirectory}\\labels.xml");
            LabelXMLFile labelXMLFile = new LabelXMLFile();
            labelXMLFile.ReadFile();
        }
    }
}
