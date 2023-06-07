using Nocturnal.Core.System;

namespace Nocturnal.Core.Entitites;

public enum ItemType { None, Weapon, Clothes, Food, Quest, Written, Misc }

public abstract class Item
{
    public string? Name { get; set; }
    public ItemType Type { get; set; }
    public string? Description { get; set; }
    public float Value { get; set; }

    virtual public string PrintInfo()
    {
        return $"{Globals.JsonReader!["NAME"]}: {Name}\n" +
            $"{Globals.JsonReader!["DESCRIPTION"]}: {Description}\n" +
            $"{Globals.JsonReader!["TYPE"]}: {Type}\n" +
            $"{Globals.JsonReader!["VALUE"]}: {Value}";
    }
}
