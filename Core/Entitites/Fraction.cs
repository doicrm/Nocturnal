using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

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
            return Display.GetJsonString("ATTITUDE.ANGRY").ToLower();
        else if (Attitude is Attitudes.Hostile)
            return Display.GetJsonString("ATTITUDE.HOSTILE").ToLower();
        else if (Attitude is Attitudes.Friendly)
            return Display.GetJsonString("ATTITUDE.FRIENDLY").ToLower();
        return Display.GetJsonString("ATTITUDE.NEUTRAL").ToLower();
    }

    public static void InsertInstances()
    {
        Fraction Beggars = new("Beggars", Display.GetJsonString("FRACTION.BEGGARS"), 0, Attitudes.Neutral);
        Fraction Police = new("Police", Display.GetJsonString("FRACTION.POLICE"), 0, Attitudes.Neutral);
        Fraction Hammers = new("Hammers", Display.GetJsonString("FRACTION.HAMMERS"), 0, Attitudes.Neutral);

        Globals.Fractions[Beggars.ID] = Beggars;
        Globals.Fractions[Police.ID] = Police;
        Globals.Fractions[Hammers.ID] = Hammers;
    }
}
