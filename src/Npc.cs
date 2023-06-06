namespace Nocturnal.src;

public enum Genders { Male, Female, Undefined }

public enum NpcStatus { Normal, Unconscious, Dead }

public class Npc
{
    public string Name { get; set; }
    public Genders Sex { get; set; }
    public Fraction? Fraction { get; set; }
    public Attitudes Attitude { get; set; }
    public NpcStatus Status { get; set; }
    public bool IsKnowHero { get; set; }

    public Npc()
    {
        Name = "None";
        Sex = Genders.Undefined;
        Fraction = null;
        Attitude = Attitudes.Neutral;
        Status = NpcStatus.Normal;
        IsKnowHero = false;
    }

    public Npc(string name, Genders sex, Fraction fraction)
    {
        Name = name;
        Sex = sex;
        Fraction = fraction;
        Attitude = Attitudes.Neutral;
        Status = NpcStatus.Normal;
        IsKnowHero = false;
    }

    public Npc (string name, Genders sex, Fraction fraction, Attitudes attitude, NpcStatus status, bool isKnowHero)
    {
        Name = name;
        Sex = sex;
        Fraction = fraction;
        Attitude = attitude;
        Status = status;
        IsKnowHero = isKnowHero;
    }

    public void SetAttitude(Attitudes attitude)
    {
        if (Attitude == attitude) return;
        Attitude = attitude;
        PrintAttitude();
    }

    public void PrintAttitude()
    {
        string attitude;

        if (Attitude == Attitudes.Angry)
        {
            attitude = $"{Globals.JsonReader!["ATTITUDE.ANGRY"]!.ToString().ToLower()}";
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else if (Attitude == Attitudes.Hostile)
        {
            attitude = $"{Globals.JsonReader!["ATTITUDE.HOSTILE"]!.ToString().ToLower()}";
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (Attitude == Attitudes.Friendly)
        {
            attitude = $"{Globals.JsonReader!["ATTITUDE.FRIENDLY"]!.ToString().ToLower()}";
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            attitude = $"{Globals.JsonReader!["ATTITUDE.NEUTRAL"]!.ToString().ToLower()}";
            Console.ResetColor();
        }

        Display.Write($"\t{Name} is {attitude} now.");
        Console.ResetColor();
    }

    public bool IsDead()
    {
        return Status == NpcStatus.Dead;
    }
}
