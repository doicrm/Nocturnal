using Nocturnal.core.utils;
using Nocturnal.entitites;
using Nocturnal.events.prologue;
using Nocturnal.services;
using Nocturnal.ui;

namespace Nocturnal.core
{
    public enum Weather { Sunny, Cloudy, Stormy, Rainy, Snowfall }

    public class Game
    {
        public bool IsPlaying { get; set; }
        public Location? CurrentLocation { get; set; }
        public Weather Weather { get; set; }
        public GameSettings Settings { get; set; }
        public StoryGlobals StoryGlobals { get; set; }

        private static Game? _instance = null;

        public static Game Instance
        {
            get
            {
                _instance ??= new Game();
                return _instance;
            }
        }

        private Game()
        {
            IsPlaying = true;
            CurrentLocation = null;
            ConsoleService.InitConsole();
            Settings = new GameSettings();
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
            await Display.Write($"{Localizator.GetString("PRESS_ANY_KEY")}", 25);
            await Task.Run(Console.ReadKey);
        }

        private static async Task Welcome()
        {
            Console.Clear();
            await Task.Delay(500);
            await Display.Write($"\n\t{Localizator.GetString("AUTHOR_PRESENTS")}", 40);
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
                    { Localizator.GetString("MAIN_MENU.NEW_GAME"), NewGame },
                    { Localizator.GetString("MAIN_MENU.LOAD_GAME"), LoadGame },
                    { Localizator.GetString("MAIN_MENU.CHANGE_LANG"), ChangeLanguage },
                    { Localizator.GetString("MAIN_MENU.QUIT_GAME"), EndGame }
                });
            });
        }

        private static async Task NewGame()
        {
            await GameDataService.InitAll();
            await SaveService.CreateSave();
            Console.Clear();
            await PrologueEvents.Prologue();
        }

        private static async Task LoadGame()
        {
            Console.Clear();
            await Display.Write($"\n{Localizator.GetString("MAIN_MENU.LOAD_GAME").ToUpper()}", 25);
            Console.ResetColor();
            await SaveService.FindSaves();
        }

        private async Task ChangeLanguage()
        {
            await ConfigService.CreateConfigFile();
            await JsonService.LoadAndParseLocalizationFile(Settings.GetLanguage());
            await Display.LoadLogo();
        }

        private async Task EndGame()
        {
            Console.Clear();
            await Display.Write($"\n{Localizator.GetString("QUIT_GAME")}", 25);
            _ = new InteractiveMenu(new MenuOptions
            {
                { Localizator.GetString("YES"), End },
                { Localizator.GetString("NO"), Display.LoadLogo }
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
                Globals.Locations[CurrentLocation.Id].IsVisited = true;
                await SaveService.UpdateSave();
            }

            if (CurrentLocation == null && !Globals.Locations["DarkAlley"].IsVisited) {
                location = Globals.Locations["DarkAlley"];
            }

            if (!Globals.Locations.ContainsKey(location.Id)) return;

            CurrentLocation = location;
            await CurrentLocation.Events!.Invoke();
        }
    }
}
