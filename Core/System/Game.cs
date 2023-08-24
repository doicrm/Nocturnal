using Nocturnal.Core.Entitites;
using Nocturnal.Core.Entitites.Characters;
using Nocturnal.Core.Entitites.Items;
using Nocturnal.Core.Events.Prologue;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.System
{
    public enum Weather { Sunny, Cloudy, Stormy, Rainy, Snowfall }

    public sealed class Game
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
            ChangeConsoleName();
            Settings = new();
            StoryGlobals = StoryGlobals.Instance;
            Logger.WriteLog("Game initialized");
        }

        public void Run()
        {
            while (IsPlaying)
            {
                Welcome();
                //WriteLogo(); // Disable for testing use
                LoadLogo();
                MainMenu();
                End();
            }
        }

        public static void ChangeConsoleName()
            => Console.Title = $"{Constants.GAME_NAME} {Constants.GAME_VERSION}";

        public static void Pause()
        {
            Console.Write($"\t{Display.GetJsonString("PRESS_ANY_KEY")}");
            Console.ReadKey();
        }

        public static void Welcome()
        {
            Console.Clear();
            Thread.Sleep(500);
            Display.Write($"\n\t{Display.GetJsonString("AUTHOR_PRESENTS")}", 40);
            Thread.Sleep(2000);
            Console.Clear();
        }

        public static void WriteLogo()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            foreach (var s in Constants.GAME_LOGO)
                Display.Write(s, 1);
            Console.ResetColor();
        }

        public void LoadLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            foreach (var s in Constants.GAME_LOGO)
                Console.Write(s);
            Console.ResetColor();
            MainMenu();
        }

        public void MainMenu()
        {
            Console.ResetColor();
            Console.WriteLine();
            Menu mainMenu = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("MAIN_MENU.NEW_GAME"), NewGame },
                { Display.GetJsonString("MAIN_MENU.LOAD_GAME"), LoadGame },
                { Display.GetJsonString("MAIN_MENU.CHANGE_LANG"), ChangeLanguage },
                { Display.GetJsonString("MAIN_MENU.QUIT_GAME"), EndGame }
            });
        }

        public static void NewGame()
        {
            InitAll();
            SaveManager.CreateSave();
            Console.Clear();
            PrologueEvents.Prologue();
        }

        public static void LoadGame()
        {
            Console.Clear();
            Console.Write($"\n\t{Display.GetJsonString("MAIN_MENU.LOAD_GAME").ToUpper()}");
            Console.ResetColor();
            SaveManager.SearchForSaves();
        }

        public void ChangeLanguage()
        {
            GameSettings.CreateConfigFile();
            GameSettings.LoadDataFromFile(GameSettings.Lang);
            LoadLogo();
        }

        public void EndGame()
        {
            Console.Clear();
            Display.Write($"\n\t{Display.GetJsonString("QUIT_GAME")}", 25);
            Menu quitMenu = new(new Dictionary<string, Action>()
            {
                { Display.GetJsonString("YES"), End },
                { Display.GetJsonString("NO"), LoadLogo }
            });
        }

        public void End()
        {
            IsPlaying = false;
            Console.Clear();
            Environment.Exit(1);
        }

        public void SetCurrentLocation(Location location)
        {
            if (CurrentLocation != null)
            {
                CurrentLocation.IsVisited = true;
                Globals.Locations[CurrentLocation.ID].IsVisited = true;
                SaveManager.UpdateSave();
            }

            if (CurrentLocation == null && !Globals.Locations["DarkAlley"].IsVisited)
            {
                location = Globals.Locations["DarkAlley"];
            }

            if (!Globals.Locations.ContainsKey(location.ID)) return;

            CurrentLocation = location;
            CurrentLocation!.Events!.Invoke();
        }

        public static void InitHeroIventory()
        {
            if (Globals.Player.Inventory!.IsEmpty())
            {
                string path = $"{Directory.GetCurrentDirectory()}\\Inventory.txt";
                using StreamWriter output = new(path);
                output.WriteLine(Display.GetJsonString("INVENTORY.NO_ITEMS"));
                output.Close();
                return;
            }

            Globals.Player.Inventory!.UpdateFile();
        }

        public static void InitHeroJournal()
        {
            if (Globals.Player.Journal!.IsEmpty())
            {
                string path = $"{Directory.GetCurrentDirectory()}\\Journal.txt";
                using StreamWriter output = new(path);
                output.WriteLine(Display.GetJsonString("JOURNAL.NO_QUESTS"));
                output.Close();
                return;
            }

            Globals.Player.Journal!.UpdatedJournalFile();
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
            InitHeroIventory();
            InitHeroJournal();
            Npc.InsertInstances();
            Item.InsertInstances();
            Fraction.InsertInstances();
            Quest.InsertInstances();
            InitLocations();
        }
    }
}
