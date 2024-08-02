using Nocturnal.src.core;
using Nocturnal.src.entitites;
using Nocturnal.src.events.prologue;
using Nocturnal.src.ui;

namespace Nocturnal.src.services
{
    public class GameDataService
    {
        public static void InitAll()
        {
            InitHeroInventory().GetAwaiter().GetResult();
            InitHeroJournal().GetAwaiter().GetResult();
            Npc.InsertInstances();
            Item.InsertInstances();
            Fraction.InsertInstances();
            Quest.InsertInstances();
            InitLocations();
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

        public static void InitLocations()
        {
            var locations = new List<Location>
            {
                new("DarkAlley", Display.GetJsonString("LOCATION.DARK_ALLEY"), null!, PrologueEvents.DarkAlley),
                new("Street", Display.GetJsonString("LOCATION.STREET"), Globals.Fractions["Police"], PrologueEvents.Street),
                new("GunShop", Display.GetJsonString("LOCATION.GUN_SHOP"), Globals.Fractions["Police"], PrologueEvents.GunShop),
                new("NightclubEden", Display.GetJsonString("LOCATION.NIGHTCLUB_EDEN"), Globals.Fractions["Police"], PrologueEvents.NightclubEden)
            };

            foreach (var location in locations)
            {
                Globals.Locations.Add(location.ID, location);
            }
        }
    }
}
