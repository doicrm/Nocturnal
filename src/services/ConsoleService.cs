using Nocturnal.src.core;

namespace Nocturnal.src.services
{
    public class ConsoleService
    {
        public static void ChangeConsoleName()
            => Console.Title = $"{Constants.GAME_NAME} {Constants.GAME_VERSION}";
    }
}
