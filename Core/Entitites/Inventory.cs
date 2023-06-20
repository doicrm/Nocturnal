using Nocturnal.Core.Entitites.Items;
using Nocturnal.Core.System;

namespace Nocturnal.Core.Entitites;

public class Inventory
{
    public IList<Item> Items { get; set; }

    public Inventory()
    {
        Items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
        UpdateFile();
        SaveManager.UpdateSave();
    }

    public void RemoveItem(Item item)
    {
        if (!HasItem(item)) return;

        Items.Remove(item);
        UpdateFile();
        SaveManager.UpdateSave();
    }

    public void Show()
    {
        if (!Items.Any()) return;

        for (int i = 0; i < Items.Count; i++)
        {
            Console.WriteLine($"\t{i + 1}. {Items[i].Name}");
        }
    }

    public void Clear()
    {
        if (Items.Any())
            Items.Clear();
    }

    public bool HasItem(Item item)
    {
        foreach (Item Item in Items)
            if (Item == item)
                return true;
        return false;
    }

    public void UpdateFile()
    {
        string path = $"{Directory.GetCurrentDirectory()}\\Inventory.txt";
        using StreamWriter output = new(path);

        if (Items.Count <= 0)
        {
            output.WriteLine($"{Globals.JsonReader!["INVENTORY.NO_ITEMS"]}");
            output.Close();
            return;
        }

        foreach (Item Item in Items)
        {
            output.WriteLine($"{Globals.JsonReader!["NAME"]}: {Item.Name}");
            output.WriteLine($"{Globals.JsonReader!["TYPE"]}: {Item.Type}");
            output.WriteLine($"{Globals.JsonReader!["DESCRIPTION"]}: {Item.Description}");
            output.WriteLine($"{Globals.JsonReader!["VALUE"]}: {Item.Value}$");
            output.WriteLine("...........................................................................");
        }

        output.Close();
    }
}
