namespace Nocturnal.src
{
    public enum Weather { Sunny, Cloudy, Stormy, Rainy, Snowfall }

    public class Game
    {
        public static readonly Game Instance = new();
        private bool IsPlaying { get; set; }
        private int Menu { get; set; }
        private int Choice { get; set; }
        private Location? CurrentLocation { get; set; }
        private Weather Weather { get; set; }

        public static readonly string[] Logo = new string[8] {
            "\t ****     **   *******     ******  ********** **     ** *******   ****     **     **     **\n",
            "\t/**/**   /**  **/////**   **////**/////**/// /**    /**/**////** /**/**   /**    ****   /**\n",
            "\t/**//**  /** **     //** **    //     /**    /**    /**/**   /** /**//**  /**   **//**  /**\n",
            "\t/** //** /**/**      /**/**           /**    /**    /**/*******  /** //** /**  **  //** /**\n",
            "\t/**  //**/**/**      /**/**           /**    /**    /**/**///**  /**  //**/** **********/**\n",
            "\t/**   //****//**     ** //**    **    /**    /**    /**/**  //** /**   //****/**//////**/**\n",
            "\t/**    //*** //*******   //******     /**    //******* /**   //**/**    //***/**     /**/********\n",
            "\t//      ///   ///////     //////      //      ///////  //     // //      /// //      // ////////\n"
        };

        public Game()
        {
            Menu = 0;
            Choice = 0;
            IsPlaying = true;
            CurrentLocation = null;
        }

        public void Run()
        {
            while (IsPlaying)
            {
                Welcome();
                WriteLogo();
                MainMenu();
                End();
            }
        }

        public static void Pause()
        {
            Console.Write(Globals.JsonReader["messages"]["press_any_key"].ToString());
            Console.ReadKey();
        }

        public static void Welcome()
        {
            Console.Clear();
            Thread.Sleep(500);
            Display.Write(Globals.JsonReader["messages"]["game_introduce"].ToString(), 40);
            Thread.Sleep(2000);
            Console.Clear();
        }

        public static void WriteLogo()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();

            foreach (string s in Logo)
            {
                Display.Write(s, 1);
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        public void MainMenu()
        {
            Console.ResetColor();
            Console.WriteLine();

            var options = new Dictionary<string, Action>()
            {
                { Globals.JsonReader["options"]["main_menu"][0].ToString(), NewGame },
                { Globals.JsonReader["options"]["main_menu"][1].ToString(), LoadGame },
                { Globals.JsonReader["options"]["main_menu"][2].ToString(), ChangeLanguage },
                { Globals.JsonReader["options"]["main_menu"][3].ToString(), EndGame }
            };

            Menu mainMenu = new(options);
        }

        public void NewGame()
        {
            SaveManager.CreateSave();
            InitAll();
            Console.Clear();
            //SetCurrentLocation(Globals.Locations["DarkAlley"]);
        }

        public void LoadGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Globals.JsonReader["messages"]["feature_unavailable"].ToString());
            Console.ResetColor();
            SaveManager.SearchForSaves();
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(Globals.JsonReader["messages"]["back_to_menu"].ToString());
            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
            LoadLogo();
            MainMenu();
        }

        public void ChangeLanguage()
        {
            GameSettings.CreateConfigFile();
            GameSettings.LoadDataFromFile(GameSettings.lang);
            LoadLogo();
        }

        public void Credits()
        {
            Console.Clear();
        }

        public void LoadLogo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();

            foreach (string s in Logo)
            {
                Console.Write(s);
            }

            Console.WriteLine();
            Console.ResetColor();
            MainMenu();
        }

        public void EndGame()
        {
            Console.Clear();
            Display.Write(Globals.JsonReader["messages"]["quit_game"].ToString(), 25);

            var options = new Dictionary<string, Action>()
            {
                { Globals.JsonReader["Yes"].ToString(), End },
                { Globals.JsonReader["No"].ToString(), LoadLogo }
            };

            Menu quitMenu = new(options);
        }

        public void End()
        {
            IsPlaying = false;
            Console.Clear();
            Environment.Exit(1);
        }

        public void SetCurrentLocation(Location location)
        {
            CurrentLocation = location;
            CurrentLocation.Events();
        }

        public void SetWeather(Weather weather) { Weather = weather; }

        public static void InitHeroIventory()
        {

        }

        public static void InitHeroJournal()
        {

        }

        public static void InitLocations()
        {
            Location DarkAlley = new("Dark alley", null, null);

            Globals.Locations.Add("Dark Alley", DarkAlley);
        }

        public static void InitAll()
        {
            InitLocations();
        }
    }
}
