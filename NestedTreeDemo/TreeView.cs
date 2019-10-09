using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NestedTreeDemo
{
    public class LabelXMLFile
    {
        public List<IGrouping<string, XmlElement>> ReadFile()
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
            var data = elements.Flatten(x => x.ChildElement ?? new List<XmlElement>()).Select(z => new XmlElement { ResourceKey = z.ResourceKey, Value = z.Value, Language = z.Language }).Where(x => x.Value != null).GroupBy(x => x.ResourceKey).ToList();
            return data;
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
    
}
