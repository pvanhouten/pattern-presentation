namespace Console.Adapters
{
    public interface IAdapterFactory
    {
        IInventoryFileAdapter GetAdapter(string fileExtension);
    }
}
