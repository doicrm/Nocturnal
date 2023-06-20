using Nocturnal.Core.System;

namespace Nocturnal.Core.Entitites;

public enum Attitudes { Neutral, Angry, Hostile, Friendly }

public class Fraction
{
    public string ID { get; set; }
    public string Name { get; set; }
    public uint HeroReputation { get; set; }
    public Attitudes Attitude { get; set; }

    public Fraction()
    {
        ID = "";
        Name = "";
        HeroReputation = 0;
        Attitude = Attitudes.Neutral;
    }

    public Fraction(string id, string name, uint heroReputation, Attitudes attitude)
    {
        ID = id;
        Name = name;
        HeroReputation = heroReputation;
        Attitude = attitude;
    }

    public void AddRep(uint heroReputation) => HeroReputation += heroReputation;
    public void RemoveRep(uint heroReputation) => HeroReputation -= heroReputation;
    public void SetAttitude(Attitudes attitude) => Attitude = attitude;
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

    public static void InsertInstances()
    {
        Fraction Beggars = new("Beggars", "Beggars", 0, Attitudes.Neutral);
        Fraction Police = new("Police", "Police", 0, Attitudes.Neutral);
        Fraction Hammers = new("Hammers", "Hammers", 0, Attitudes.Neutral);
        Fraction Sleepers = new("Sleepers", "Sleepers", 0, Attitudes.Neutral);

        Globals.Fractions[Beggars.ID] = Beggars;
        Globals.Fractions[Police.ID] = Police;
        Globals.Fractions[Hammers.ID] = Hammers;
        Globals.Fractions[Sleepers.ID] = Sleepers;
    }
}
