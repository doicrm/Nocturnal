﻿namespace Nocturnal.src
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Nocturnal [Demo Build]";
            GameSettings config = new();

            if (config.LoadConfigFile())
            {
                Game game = new Game();
            }
        }
    }
}
