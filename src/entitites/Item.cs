using Nocturnal.src.core;
using Nocturnal.src.services;
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
            sb.AppendLine($"{LocalizationService.GetString("NAME")}: {Name}");
            sb.AppendLine($"{LocalizationService.GetString("DESCRIPTION")}: {Description}");
            sb.AppendLine($"{LocalizationService.GetString("TYPE")}: {Type}");
            sb.AppendLine($"{LocalizationService.GetString("VALUE")}: {Value}");

            return sb.ToString();
        }

        public static void InsertInstances()
        {
            var items = new[]
            {
                new Item("AD13", LocalizationService.GetString("ITEM.AD13.NAME"), ItemType.Quest, LocalizationService.GetString("ITEM.AD13.DESCRIPTION"), 50),
                new Item("AccessCard", LocalizationService.GetString("ITEM.ACCESS_CARD.NAME"), ItemType.Misc, LocalizationService.GetString("ITEM.ACCESS_CARD.DESCRIPTION"), 0)
            };

            Globals.Items = items.ToDictionary(item => item.ID);
            Weapon.InsertInstances();
        }
    }
}
