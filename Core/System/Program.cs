namespace Nocturnal.Core.System
{
    public class Program
    {
        public static Game? Game { get; private set; }

        static void Main()
        {
            Game = Game.Instance;

            if (GameSettings.LoadConfigFile())
            {
                Game.Run();
            }
        }
    }
}
