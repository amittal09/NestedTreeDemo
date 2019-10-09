using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedTreeDemo
{
    public class XmlElement
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Language { get; set; }
        public string ResourceKey { get; set; } = "";
        public List<XmlElement> ChildElement { get; set; }
    }
}
