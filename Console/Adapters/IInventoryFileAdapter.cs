using Console.Model;
using System.Collections.Generic;
using System.IO;

namespace Console.Adapters
{
    public interface IInventoryFileAdapter
    {
        List<InventoryItem> GetInventoryItems(Stream inputStream);
    }
}
