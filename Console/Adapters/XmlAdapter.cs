using Console.Model;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Console.Adapters
{
    public class XmlAdapter : IInventoryFileAdapter
    {
        public List<InventoryItem> GetInventoryItems(Stream inputStream)
        {
            string xml;
            using (var reader = new StreamReader(inputStream))
            {
                xml = reader.ReadToEnd();
            }

            var ns = new XmlSerializerNamespaces();
            var serializer = new XmlSerializer(typeof(Inventory), "");

            ns.Add("", "");
            var output = (Inventory)serializer.Deserialize(new StringReader(xml));

            return output.Items;
        }
    }
}
