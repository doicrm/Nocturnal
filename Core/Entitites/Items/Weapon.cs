using Nocturnal.Core.System;

namespace Nocturnal.Core.Entitites.Items
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
            return $"{Globals.JsonReader!["NAME"]}: {Name}\n" +
                $"{Globals.JsonReader!["DESCRIPTION"]}: {Description}\n" +
                $"{Globals.JsonReader!["DAMAGE_MIN"]}: {DamageMin}\n" +
                $"{Globals.JsonReader!["DAMAGE_MAX"]}: {DamageMin}\n" +
                $"{Globals.JsonReader!["TYPE"]}: {Type}\n" +
                $"{Globals.JsonReader!["VALUE"]}: {Value}";
        }

        public static new void InsertInstances()
        {
            Weapon Pistol = new("Pistol", $"{Globals.JsonReader!["WEAPON.PISTOL.NAME"]}", $"{Globals.JsonReader!["WEAPON.PISTOL.DESCRIPTION"]}", 10, 15, 250);

            Globals.Items[Pistol.ID] = Pistol;
        }
    }
}
