using Nocturnal.core;
using Nocturnal.entitites;

namespace Nocturnal.services
{
    public abstract class GameDataService
    {
        public static async Task InitAll()
        {
            await InitHeroInventory().ConfigureAwait(false);
            await InitHeroJournal().ConfigureAwait(false);
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
                await inventory.UpdateFile().ConfigureAwait(false);
                return;
            }

            await WriteToFile("Inventory.txt", LocalizationService.GetString("INVENTORY.NO_ITEMS")).ConfigureAwait(false);
        }

        public static async Task InitHeroJournal()
        {
            var journal = Globals.Player.Journal;

            if (journal != null && !journal.IsEmpty())
            {
                await journal.UpdatedJournalFile().ConfigureAwait(false);
                return;
            }

            await WriteToFile("Journal.txt", LocalizationService.GetString("JOURNAL.NO_QUESTS")).ConfigureAwait(false);
        }

        private static async Task WriteToFile(string fileName, string content)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            await File.WriteAllTextAsync(path, content).ConfigureAwait(false);
        }
    }
}
