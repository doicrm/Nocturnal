using Nocturnal.Core.Entitites.Properties;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;
using System.Reflection;

namespace Nocturnal.Core.Entitites.Living;

public enum Genders { Male, Female, Undefined }

public enum NpcStatus { Normal, Unconscious, Dead }

public class Npc
{
    public string Name { get; set; }
    public Genders Sex { get; set; }
    public Attributes Attributes { get; set; }
    public Fraction? Fraction { get; set; }
    public Attitudes Attitude { get; set; }
    public NpcStatus Status { get; set; }
    public bool IsKnowHero { get; set; }

    public Npc()
    {
        Name = "None";
        Sex = Genders.Undefined;
        Fraction = null;
        Attitude = Attitudes.Neutral;
        Status = NpcStatus.Normal;
        IsKnowHero = false;
    }

    public Npc(string name, Genders sex, Fraction fraction)
    {
        Name = name;
        Sex = sex;
        Fraction = fraction;
        Attitude = Attitudes.Neutral;
        Status = NpcStatus.Normal;
        IsKnowHero = false;
    }

    public Npc(string name, Genders sex, Fraction fraction, Attitudes attitude, NpcStatus status, bool isKnowHero)
    {
        Name = name;
        Sex = sex;
        Fraction = fraction;
        Attitude = attitude;
        Status = status;
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
        else
        {
            Console.WriteLine("Invalid attribute name.");
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

        Display.Write($"\t{Name} is {attitude} now.");
        Console.ResetColor();
    }

    public bool IsDead() { return Status == NpcStatus.Dead; }
}
