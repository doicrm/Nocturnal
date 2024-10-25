using System.Reflection;
using Nocturnal.core;
using Nocturnal.services;
using Nocturnal.ui;

namespace Nocturnal.entitites
{
    public enum Genders { Male, Female, Undefined }
    public enum NpcStatus { Normal, Unconscious, Dead }

    public class Npc
    {
        protected string Id { get; init; }
        public string Name { get; set; }
        public Genders Sex { get; set; }
        protected Attributes Attributes { get; set; }
        public Fraction? Fraction { get; set; }
        public Attitudes Attitude { get; private set; }
        public NpcStatus Status { get; set; }
        public Inventory? Inventory { get; protected init; }
        public bool IsKnowHero { get; set; }

        protected Npc()
        {
            Id = "";
            Name = "";
            Sex = Genders.Undefined;
            Attributes = Attributes.Default();
            Fraction = null;
            Attitude = Attitudes.Neutral;
            Status = NpcStatus.Normal;
            Inventory = new Inventory();
            IsKnowHero = false;
        }

        private Npc(string id, string name, Genders sex, Fraction fraction)
        {
            Id = id;
            Name = name;
            Sex = sex;
            Attributes = Attributes.Default();
            Fraction = fraction;
            Attitude = Attitudes.Neutral;
            Status = NpcStatus.Normal;
            Inventory = new Inventory();
            IsKnowHero = false;
        }

        public Npc(string id, string name, Genders sex, Fraction fraction, Attitudes attitude, NpcStatus status, bool isKnowHero)
        {
            Id = id;
            Name = name;
            Sex = sex;
            Attributes = Attributes.Default();
            Fraction = fraction;
            Attitude = attitude;
            Status = status;
            Inventory = new Inventory();
            IsKnowHero = isKnowHero;
        }

        public void RaiseAttribute(string attributeName, int value)
        {
            var property = typeof(Attributes).GetProperty(attributeName);
            if (property != null && property.PropertyType == typeof(int?))
            {
                var attributeValue = (int?)property.GetValue(Attributes);
                if (attributeValue == null) return;
                var newValue = attributeValue.Value + value;
                property.SetValue(Attributes, newValue);
            }
            else
            {
                Console.WriteLine("Invalid attribute name.");
            }
        }

        public void DropAttribute(string attributeName, int value)
        {
            var property = typeof(Attributes).GetProperty(attributeName);
            if (property == null || property.PropertyType != typeof(int?)) return;
            var attributeValue = (int?)property.GetValue(Attributes);
            if (attributeValue == null) return;
            var newValue = attributeValue.Value - value;
            property.SetValue(Attributes, newValue);
        }

        public async Task SetAttitude(Attitudes newAttitude)
        {
            if (Attitude == newAttitude) return;
            Attitude = newAttitude;
            await PrintAttitude();
        }

        private async Task PrintAttitude()
        {
            var (attitude, color) = Attitude switch
            {
                Attitudes.Angry => (LocalizationService.GetString("ATTITUDE.ANGRY").ToLower(), ConsoleColor.Yellow),
                Attitudes.Hostile => (LocalizationService.GetString("ATTITUDE.HOSTILE").ToLower(), ConsoleColor.Red),
                Attitudes.Friendly => (LocalizationService.GetString("ATTITUDE.FRIENDLY").ToLower(), ConsoleColor.Green),
                _ => (LocalizationService.GetString("ATTITUDE.NEUTRAL").ToLower(), (ConsoleColor?)null)
            };

            if (color.HasValue)
                Console.ForegroundColor = color.Value;
            else
                Console.ResetColor();

            var message = Game.Instance.Settings.GetLanguage() == GameLanguages.En
                ? $"\t{Name} is {attitude} now.\n"
                : $"\t{Name} jest teraz {attitude}.\n";

            await Display.Write(message);
            Console.ResetColor();
        }

        public bool IsDead() { return Status == NpcStatus.Dead; }

        public async Task AddItem(Item item) => await Inventory!.AddItem(item);
        public async Task RemoveItem(Item item) => await Inventory!.RemoveItem(item);
        public void ShowInventory() => Inventory!.Show();
        public async Task ClearInventory() => await Inventory!.Clear();
        public bool HasItem(Item item) { return Inventory!.HasItem(item); }

        public static void InsertInstances()
        {
            var npcs = new List<Npc>
            {
                new("Bob", "Bob", Genders.Male, null!),
                new("Caden", "Caden", Genders.Male, null!),
                new("CadensPartner", LocalizationService.GetString("NPC.POLICEMAN"), Genders.Male, null!),
                new("Zed", "Zed", Genders.Male, null!),
                new("Luna", "Luna", Genders.Female, null!),
                new("Jet", "Jet", Genders.Male, null!),
                new("HexFolstam", "Hex Folstam", Genders.Male, null!),
                new("Enigma", "Enigma", Genders.Male, null!)
            };

            Globals.Npcs = npcs.ToDictionary(npc => npc.Id);
        }
    }
}
