using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.System
{
    public class Program
    {
        public static Game? Game { get; private set; }

        static async Task Main()
        {
            await Logger.WriteLog("Program runs");
            Game = Game.Instance;
            if (!await GameSettings.LoadConfigFile()) return;
            await Game.Run();
        }
    }
}