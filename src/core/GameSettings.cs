using Nocturnal.src.ui;

namespace Nocturnal.src.core
{
    public enum GameLanguages { EN = 1, PL = 2 }

    public class GameSettings
    {
        public static int Lang { get; private set; }

        public GameSettings() { Lang = 0; }

        public static int SelectLanguage()
        {
            int choice = 0;

            Console.ResetColor();
            Console.Clear();

            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>()
            {
                { "English", async () => { choice = (int)GameLanguages.EN; await Task.CompletedTask; } },
                { "Polski", async () => { choice = (int)GameLanguages.PL; await Task.CompletedTask; } },
            });

            return choice;
        }

        public static void SetLanguage(GameLanguages lang)
        {
            Lang = (int)lang;
        }

        public static bool IsSetLanguage(GameLanguages lang)
        {
            return Lang == (uint)lang;
        }
    }
}
