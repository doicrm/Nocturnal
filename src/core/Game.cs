using Nocturnal.src.entitites;
using Nocturnal.src.events.prologue;
using Nocturnal.src.core.utilities;
using Nocturnal.src.services;
using Nocturnal.src.ui;

namespace Nocturnal.src.core
{
    public enum Weather { Sunny, Cloudy, Stormy, Rainy, Snowfall }

    public class Game
    {
        public bool IsPlaying { get; set; }
        public Location? CurrentLocation { get; set; }
        public Weather Weather { get; set; }
        public GameSettings Settings { get; set; }
        public StoryGlobals StoryGlobals { get; set; }

        private static Game? instance = null;

        public static Game Instance
        {
            get
            {
                instance ??= new Game();
                return instance;
            }
        }

        private Game()
        {
            IsPlaying = true;
            CurrentLocation = null;
            ConsoleService.ChangeConsoleName();
            Settings = new();
            StoryGlobals = StoryGlobals.Instance;
            Logger.WriteLog("Game initialized").GetAwaiter().GetResult();
        }

        public async Task Run()
        {
            while (IsPlaying)
            {
                await Welcome();
                //await WriteLogo(); // Disable for testing use
                await Display.LoadLogo();
                await MainMenu();
                await End();
            }
        }

        public static async Task Pause()
        {
            await Display.Write($"\t{Display.GetJsonString("PRESS_ANY_KEY")}", 25);
            await Task.Run(() => Console.ReadKey());
        }

        public static async Task Welcome()
        {
            Console.Clear();
            await Task.Delay(500);
            await Display.Write($"\n\t{Display.GetJsonString("AUTHOR_PRESENTS")}", 40);
            await Task.Delay(2000);
            Console.Clear();
        }

        public async Task MainMenu()
        {
            await Task.Run(() =>
            {
                Console.ResetColor();
                Console.WriteLine();
                _ = new InteractiveMenu(new Dictionary<string, Func<Task>>()
                {
                    { Display.GetJsonString("MAIN_MENU.NEW_GAME"), NewGame },
                    { Display.GetJsonString("MAIN_MENU.LOAD_GAME"), LoadGame },
                    { Display.GetJsonString("MAIN_MENU.CHANGE_LANG"), ChangeLanguage },
                    { Display.GetJsonString("MAIN_MENU.QUIT_GAME"), EndGame }
                });
            });
        }

        public static async Task NewGame()
        {
            InitAll();
            await SaveService.CreateSave();
            Console.Clear();
            await PrologueEvents.Prologue();
        }

        public static async Task LoadGame()
        {
            Console.Clear();
            await Display.Write($"\n\t{Display.GetJsonString("MAIN_MENU.LOAD_GAME").ToUpper()}", 25);
            Console.ResetColor();
            await SaveService.SearchForSaves();
        }

        public async Task ChangeLanguage()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), ConfigService.configFilePath);
            await ConfigService.CreateConfigFile(filePath);
            await ConfigService.LoadDataFromFile(Settings.GetLanguage());
            await Display.LoadLogo();
        }

        public async Task EndGame()
        {
            Console.Clear();
            await Display.Write($"\n\t{Display.GetJsonString("QUIT_GAME")}", 25);
            _ = new InteractiveMenu(new Dictionary<string, Func<Task>>()
            {
                { Display.GetJsonString("YES"), End },
                { Display.GetJsonString("NO"), Display.LoadLogo }
            });
        }

        public async Task End()
        {
            await Task.Run(() =>
            {
                IsPlaying = false;
                Console.Clear();
                Environment.Exit(1);
            });
        }

        public async Task SetCurrentLocation(Location location)
        {
            if (CurrentLocation != null)
            {
                CurrentLocation.IsVisited = true;
                Globals.Locations[CurrentLocation.ID].IsVisited = true;
                await SaveService.UpdateSave();
            }

            if (CurrentLocation == null && !Globals.Locations["DarkAlley"].IsVisited) {
                location = Globals.Locations["DarkAlley"];
            }

            if (!Globals.Locations.ContainsKey(location.ID)) return;

            CurrentLocation = location;
            await CurrentLocation.Events!.Invoke();
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
    }
}
