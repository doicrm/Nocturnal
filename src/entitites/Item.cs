using System.Text;
using Nocturnal.core;
using Nocturnal.services;

namespace Nocturnal.entitites
{
    public enum ItemType { None, Weapon, Clothes, Food, Quest, Written, Misc }

    public class Item
    {
        public string Id { get; protected init; }
        public string? Name { get; protected init; }
        public ItemType Type { get; protected init; }
        public string? Description { get; protected init; }
        public float Value { get; protected init; }

        private Item(string id, string? name, ItemType type, string? description, float value)
        {
            Id = id;
            Name = name;
            Type = type;
            Description = description;
            Value = value;
        }

        protected Item() {
            throw new NotImplementedException();
        }

        public virtual string PrintInfo()
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

            Globals.Items = items.ToDictionary(item => item.Id);
            Weapon.InsertInstances();
        }
    }
}
