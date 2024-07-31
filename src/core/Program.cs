using Nocturnal.src.core.utilities;
using Nocturnal.src.services;

namespace Nocturnal.src.core
{
    public class Program
    {
        public static Game? Game { get; private set; }

        static async Task Main()
        {
            await Logger.WriteLog("Program runs");
            Game = Game.Instance;
            if (!await ConfigService.LoadConfigFile()) return;
            await Game.Run();
        }
    }
}