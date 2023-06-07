using Nocturnal.Core.System;

namespace Nocturnal.Core.Entitites;

public enum Attitudes { Neutral, Angry, Hostile, Friendly }

public class Fraction
{
    public string Name { get; set; }
    public uint HeroReputation { get; set; }
    public Attitudes Attitude { get; set; }

    public Fraction()
    {
        Name = "None";
        HeroReputation = 0;
        Attitude = Attitudes.Neutral;
    }

    public Fraction(string name, uint heroReputation, Attitudes attitude)
    {
        Name = name;
        HeroReputation = heroReputation;
        Attitude = attitude;
    }

    public void AddRep(uint heroReputation) { HeroReputation += heroReputation; }
    public void RemoveRep(uint heroReputation) { HeroReputation -= heroReputation; }
    public void SetAttitude(Attitudes attitude) { Attitude = attitude; }
    public string PrintAttitude()
    {
        if (Attitude is Attitudes.Angry)
            return $"{Globals.JsonReader!["ATTITUDE.ANGRY"]!.ToString().ToLower()}";
        else if (Attitude is Attitudes.Hostile)
            return $"{Globals.JsonReader!["ATTITUDE.HOSTILE"]!.ToString().ToLower()}";
        else if (Attitude is Attitudes.Friendly)
            return $"{Globals.JsonReader!["ATTITUDE.FRIENDLY"]!.ToString().ToLower()}";
        return $"{Globals.JsonReader!["ATTITUDE.NEUTRAL"]!.ToString().ToLower()}";
    }
}
