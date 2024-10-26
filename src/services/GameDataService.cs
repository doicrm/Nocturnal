using Nocturnal.core;
using Nocturnal.entitites;

namespace Nocturnal.services;

public abstract class GameDataService
{
    public static async Task InitAll()
    {
        await InitHeroInventory();
        await InitHeroJournal();
        InsertInstances();
    }

    private static void InsertInstances()
    {
        Npc.InsertInstances();
        Item.InsertInstances();
        Fraction.InsertInstances();
        Quest.InsertInstances();
        Location.InsertInstances();
    }

    public static async Task InitHeroInventory()
    {
        var inventory = Globals.Player.Inventory;

        if (inventory != null && !inventory.IsEmpty())
        {
            await inventory.UpdateFile();
            return;
        }

        await WriteToFile("Inventory.txt", Localizator.GetString("INVENTORY.NO_ITEMS"));
    }

    public static async Task InitHeroJournal()
    {
        var journal = Globals.Player.Journal;

        if (journal != null && !journal.IsEmpty())
        {
            await journal.UpdatedJournalFile().ConfigureAwait(false);
            return;
        }

        await WriteToFile("Journal.txt", Localizator.GetString("JOURNAL.NO_QUESTS"));
    }

    private static async Task WriteToFile(string fileName, string content)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        await File.WriteAllTextAsync(path, content);
    }
}