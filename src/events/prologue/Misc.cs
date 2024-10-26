using Nocturnal.core;
using Nocturnal.core.utils;
using Nocturnal.services;
using Nocturnal.ui;

namespace Nocturnal.events.prologue;

public static class MiscEvents
{
    public static async Task LoadingFiles()
    {
        Console.ForegroundColor = ConsoleColor.White;
        await Display.Write("\t||", 100);

        for (var i = 0; i < 21; i++)
        {
            await Task.Delay(1000);
            await Display.Write("=", 100);
        }

        await Display.Write("||", 100);
        await Task.Delay(1000);
        await Display.Write($"\n\t{Localizator.GetString("MISC.DOWNLOAD_COMPLETED")}");
        Console.ResetColor();
    }

    public static async Task NamingHero()
    {
        await Display.WriteNarration($"\t{Localizator.GetString("MISC.REMEMBER_YOUR_NAME")}\n");
        Console.ResetColor();
        Globals.Player.Name = await Input.GetString();
        await SaveService.UpdateSave();
    }
}