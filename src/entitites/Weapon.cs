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

            sb.AppendLine($"{LocalizationService.GetString("NAME")}: {Name}");
            sb.AppendLine($"{LocalizationService.GetString("DESCRIPTION")}: {Description}");
            sb.AppendLine($"{LocalizationService.GetString("DAMAGE_MIN")}: {DamageMin}");
            sb.AppendLine($"{LocalizationService.GetString("DAMAGE_MAX")}: {DamageMin}");
            sb.AppendLine($"{LocalizationService.GetString("TYPE")}: {Type}");
            sb.AppendLine($"{LocalizationService.GetString("VALUE")}: {Value}");

            return sb.ToString();
        }

        public static new void InsertInstances()
        {
            var weapons = new[]
{
                new Weapon("Pistol", LocalizationService.GetString("WEAPON.PISTOL.NAME"), LocalizationService.GetString("WEAPON.PISTOL.DESCRIPTION"), 10, 15, 250)
            };

            foreach (var weapon in weapons)
            {
                Globals.Items.Add(weapon.ID, weapon);
            }
        }
    }
}
