namespace Console.Adapters
{
    public class AdapterFactory : IAdapterFactory
    {
        public IInventoryFileAdapter GetAdapter(string fileExtension)
        {
            // REVIEW: LSP example
            // REVIEW: OCP violation
            switch (fileExtension)
            {
                case ".csv":
                    return new CsvAdapter();
                case ".xml":
                    return new XmlAdapter();
            }
            return null;
        }
    }
}
