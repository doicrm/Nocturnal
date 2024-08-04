using Nocturnal.src.entitites;
using Nocturnal.src.events.prologue;
using Nocturnal.src.core.utilities;
using Nocturnal.src.services;
using Nocturnal.src.ui;
using Spectre.Console;

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
            ConsoleService.InitConsole();
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
            await Display.Write($"{LocalizationService.GetString("PRESS_ANY_KEY")}", 25);
            await Task.Run(() => Console.ReadKey());
        }

        public static async Task Welcome()
        {
            Console.Clear();
            await Task.Delay(500);
            await Display.Write($"\n\t{LocalizationService.GetString("AUTHOR_PRESENTS")}", 40);
            await Task.Delay(2000);
            Console.Clear();
        }

        public async Task MainMenu()
        {
            await Task.Run(() =>
            {
                Console.ResetColor();
                Console.WriteLine();
                _ = new InteractiveMenu(new MenuOptions
                {
                    { LocalizationService.GetString("MAIN_MENU.NEW_GAME"), NewGame },
                    { LocalizationService.GetString("MAIN_MENU.LOAD_GAME"), LoadGame },
                    { LocalizationService.GetString("MAIN_MENU.CHANGE_LANG"), ChangeLanguage },
                    { LocalizationService.GetString("MAIN_MENU.QUIT_GAME"), EndGame }
                });
            });
        }

        public static async Task NewGame()
        {
            await GameDataService.InitAll();
            await SaveService.CreateSave();
            Console.Clear();
            await PrologueEvents.Prologue();
        }

        public static async Task LoadGame()
        {
            Console.Clear();
            await Display.Write($"\n{LocalizationService.GetString("MAIN_MENU.LOAD_GAME").ToUpper()}", 25);
            Console.ResetColor();
            await SaveService.FindSaves();
        }

        public async Task ChangeLanguage()
        {
            await ConfigService.CreateConfigFile();
            await JsonService.LoadAndParseLocalizationFile(Settings.GetLanguage());
            await Display.LoadLogo();
        }

        public async Task EndGame()
        {
            Console.Clear();
            await Display.Write($"\n{LocalizationService.GetString("QUIT_GAME")}", 25);
            _ = new InteractiveMenu(new MenuOptions
            {
                { LocalizationService.GetString("YES"), End },
                { LocalizationService.GetString("NO"), Display.LoadLogo }
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
    }
}
