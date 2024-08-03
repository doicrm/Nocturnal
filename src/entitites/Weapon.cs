using Nocturnal.src.core;
using Nocturnal.src.services;
using Nocturnal.src.ui;
using System.Text;

namespace Nocturnal.src.entitites
{
    public class Weapon : Item
    {
        public int DamageMin { get; set; }
        public int DamageMax { get; set; }

        public Weapon(string id, string? name, string? description, int damageMin, int damageMax, float value) : base()
        {
            ID = id;
            Name = name;
            Type = ItemType.Weapon;
            Description = description;
            DamageMin = damageMin;
            DamageMax = damageMax;
            Value = value;
        }

        public override string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{Display.GetJsonString("NAME")}: {Name}");
            sb.AppendLine($"{Display.GetJsonString("DESCRIPTION")}: {Description}");
            sb.AppendLine($"{Display.GetJsonString("DAMAGE_MIN")}: {DamageMin}");
            sb.AppendLine($"{Display.GetJsonString("DAMAGE_MAX")}: {DamageMin}");
            sb.AppendLine($"{Display.GetJsonString("TYPE")}: {Type}");
            sb.AppendLine($"{Display.GetJsonString("VALUE")}: {Value}");

            return sb.ToString();
        }

        public static new void InsertInstances()
        {
            var weapons = new[]
{
                new Weapon("Pistol", $"{JsonService.JsonReader!["WEAPON.PISTOL.NAME"]}", $"{JsonService.JsonReader!["WEAPON.PISTOL.DESCRIPTION"]}", 10, 15, 250)
            };

            foreach (var weapon in weapons)
            {
                Globals.Items.Add(weapon.ID, weapon);
            }
        }
    }
}
