using System.IO;

namespace Console.IO
{
    public interface IFileHelper
    {
        Stream GetFileContents(string path);
        string[] GetInventoryFilePaths(string sourcePath);
    }
}
