using Nocturnal.src.core;
using Nocturnal.src.interfaces;

namespace Nocturnal.src.services
{
    public class ConsoleService : IConsoleInitiator
    {
        public static void InitConsole()
            => Console.Title = $"{Constants.GAME_NAME} {Constants.GAME_VERSION}";
    }
}
