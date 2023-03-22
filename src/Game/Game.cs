namespace Nocturnal.src
{
    enum Weather
    {
        Sunny, Cloudy, Stormy, Rainy, Snowfall
    }

    internal class Game
    {
        public static readonly Game Instance = new();
        private bool IsPlaying { get; set; }
        private int Menu { get; set; }
        private int Choice { get; set; }
        //private Location* CurrentLocation { get; set; }
        private Weather Weather { get; set; }

        static readonly string[] Logo = new string[8] {
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
            //CurrentLocation = null;
        }

        //static void InitLocations();

        //static void InitHeroIventory();

        //static void InitHeroJournal();

        public static void Pause()
        {
            Console.Write("\tPress any key...");
            Console.ReadKey();
        }

        //void Run();

        //void InitAll();

        //void Welcome();

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

        //void MainMenu();

        //void NewGame();

        //void LoadGame();

        //void ChangeLanguage();

        //void Credits();

        //void LoadLogo();

        //void EndGame();

        void End() { IsPlaying = false; }

        //void SetCurrentLocation(Location* location);

        void SetWeather(Weather weather) { Weather = weather; }
    }
}
