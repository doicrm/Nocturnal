using Nocturnal.src.core;
using Nocturnal.src.ui;
using System.Text;

namespace Nocturnal.src.entitites
{
    public enum ItemType { None, Weapon, Clothes, Food, Quest, Written, Misc }

    public class Item
    {
        public string ID { get; set; }
        public string? Name { get; set; }
        public ItemType Type { get; set; }
        public string? Description { get; set; }
        public float Value { get; set; }

        public Item(string id, string? name, ItemType type, string? description, float value)
        {
            ID = id;
            Name = name;
            Type = type;
            Description = description;
            Value = value;
        }

        public Item() { }

        virtual public string PrintInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Display.GetJsonString("NAME")}: {Name}");
            sb.AppendLine($"{Display.GetJsonString("DESCRIPTION")}: {Description}");
            sb.AppendLine($"{Display.GetJsonString("TYPE")}: {Type}");
            sb.AppendLine($"{Display.GetJsonString("VALUE")}: {Value}");

            return sb.ToString();
        }

        public static void InsertInstances()
        {
            var items = new[]
            {
                new Item("AD13", Display.GetJsonString("ITEM.AD13.NAME"), ItemType.Quest, Display.GetJsonString("ITEM.AD13.DESCRIPTION"), 50),
                new Item("AccessCard", Display.GetJsonString("ITEM.ACCESS_CARD.NAME"), ItemType.Misc, Display.GetJsonString("ITEM.ACCESS_CARD.DESCRIPTION"), 0)
            };

            foreach (var item in items)
            {
                Globals.Items[item.ID] = item;
            }

            Weapon.InsertInstances();
        }
    }
}
