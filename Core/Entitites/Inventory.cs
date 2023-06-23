using Nocturnal.Core.Entitites.Items;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

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
        if (Items.Any()) Items.Clear();
        SaveManager.UpdateSave();
    }

    public bool HasItem(Item item)
    {
        return Items.Contains(item);
    }

    public void UpdateFile()
    {
        string path = $"{Directory.GetCurrentDirectory()}\\Inventory.txt";
        using StreamWriter output = new(path);

        if (Items.Count <= 0)
        {
            output.WriteLine($"{Display.GetJsonString("INVENTORY.NO_ITEMS")}");
            output.Close();
            return;
        }

        foreach (Item Item in Items)
        {
            output.WriteLine($"{Display.GetJsonString("NAME")}: {Item.Name}");
            output.WriteLine($"{Display.GetJsonString("TYPE")}: {Item.Type}");
            output.WriteLine($"{Display.GetJsonString("DESCRIPTION")}: {Item.Description}");
            output.WriteLine($"{Display.GetJsonString("VALUE")}: {Item.Value}$");
            output.WriteLine("...........................................................................");
        }

        output.Close();
    }
}
