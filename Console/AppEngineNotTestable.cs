using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Console.Model;
using System.Xml.Serialization;

namespace Console
{
    public class AppEngineNotTestable
    {
        public void Run()
        {
            var files = Directory
                .GetFiles(".", "*.*")
                .Where(file => file.ToLower().EndsWith("csv") || file.ToLower().EndsWith("xml"))
                .ToArray();

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file);
                var items = new List<InventoryItem>();
                switch (extension)
                {
                    case ".csv":
                        using (var reader = new StreamReader(File.OpenRead(file)))
                        {
                            var rowIndex = 0;
                            while (!reader.EndOfStream)
                            {
                                var row = reader.ReadLine();

                                if (rowIndex == 0 || row == null)
                                {
                                    rowIndex++;
                                    continue;
                                }

                                var name = row.Split(',')[0];
                                var quantity = row.Split(',')[1];

                                items.Add(new InventoryItem
                                {
                                    Name = name,
                                    Quantity = Convert.ToInt32(quantity)
                                });

                                rowIndex++;
                            }
                        }
                        break;
                    case ".xml":
                        {
                            var xml = File.ReadAllText(file);

                            var ns = new XmlSerializerNamespaces();
                            var serializer = new XmlSerializer(typeof(Inventory), "");

                            ns.Add("", "");
                            var output = (Inventory)serializer.Deserialize(new StringReader(xml));

                            items = output.Items;
                        }
                        break;
                }

                System.Console.WriteLine("Source file: {0}", file);
                if (items == null)
                {
                    System.Console.WriteLine("No items.");
                    continue;
                }

                foreach (var item in items)
                {
                    System.Console.WriteLine("Item: {0}, Quantity: {1}", item.Name, item.Quantity);
                }
            }
            System.Console.WriteLine("Done!");
        }
    }
}
