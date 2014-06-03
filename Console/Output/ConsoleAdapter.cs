namespace Console.Output
{
    public class ConsoleAdapter : IOutputAdapter
    {
        public void WriteMessage(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}
