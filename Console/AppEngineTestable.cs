using System.IO;
using Console.Adapters;
using Console.IO;
using Console.Output;

namespace Console
{
    public class AppEngineTestable
    {
        private readonly IAdapterFactory _factory;
        private readonly IFileHelper _helper;
        private readonly IOutputAdapter _output;

        // DIP: all dependencies are passed via constructor injection
        public AppEngineTestable(IAdapterFactory factory, IFileHelper helper, IOutputAdapter output)
        {
            _factory = factory;
            _helper = helper;
            _output = output;
        }

        public void Run()
        {
            foreach (var file in _helper.GetInventoryFilePaths("."))
            {
                var extension = Path.GetExtension(file);
                var adapter = _factory.GetAdapter(extension);
                var items = adapter.GetInventoryItems(_helper.GetFileContents(file));

                _output.WriteMessage(string.Format("Source file: {0}", file));
                if (items == null)
                {
                    _output.WriteMessage("No items.");
                    continue;
                }

                foreach (var item in items)
                {
                    _output.WriteMessage(string.Format("Item: {0}, Quantity: {1}", item.Name, item.Quantity));
                }
            }
            _output.WriteMessage("Done!");
        }
    }
}
