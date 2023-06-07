using Nocturnal.Core.Entitites.Items;

namespace Nocturnal.Core.Entitites;

public class Inventory
{
    public List<Item> Items = new();

    public void AddItem(Item item) => Items.Add(item);

    public void RemoveItem(Item item) => Items.Remove(item);

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
        {
            if (Item == item) return true;
        }
        return false;
    }
}
