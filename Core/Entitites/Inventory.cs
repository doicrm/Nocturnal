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
        if (IsEmpty()) return;

        foreach (var item in Items)
        {
            Console.WriteLine(item.Name);
        }
    }

    public bool IsEmpty()
    {
        return !Items.Any();
    }

    public void Clear()
    {
        if (IsEmpty()) Items.Clear();
        SaveManager.UpdateSave();
    }

    public bool HasItem(Item item)
    {
        return Items.Contains(item, new ItemEqualityComparer());
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

class ItemEqualityComparer : IEqualityComparer<Item>
{
    public bool Equals(Item? item1, Item? item2)
    {
        return item1?.ID == item2?.ID && item1?.Name == item2?.Name;
    }

    public int GetHashCode(Item item)
    {
        return HashCode.Combine(item.ID, item.Name);
    }
}
