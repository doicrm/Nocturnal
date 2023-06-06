namespace Nocturnal.src;

public static class Event
{
    public static void HeroDeath()
    {
        ClearInstances();
        Thread.Sleep(500);
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Display.Write($"{Globals.JsonReader!["YOU_ARE_DEAD"]}");
        Thread.Sleep(1000);
        Console.ResetColor();
        Display.Write($"{Globals.JsonReader!["BACK_TO_MENU"]}", 25);
        Console.ReadKey();
        Console.Clear();
        Program.Game!.LoadLogo();
    }

    public static void GameOver()
    {
        ClearInstances();
        Thread.Sleep(500);
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine();
        Display.Write($"{Globals.JsonReader!["GAME_OVER"]}", 25);
        Thread.Sleep(1000);
        Console.WriteLine("\n");
        Console.ResetColor();
        Program.Game!.Credits();
    }

    public static void ClearInstances()
    {
        // TODO!!!
    }

    //-------------------------------------
    //  INTRO events
    //-------------------------------------
    public static void Prologue()
    {
        Display.Write($"\n\t{Globals.JsonReader!["PROLOGUE"]}");
        Thread.Sleep(2000);
        Display.Write($"\n\n\t{Globals.JsonReader!["PARADISE_LOST"]}");
        Thread.Sleep(5000);
        Console.Clear();
        StoryIntroduction();
    }

    public static void StoryIntroduction()
    {
        Display.Write($"\n\t{Globals.JsonReader!["INTRO_ZERO"]}");
        Thread.Sleep(1000);
        Display.Write($" {Globals.JsonReader!["INTRO_ONE"]}\n", 20);
        Game.Pause();
        Console.Clear();
        Console.WriteLine();
        Thread.Sleep(2500);
        Display.WriteNarration($"\t{Globals.JsonReader!["INTRO_TWO"]}", 75);
        Thread.Sleep(2500);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["INTRO_THREE"]}", 75);
        Thread.Sleep(2500);
        Display.WriteNarration($"\n\t{Globals.JsonReader!["INTRO_FOUR"]}", 75);
        Thread.Sleep(3000);
        Console.Clear();
        //WakeUpInDarkAlley();
    }

    //-------------------------------------
    //  PROLOGUE: Dark Alley events
    //-------------------------------------
    public static void DarkAlley()
    {
        if (!Globals.Locations["DarkAlley"].IsVisited)
        {
            Globals.Locations["DarkAlley"].IsVisited = true;
            Prologue();
            return;
        }
        //DarkAlleyCrossroads();
    }
}
