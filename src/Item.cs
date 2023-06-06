namespace Nocturnal.src;

public enum ItemType { None, Weapon, Clothes, Food, Quest, Written, Misc }

public abstract class Item
{
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public string Description { get; set; }
    public float Value { get; set; }

    virtual public string PrintInfo()
    {
        return ($"{Globals.JsonReader!["NAME"]}: {Name}\n" +
            $"{Globals.JsonReader!["DESCRIPTION"]}: {Description}\n" +
            $"{Globals.JsonReader!["TYPE"]}: {Type}\n" +
            $"{Globals.JsonReader!["VALUE"]}: {Value}");
    }
}

public class Weapon : Item
{
    public int DamageMin { get; set; }
    public int DamageMax { get; set; }

    public Weapon (int damageMin, int damageMax)
    {
        Type = ItemType.Weapon;
        DamageMin = damageMin;
        DamageMax = damageMax;
    }

    public override string PrintInfo()
    {
        return ($"{Globals.JsonReader!["NAME"]}: {Name}\n" +
            $"{Globals.JsonReader!["DESCRIPTION"]}: {Description}\n" +
            $"{Globals.JsonReader!["DAMAGE_MIN"]}: {DamageMin}\n" +
            $"{Globals.JsonReader!["DAMAGE_MAX"]}: {DamageMin}\n" +
            $"{Globals.JsonReader!["TYPE"]}: {Type}\n" +
            $"{Globals.JsonReader!["VALUE"]}: {Value}");
    }
}
