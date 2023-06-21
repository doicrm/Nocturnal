using Nocturnal.Core.Entitites.Items;
using Nocturnal.Core.Entitites.Properties;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;
using System.Reflection;

namespace Nocturnal.Core.Entitites.Characters;

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

    public void SetAttitude(Attitudes attitude)
    {
        if (Attitude == attitude) return;
        Attitude = attitude;
        PrintAttitude();
    }

    public void PrintAttitude()
    {
        string attitude;

        if (Attitude is Attitudes.Angry)
        {
            attitude = $"{Globals.JsonReader!["ATTITUDE.ANGRY"]!.ToString().ToLower()}";
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else if (Attitude is Attitudes.Hostile)
        {
            attitude = $"{Globals.JsonReader!["ATTITUDE.HOSTILE"]!.ToString().ToLower()}";
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (Attitude is Attitudes.Friendly)
        {
            attitude = $"{Globals.JsonReader!["ATTITUDE.FRIENDLY"]!.ToString().ToLower()}";
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            attitude = $"{Globals.JsonReader!["ATTITUDE.NEUTRAL"]!.ToString().ToLower()}";
            Console.ResetColor();
        }

        if (GameSettings.Lang == (int)GameLanguages.EN)
            Display.Write($"\t{Name} is {attitude} now.\n");
        else
            Display.Write($"\t{Name} jest teraz {attitude}.\n");
        Console.ResetColor();
    }

    public bool IsDead() { return Status == NpcStatus.Dead; }

    public void AddItem(Item item) => Inventory!.AddItem(item);
    public void RemoveItem(Item item) => Inventory!.RemoveItem(item);
    public void ShowInventory() => Inventory!.Show();
    public void ClearInventory() => Inventory!.Clear();
    public bool HasItem(Item item) { return Inventory!.HasItem(item); }

    public static void InsertInstances()
    {
        Npc Bob = new("Bob", "Bob", Genders.Male, null!);
        Npc Caden = new("Caden", "Caden", Genders.Male, null!);
        Npc CadensPartner = new("CadensPartner", $"{Globals.JsonReader!["NPC.POLICEMAN"]}", Genders.Male, null!);
        Npc Zed = new("Zed", "Zed", Genders.Male, null!);
        Npc Luna = new("Luna", "Luna", Genders.Female, null!);
        Npc Jet = new("Jet", "Jet", Genders.Male, null!);
        Npc HexFolstam = new("HexFolstam", "Hex Folstam", Genders.Male, null!);
        Npc Enigma = new("Enigma", "Enigma", Genders.Male, null!);

        Globals.Npcs.Add(Bob.ID, Bob);
        Globals.Npcs.Add(Caden.ID, Caden);
        Globals.Npcs.Add(CadensPartner.ID, CadensPartner);
        Globals.Npcs.Add(Zed.ID, Zed);
        Globals.Npcs.Add(Luna.ID, Luna);
        Globals.Npcs.Add(Jet.ID, Jet);
        Globals.Npcs.Add(HexFolstam.ID, HexFolstam);
        Globals.Npcs.Add(Enigma.ID, Enigma);
    }
}
