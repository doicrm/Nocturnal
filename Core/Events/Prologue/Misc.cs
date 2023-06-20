using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue
{
    public static class MiscEvents
    {
        public static void LoadingFiles()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Display.Write("\t||", 100);

            for (int i = 0; i < 21; i++)
            {
                Thread.Sleep(1000);
                Display.Write("=", 100);
            }

            Display.Write("||", 100);
            Thread.Sleep(1000);
            Display.Write($"\n\t{Globals.JsonReader!["MISC.DOWNLOAD_COMPLETED"]}");
            Console.ResetColor();
        }

        public static void NamingHero()
        {
            Display.WriteNarration($"\t{Globals.JsonReader!["MISC.REMEMBER_YOUR_NAME"]}\n");
            Console.ResetColor();
            Globals.Player.Name = Input.GetString();
            SaveManager.UpdateSave();
        }
    }
}
