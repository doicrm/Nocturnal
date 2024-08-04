using Nocturnal.src.core;
using Nocturnal.src.services;

namespace Nocturnal.src.entitites
{
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
                return LocalizationService.GetString("ATTITUDE.ANGRY").ToLower();
            if (Attitude is Attitudes.Hostile)
                return LocalizationService.GetString("ATTITUDE.HOSTILE").ToLower();
            if (Attitude is Attitudes.Friendly)
                return LocalizationService.GetString("ATTITUDE.FRIENDLY").ToLower();
            return LocalizationService.GetString("ATTITUDE.NEUTRAL").ToLower();
        }

        public static void InsertInstances()
        {
            var fractions = new[]
            {
                new Fraction("Beggars", LocalizationService.GetString("FRACTION.BEGGARS"), 0, Attitudes.Neutral),
                new Fraction("Police", LocalizationService.GetString("FRACTION.POLICE"), 0, Attitudes.Neutral),
                new Fraction("Hammers", LocalizationService.GetString("FRACTION.HAMMERS"), 0, Attitudes.Neutral)
            };

            Globals.Fractions = fractions.ToDictionary(fraction => fraction.ID);
        }
    }
}
