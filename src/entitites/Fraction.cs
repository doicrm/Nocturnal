using Nocturnal.core;
using Nocturnal.services;

namespace Nocturnal.entitites
{
    public enum Attitudes { Neutral, Angry, Hostile, Friendly }

    public class Fraction
    {
        private string Id { get; set; }
        public string Name { get; set; }
        private uint HeroReputation { get; set; }
        private Attitudes Attitude { get; set; }

        public Fraction()
        {
            Id = "";
            Name = "";
            HeroReputation = 0;
            Attitude = Attitudes.Neutral;
        }

        private Fraction(string id, string name, uint heroReputation, Attitudes attitude)
        {
            Id = id;
            Name = name;
            HeroReputation = heroReputation;
            Attitude = attitude;
        }

        public void AddRep(uint heroReputation) => HeroReputation += heroReputation;
        public void RemoveRep(uint heroReputation) => HeroReputation -= heroReputation;
        public void SetAttitude(Attitudes attitude) => Attitude = attitude;
        public string PrintAttitude()
        {
            return Attitude switch
            {
                Attitudes.Angry => LocalizationService.GetString("ATTITUDE.ANGRY").ToLower(),
                Attitudes.Hostile => LocalizationService.GetString("ATTITUDE.HOSTILE").ToLower(),
                Attitudes.Friendly => LocalizationService.GetString("ATTITUDE.FRIENDLY").ToLower(),
                _ => LocalizationService.GetString("ATTITUDE.NEUTRAL").ToLower()
            };
        }

        public static void InsertInstances()
        {
            var fractions = new[]
            {
                new Fraction("Beggars", LocalizationService.GetString("FRACTION.BEGGARS"), 0, Attitudes.Neutral),
                new Fraction("Police", LocalizationService.GetString("FRACTION.POLICE"), 0, Attitudes.Neutral),
                new Fraction("Hammers", LocalizationService.GetString("FRACTION.HAMMERS"), 0, Attitudes.Neutral)
            };

            Globals.Fractions = fractions.ToDictionary(fraction => fraction.Id);
        }
    }
}
