using System.Collections.Generic;
using System.Xml.Serialization;

namespace Console.Model
{
    [XmlType("inventory")]
    public class Inventory
    {
        public Inventory()
        {
            Items = new List<InventoryItem>();
        }

        [XmlArray("items")]
        [XmlArrayItem("item")]
        public List<InventoryItem> Items { get; set; }
    }
}
