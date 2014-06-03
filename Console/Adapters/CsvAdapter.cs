using Console.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace Console.Adapters
{
    public class CsvAdapter : IInventoryFileAdapter
    {
        // REVIEW: SRP example
        public List<InventoryItem> GetInventoryItems(Stream inputStream)
        {
            var items = new List<InventoryItem>();

            using (var reader = new StreamReader(inputStream))
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

            return items;
        }
    }
}
