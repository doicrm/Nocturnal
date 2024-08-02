using Nocturnal.src.core;
using Nocturnal.src.services;
using Nocturnal.src.ui;
using System.Reflection;

namespace Nocturnal.src.entitites
{
    public enum Genders { Male, Female, Undefined }
    public enum NpcStatus { Normal, Unconscious, Dead }

    public class Npc
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Genders Sex { get; set; }
        public Attributes Attributes { get; set; }
        public Fraction? Fraction { get; set; }
        public Attitudes Attitude { get; set; }
        public NpcStatus Status { get; set; }
        public Inventory? Inventory { get; set; }
        public bool IsKnowHero { get; set; }

        public Npc()
        {
            ID = "";
            Name = "";
            Sex = Genders.Undefined;
            Attributes = Attributes.Default();
            Fraction = null;
            Attitude = Attitudes.Neutral;
            Status = NpcStatus.Normal;
            Inventory = new();
            IsKnowHero = false;
        }

        public Npc(string id, string name, Genders sex, Fraction fraction)
        {
            ID = id;
            Name = name;
            Sex = sex;
            Attributes = Attributes.Default();
            Fraction = fraction;
            Attitude = Attitudes.Neutral;
            Status = NpcStatus.Normal;
            Inventory = new();
            IsKnowHero = false;
        }

        public Npc(string id, string name, Genders sex, Fraction fraction, Attitudes attitude, NpcStatus status, bool isKnowHero)
        {
            ID = id;
            Name = name;
            Sex = sex;
            Attributes = Attributes.Default();
            Fraction = fraction;
            Attitude = attitude;
            Status = status;
            Inventory = new();
            IsKnowHero = isKnowHero;
        }

        public void RaiseAttribute(string attributeName, int value)
        {
            PropertyInfo? property = typeof(Attributes).GetProperty(attributeName);
            if (property != null && property.PropertyType == typeof(int?))
            {
                int? attributeValue = (int?)property.GetValue(Attributes);
                if (attributeValue != null)
                {
                    int newValue = attributeValue.Value + value;
                    property.SetValue(Attributes, newValue);
                }
            }
            else
            {
                Console.WriteLine("Invalid attribute name.");
            }
        }

        public void DropAttribute(string attributeName, int value)
        {
            PropertyInfo? property = typeof(Attributes).GetProperty(attributeName);
            if (property != null && property.PropertyType == typeof(int?))
            {
                int? attributeValue = (int?)property.GetValue(Attributes);
                if (attributeValue != null)
                {
                    int newValue = attributeValue.Value - value;
                    property.SetValue(Attributes, newValue);
                }
            }
        }

        public async Task SetAttitude(Attitudes attitude)
        {
            if (Attitude == attitude) return;
            Attitude = attitude;
            await PrintAttitude();
        }

        public async Task PrintAttitude()
        {
            string attitude;

            if (Attitude is Attitudes.Angry)
            {
                attitude = $"{JsonService.JsonReader!["ATTITUDE.ANGRY"]!.ToString().ToLower()}";
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (Attitude is Attitudes.Hostile)
            {
                attitude = $"{JsonService.JsonReader!["ATTITUDE.HOSTILE"]!.ToString().ToLower()}";
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (Attitude is Attitudes.Friendly)
            {
                attitude = $"{JsonService.JsonReader!["ATTITUDE.FRIENDLY"]!.ToString().ToLower()}";
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                attitude = $"{JsonService.JsonReader!["ATTITUDE.NEUTRAL"]!.ToString().ToLower()}";
                Console.ResetColor();
            }

            if (Game.Instance.Settings.GetLanguage() == GameLanguages.EN)
                await Display.Write($"\t{Name} is {attitude} now.\n");
            else
                await Display.Write($"\t{Name} jest teraz {attitude}.\n");
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
                new("CadensPartner", $"{JsonService.JsonReader!["NPC.POLICEMAN"]}", Genders.Male, null!),
                new("Zed", "Zed", Genders.Male, null!),
                new("Luna", "Luna", Genders.Female, null!),
                new("Jet", "Jet", Genders.Male, null!),
                new("HexFolstam", "Hex Folstam", Genders.Male, null!),
                new("Enigma", "Enigma", Genders.Male, null!)
            };

            foreach (var npc in npcs)
            {
                Globals.Npcs.Add(npc.ID, npc);
            }
        }
    }
}
