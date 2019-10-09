using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NestedTreeDemo
{
    public class LabelXMLFile
    {
        public void ReadFile()
        {
            XDocument document = XDocument.Load($"{AppDomain.CurrentDomain.BaseDirectory}\\labels.xml");
            List<XmlElement> elements = new List<XmlElement>();
            var parents = document.Root.Elements();
            foreach (var currentElement in parents)
            {
                var current = new XmlElement { Name = currentElement.Name.LocalName, Value = currentElement.Attribute("id").Value, Language = currentElement.Attribute("id").Value };
                GetChildElements(currentElement, current);
                elements.Add(current);
            }
            var data1 = elements.Flatten(x => x.ChildElement ?? new List<XmlElement>()).Select(z => new XmlElement { ResourceKey = z.ResourceKey, Value = z.Value, Language = z.Language }).ToList();
            var data = elements.Flatten(x => x.ChildElement ?? new List<XmlElement>()).Select(z => new XmlElement { ResourceKey = z.ResourceKey, Value = z.Value, Language = z.Language }).Where(x => x.Value != null).GroupBy(x => x.ResourceKey).ToList();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in data)
            {
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine(item.Key);
                stringBuilder.AppendLine(":");
                stringBuilder.AppendLine("{");
                foreach (var item1 in item)
                {
                    stringBuilder.AppendLine($"{nameof(item1.ResourceKey)}:{item1.ResourceKey}");
                    stringBuilder.AppendLine($"{nameof(item1.Language)}:{item1.Language}");
                    stringBuilder.AppendLine($"{nameof(item1.Value)}:{item1.Value}");
                }
            }
            stringBuilder.Append("}");
            Console.WriteLine(stringBuilder);
            Console.ReadLine();


            Console.Read();
        }
        public void GetChildElements(XElement element, XmlElement currentElementObjcet)
        {
            var childelements = element.Elements();
            if (childelements.Count() != 0)
            {
                currentElementObjcet.ChildElement = new List<XmlElement>();
                foreach (var currentElement in childelements)
                {
                    var key = currentElementObjcet.ResourceKey == string.Empty ? string.Empty : $"{ currentElementObjcet.ResourceKey}.";
                    var child = new XmlElement { Name = currentElement.Name.LocalName, ResourceKey = $"{key}{currentElement.Name.LocalName}", Language = currentElementObjcet.Language };
                    currentElementObjcet.ChildElement.Add(child);
                    GetChildElements(currentElement, child);
                }
            }
            else
            {
                currentElementObjcet.Value = element.Value;
            }

        }


    }
    public class XmlElement
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Language { get; set; }
        public string ResourceKey { get; set; } = "";
        public List<XmlElement> ChildElement { get; set; }
    }
}
