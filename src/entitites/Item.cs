using Nocturnal.src.core;
using Nocturnal.src.ui;

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
            return $"{Display.GetJsonString("NAME")}: {Name}\n" +
                $"{Display.GetJsonString("DESCRIPTION")}: {Description}\n" +
                $"{Display.GetJsonString("TYPE")}: {Type}\n" +
                $"{Display.GetJsonString("VALUE")}: {Value}";
        }

        static public void InsertInstances()
        {
            Item AD13 = new("AD13", $"{Display.GetJsonString("ITEM.AD13.NAME")}", ItemType.Quest, $"{Display.GetJsonString("ITEM.AD13.DESCRIPTION")}", 50);
            Item AccessCard = new("AccessCard", $"{Display.GetJsonString("ITEM.ACCESS_CARD.NAME")}", ItemType.Misc, $"{Display.GetJsonString("ITEM.ACCESS_CARD.DESCRIPTION")}", 0);

            Globals.Items[AD13.ID] = AD13;
            Globals.Items[AccessCard.ID] = AccessCard;

            Weapon.InsertInstances();
        }
    }
}
