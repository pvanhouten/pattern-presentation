using System.Xml.Serialization;

namespace Console.Model
{
    [XmlType("item")]
    public class InventoryItem
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("quantity")]
        public int Quantity { get; set; }
    }
}
