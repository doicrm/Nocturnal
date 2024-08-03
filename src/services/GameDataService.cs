using Nocturnal.src.core;
using Nocturnal.src.entitites;
using Nocturnal.src.ui;

namespace Nocturnal.src.services
{
    public class GameDataService
    {
        public static async Task InitAll()
        {
            await InitHeroInventory();
            await InitHeroJournal();
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

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Inventory.txt");
            await File.WriteAllTextAsync(path, Display.GetJsonString("INVENTORY.NO_ITEMS"));
        }

        public static async Task InitHeroJournal()
        {
            var journal = Globals.Player.Journal;

            if (journal != null && !journal.IsEmpty())
            {
                await journal.UpdatedJournalFile();
                return;
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Journal.txt");
            await File.WriteAllTextAsync(path, Display.GetJsonString("JOURNAL.NO_QUESTS"));
        }
    }
}
