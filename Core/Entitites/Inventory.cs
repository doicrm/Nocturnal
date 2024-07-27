using Nocturnal.Core.Entitites.Items;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Entitites
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
            await SaveManager.UpdateSave();
        }

        public async Task RemoveItem(Item item)
        {
            if (!HasItem(item)) return;

            Items.Remove(item);
            await UpdateFile();
            await SaveManager.UpdateSave();
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
            if (IsEmpty()) return;

            Items.Clear();
            await UpdateFile();
            await SaveManager.UpdateSave();
        }

        public bool HasItem(Item item)
        {
            return Items.Contains(item, new ItemEqualityComparer());
        }

        public async Task UpdateFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Inventory.txt");

            using (StreamWriter output = new StreamWriter(path))
            {
                if (Items.Count <= 0)
                {
                    await output.WriteLineAsync(Display.GetJsonString("INVENTORY.NO_ITEMS"));
                    return;
                }

                foreach (Item item in Items)
                {
                    await output.WriteLineAsync($"{Display.GetJsonString("NAME")}: {item.Name}");
                    await output.WriteLineAsync($"{Display.GetJsonString("TYPE")}: {item.Type}");
                    await output.WriteLineAsync($"{Display.GetJsonString("DESCRIPTION")}: {item.Description}");
                    await output.WriteLineAsync($"{Display.GetJsonString("VALUE")}: {item.Value}$");
                    await output.WriteLineAsync("...........................................................................");
                }
            }
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