using Nocturnal.src.core;
using Nocturnal.src.services;

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
            return $"{JsonService.JsonReader!["NAME"]}: {Name}\n" +
                $"{JsonService.JsonReader!["DESCRIPTION"]}: {Description}\n" +
                $"{JsonService.JsonReader!["DAMAGE_MIN"]}: {DamageMin}\n" +
                $"{JsonService.JsonReader!["DAMAGE_MAX"]}: {DamageMin}\n" +
                $"{JsonService.JsonReader!["TYPE"]}: {Type}\n" +
                $"{JsonService.JsonReader!["VALUE"]}: {Value}";
        }

        public static new void InsertInstances()
        {
            Weapon Pistol = new("Pistol", $"{JsonService.JsonReader!["WEAPON.PISTOL.NAME"]}", $"{JsonService.JsonReader!["WEAPON.PISTOL.DESCRIPTION"]}", 10, 15, 250);

            Globals.Items[Pistol.ID] = Pistol;
        }
    }
}
