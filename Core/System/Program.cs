using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.System
{
    public class Program
    {
        public static Game? Game { get; private set; }

        static void Main()
        {
            Logger.WriteLog("Program runs");
            Game = Game.Instance;

            if (GameSettings.LoadConfigFile())
            {
                Game.Run();
            }
        }
    }
}
