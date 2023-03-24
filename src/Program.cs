namespace Nocturnal.src
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "Nocturnal [Demo Build]";
            GameSettings config = new();

            if (config.LoadConfigFile())
            {
                Globals.Game.Run();
            }
        }
    }
}
