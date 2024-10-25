using Nocturnal.core;
using Nocturnal.interfaces;

namespace Nocturnal.services
{
    public abstract class ConsoleService : IConsoleInitiator
    {
        public static void InitConsole()
            => Console.Title = $"{Constants.GameName} {Constants.GameVersion}";
    }
}
