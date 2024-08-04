using Nocturnal.src.core;
using Nocturnal.src.core.utils;
using Nocturnal.src.services;
using Nocturnal.src.ui;

namespace Nocturnal.src.events.prologue
{
    public static class MiscEvents
    {
        public static async Task LoadingFiles()
        {
            Console.ForegroundColor = ConsoleColor.White;
            await Display.Write("\t||", 100);

            for (int i = 0; i < 21; i++)
            {
                await Task.Delay(1000);
                await Display.Write("=", 100);
            }

            await Display.Write("||", 100);
            await Task.Delay(1000);
            await Display.Write($"\n\t{LocalizationService.GetString("MISC.DOWNLOAD_COMPLETED")}");
            Console.ResetColor();
        }

        public static async Task NamingHero()
        {
            await Display.WriteNarration($"\t{LocalizationService.GetString("MISC.REMEMBER_YOUR_NAME")}\n");
            Console.ResetColor();
            Globals.Player.Name = await Input.GetString();
            await SaveService.UpdateSave();
        }
    }
}
