namespace Nocturnal.src
{
    public enum Weather { Sunny, Cloudy, Stormy, Rainy, Snowfall }

    internal class Game
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

        //public static void InitHeroIventory();

        //public static void InitHeroJournal();

        public static void Pause()
        {
            Console.Write("\tPress any key...");
            Console.ReadKey();
        }

        public void Run()
        {
            while (IsPlaying)
            {
                Welcome();
                //WriteLogo();
                LoadLogo();
                MainMenu();
                End();
            }
        }

        public void InitAll()
        {
            InitLocations();
        }


        public static void Welcome()
        {
            Console.Clear();
            Thread.Sleep(500);
            Display.Write("\n\tRadosław 'Doic' Michalak presents", 40);
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
                { "Nowa gra", NewGame },
                { "Wczytaj grę", LoadGame },
                { "Zmień język", ChangeLanguage },
                { "Autorzy", Credits },
                { "Wyjdź z gry", EndGame }
            };

            Menu mainMenu = new(options);
        }

        public void NewGame()
        {
            SaveManager.CreateSave();
            InitAll();
            Console.Clear();
            //SetCurrentLocation(DarkAlley);
        }

        public void LoadGame()
        {
            Console.Clear();
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
            Display.Write("\tCzy na pewno chcesz zakończyć rozgrywkę?", 25);

            var options = new Dictionary<string, Action>()
            {
                { "Tak", End },
                { "Nie", LoadLogo }
            };

            Menu quitMenu = new(options);
        }

        public void End() { IsPlaying = false; }

        public void SetCurrentLocation(Location location)
        {
            CurrentLocation = location;
            CurrentLocation.Events();
        }

        public void SetWeather(Weather weather) { Weather = weather; }

        public static void InitLocations()
        {
            Location DarkAlley = new("Dark alley", null, null);

            //Locations.Add("Dark Alley", DarkAlley);
        }
    }
}
