using System.Text;
using Nocturnal.services;

namespace Nocturnal.entitites;

public class Inventory
{
    private IList<Item> Items { get; } = [];

    public Inventory() { }

    public async Task AddItem(Item item)
    {
        Items.Add(item);
        await UpdateFile();
        await SaveService.UpdateSave();
    }

    public async Task RemoveItem(Item item)
    {
        if (!HasItem(item)) return;

        Items.Remove(item);
        await UpdateFile();
        await SaveService.UpdateSave();
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

    public async Task Clear()
    {
        if (!IsEmpty()) return;

        Items.Clear();
        await UpdateFile();
        await SaveService.UpdateSave();
    }

    public bool HasItem(Item item)
    {
        return Items.Contains(item, new ItemEqualityComparer());
    }

    public async Task UpdateFile()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Inventory.txt");

        var sb = new StringBuilder();

        if (Items.Count <= 0)
        {
            sb.AppendLine(Localizator.GetString("INVENTORY.NO_ITEMS"));
            await File.WriteAllTextAsync(path, sb.ToString());
            return;
        }

        foreach (var item in Items)
        {
            sb.AppendLine($"{Localizator.GetString("NAME")}: {item.Name}");
            sb.AppendLine($"{Localizator.GetString("TYPE")}: {item.Type}");
            sb.AppendLine($"{Localizator.GetString("DESCRIPTION")}: {item.Description}");
            sb.AppendLine($"{Localizator.GetString("VALUE")}: {item.Value}$");
            sb.AppendLine("...........................................................................");
        }

        await File.WriteAllTextAsync(path, sb.ToString());
    }
}

internal class ItemEqualityComparer : IEqualityComparer<Item>
{
    public bool Equals(Item? item1, Item? item2) {
        return item1?.Id == item2?.Id && item1?.Name == item2?.Name;
    }

    public int GetHashCode(Item item) {
        return HashCode.Combine(item.Id, item.Name);
    }
}