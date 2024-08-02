using Nocturnal.src.services;
using Nocturnal.src.ui;
using System.Text;

namespace Nocturnal.src.entitites
{
    public class Inventory
    {
        public IList<Item> Items { get; set; }

        public Inventory()
        {
            Items = new List<Item>();
        }

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
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Inventory.txt");

            var sb = new StringBuilder();

            if (Items.Count <= 0)
            {
                sb.AppendLine(Display.GetJsonString("INVENTORY.NO_ITEMS"));
                return;
            }

            foreach (Item item in Items)
            {
                sb.AppendLine($"{Display.GetJsonString("NAME")}: {item.Name}");
                sb.AppendLine($"{Display.GetJsonString("TYPE")}: {item.Type}");
                sb.AppendLine($"{Display.GetJsonString("DESCRIPTION")}: {item.Description}");
                sb.AppendLine($"{Display.GetJsonString("VALUE")}: {item.Value}$");
                sb.AppendLine("...........................................................................");
            }

            await File.WriteAllTextAsync(path, sb.ToString());
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
}