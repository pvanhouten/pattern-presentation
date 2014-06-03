using System.IO;
using Console.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;

namespace UnitTests.Adapters
{
    [TestClass]
    public class CsvAdapterTests
    {
        private const string EmptyFile = "";
        private const string FilledFile =
@"name,quantity
bat,1
ball,2
";
        private const string InvalidQuantityFile =
@"name,quantity
bat,a
ball,z
";

        private CsvAdapter _adapter;

        [TestInitialize]
        public void Init()
        {
            _adapter = new CsvAdapter();
        }

        [TestMethod]
        public void Empty_file_returns_empty_list_of_items()
        {
            // arrange
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(EmptyFile));

            // act
            var output = _adapter.GetInventoryItems(stream);

            // assert
            Assert.AreEqual(0, output.Count);
        }

        [TestMethod]
        public void Filled_file_returns_list_of_items()
        {
            // arrange
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(FilledFile));

            // act
            var output = _adapter.GetInventoryItems(stream);

            // assert
            Assert.AreEqual(2, output.Count);
            Assert.AreEqual(output.ElementAt(0).Name, "bat");
            Assert.AreEqual(output.ElementAt(0).Quantity, 1);
            Assert.AreEqual(output.ElementAt(1).Name, "ball");
            Assert.AreEqual(output.ElementAt(1).Quantity, 2);
        }

        [TestMethod]
        public void Invalid_quantities_are_treated_as_zero()
        {
            // TODO: fix this test
            // arrange
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(InvalidQuantityFile));

            // act
            var output = _adapter.GetInventoryItems(stream);

            // assert
            Assert.AreEqual(2, output.Count);
            Assert.AreEqual(output.ElementAt(0).Name, "bat");
            Assert.AreEqual(output.ElementAt(0).Quantity, 0);
            Assert.AreEqual(output.ElementAt(1).Name, "ball");
            Assert.AreEqual(output.ElementAt(1).Quantity, 0);
        }
    }
}
