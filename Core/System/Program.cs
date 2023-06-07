using Nocturnal.Core.Entitites.Living;

namespace Nocturnal.Core.System;

public class Program
{
    public static Game? Game { get; private set; }

    static void Main()
    {
        Game = new Game();

        if (GameSettings.LoadConfigFile())
        {
            Game.Run();
        }
    }
}
