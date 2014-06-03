using Console;
using Console.Adapters;
using Console.IO;
using Console.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using Console.Model;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class AppEngineTests
    {
        private AppEngineTestable _engine;
        private Mock<IAdapterFactory> _factory;
        private Mock<IFileHelper> _fileHelper;
        private Mock<IOutputAdapter> _output;

        [TestInitialize]
        public void Init()
        {
            _factory = new Mock<IAdapterFactory>();
            _fileHelper = new Mock<IFileHelper>();
            _output = new Mock<IOutputAdapter>();
            _engine = new AppEngineTestable(_factory.Object, _fileHelper.Object, _output.Object);
        }

        [TestMethod]
        public void No_items_writes_special_message_to_output()
        {
            // ARRANGE
            var adapter = new Mock<IInventoryFileAdapter>();
            // tell the file helper to return a specific filename
            _fileHelper.Setup(m => m.GetInventoryFilePaths(".")).Returns(new[] { ".\\test.csv" });

            // tell the factory to return our mock file adapter
            _factory.Setup(m => m.GetAdapter(".csv")).Returns(adapter.Object);

            // set up our expectations for the output that we're going to validate in the assertions
            _output.Setup(m => m.WriteMessage("No items."));

            // tell our fake adapter to return our null inventory item list
            adapter.Setup(m => m.GetInventoryItems(It.IsAny<Stream>())).Returns((List<InventoryItem>)null);


            // ACT
            _engine.Run();

            // ASSERT
            _output.Verify(m => m.WriteMessage("No items."));
        }

        [TestMethod]
        public void Item_is_written_to_output()
        {
            // ARRANGE
            var adapter = new Mock<IInventoryFileAdapter>();
            // tell the file helper to return a specific filename
            _fileHelper.Setup(m => m.GetInventoryFilePaths(".")).Returns(new[] { ".\\test.csv" });

            // tell the factory to return our mock file adapter
            _factory.Setup(m => m.GetAdapter(".csv")).Returns(adapter.Object);

            // tell our fake adapter to return our null inventory item list
            adapter.Setup(m => m.GetInventoryItems(It.IsAny<Stream>())).Returns(new List<InventoryItem>
            {
                new InventoryItem { Name = "Fancy Cheese", Quantity = 2 },
                new InventoryItem { Name = "Moldy Cheese", Quantity = 1 }
            });

            // set up our expectations for the output that we're going to validate in the assertions
            _output.Setup(m => m.WriteMessage("Item: Fancy Cheese, Quantity: 2"));
            _output.Setup(m => m.WriteMessage("Item: Moldy Cheese, Quantity: 1"));


            // ACT
            _engine.Run();

            // ASSERT
            _output.VerifyAll();
        }
    }
}
