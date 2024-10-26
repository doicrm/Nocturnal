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
            // TODO!!!
        }

        public virtual string PrintInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Localizator.GetString("NAME")}: {Name}");
            sb.AppendLine($"{Localizator.GetString("DESCRIPTION")}: {Description}");
            sb.AppendLine($"{Localizator.GetString("TYPE")}: {Type}");
            sb.AppendLine($"{Localizator.GetString("VALUE")}: {Value}");

            return sb.ToString();
        }

        public static void InsertInstances()
        {
            var items = new[]
            {
                new Item("AD13", Localizator.GetString("ITEM.AD13.NAME"), ItemType.Quest, Localizator.GetString("ITEM.AD13.DESCRIPTION"), 50),
                new Item("AccessCard", Localizator.GetString("ITEM.ACCESS_CARD.NAME"), ItemType.Misc, Localizator.GetString("ITEM.ACCESS_CARD.DESCRIPTION"), 0)
            };

            Globals.Items = items.ToDictionary(item => item.Id);
            Weapon.InsertInstances();
        }
    }
}
