namespace Nocturnal.src;

public class Program
{
    public static Game? Game { get; private set; }

    static void Main()
    {
        Console.Title = $"{Constants.DEFAULT_GAME_NAME} {Constants.DEFAULT_DEMO_VERSION}";
        Game = new Game();

        if (GameSettings.LoadConfigFile())
        {
            Game.Run();
        }
    }
}
