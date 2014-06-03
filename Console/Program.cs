using Console.Adapters;
using Console.IO;
using Console.Output;

namespace Console
{
    class Program
    {
        static void Main()
        {
            new AppEngineTestable(new AdapterFactory(), new FileHelper(), new ConsoleAdapter()).Run();
            new AppEngineNotTestable().Run();
        }
    }
}
