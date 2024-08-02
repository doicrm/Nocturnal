using Nocturnal.src.core.utilities;
using Nocturnal.src.services;

namespace Nocturnal.src.core
{
    public class Program
    {
        static async Task Main()
        {
            await Logger.WriteLog("Program runs");

            if (await ConfigService.LoadConfigFile())
            {
                await Game.Instance.Run();
            }
        }
    }
}