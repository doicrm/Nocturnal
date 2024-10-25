using Nocturnal.core.utils;
using Nocturnal.services;

namespace Nocturnal.core
{
    public static class Program
    {
        private static async Task Main()
        {
            await Logger.WriteLog("Program runs");

            if (await ConfigService.LoadConfigFile()) {
                await Game.Instance.Run();
            }
        }
    }
}