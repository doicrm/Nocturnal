using Nocturnal.src.core;

namespace Nocturnal.src.services
{
    public class ConsoleService
    {
        public static void InitConsole()
            => Console.Title = $"{Constants.GAME_NAME} {Constants.GAME_VERSION}";
    }
}
