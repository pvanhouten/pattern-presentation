using System.IO;
using System.Linq;

namespace Console.IO
{
    public class FileHelper : IFileHelper
    {
        /// <summary>
        /// Returns a stream with the contents of the file path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Stream GetFileContents(string path)
        {
            return File.Exists(path) ? File.OpenRead(path) : null;
        }

        /// <summary>
        /// Gets a list of the inventory files in the current path
        /// </summary>
        /// <returns></returns>
        public string[] GetInventoryFilePaths(string sourcePath)
        {
            return Directory
                .GetFiles(sourcePath, "*.*")
                .Where(file => file.ToLower().EndsWith("csv") || file.ToLower().EndsWith("xml"))
                .ToArray();
        }
    }
}
