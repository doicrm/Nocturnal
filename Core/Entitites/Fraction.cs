namespace Nocturnal.Core.Entitites;

public enum Attitudes { Neutral, Angry, Hostile, Friendly }

public class Fraction
{
    public string Name { get; set; }
    public int HeroReputation { get; set; }
    public int Attitude { get; set; }

    public Fraction()
    {
        Name = "None";
        HeroReputation = 0;
        Attitude = (int)Attitudes.Neutral;
    }

    public Fraction(string name, int heroReputation, int attitude)
    {
        Name = name;
        HeroReputation = heroReputation;
        Attitude = attitude;
    }

    public void AddRep(int heroReputation) { HeroReputation += heroReputation; }
    public void RemoveRep(int heroReputation) { HeroReputation -= heroReputation; }
    public void SetAttitude(int attitude) { Attitude = attitude; }
    public string PrintAttitude()
    {
        if (Attitude == (int)Attitudes.Angry)
            return "Angry";
        else if (Attitude == (int)Attitudes.Hostile)
            return "Hostile";
        else if (Attitude == (int)Attitudes.Friendly)
            return "Friendly";
        return "Neutral";
    }
}
