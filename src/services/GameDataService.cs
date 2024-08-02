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
            if (!Globals.Player.Inventory!.IsEmpty())
            {
                await Globals.Player.Inventory!.UpdateFile();
                return;
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Inventory.txt");
            using StreamWriter output = new(path);
            await output.WriteLineAsync(Display.GetJsonString("INVENTORY.NO_ITEMS"));
        }

        public static async Task InitHeroJournal()
        {
            if (!Globals.Player.Journal!.IsEmpty())
            {
                await Globals.Player.Journal!.UpdatedJournalFile();
                return;
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Journal.txt");
            using StreamWriter output = new(path);
            await output.WriteLineAsync(Display.GetJsonString("JOURNAL.NO_QUESTS"));
        }

        public static void InitLocations()
        {
            Location DarkAlley = new("DarkAlley", Display.GetJsonString("LOCATION.DARK_ALLEY"), null!, PrologueEvents.DarkAlley);
            Location Street = new("Street", Display.GetJsonString("LOCATION.STREET"), Globals.Fractions["Police"], PrologueEvents.Street);
            Location GunShop = new("GunShop", Display.GetJsonString("LOCATION.GUN_SHOP"), Globals.Fractions["Police"], PrologueEvents.GunShop);
            Location NightclubEden = new("NightclubEden", Display.GetJsonString("LOCATION.NIGHTCLUB_EDEN"), Globals.Fractions["Police"], PrologueEvents.NightclubEden);

            Globals.Locations.Add(DarkAlley.ID, DarkAlley);
            Globals.Locations.Add(Street.ID, Street);
            Globals.Locations.Add(GunShop.ID, GunShop);
            Globals.Locations.Add(NightclubEden.ID, NightclubEden);
        }
    }
}
