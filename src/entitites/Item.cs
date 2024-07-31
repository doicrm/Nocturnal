using Nocturnal.src.core;

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

        public Item()
        {
        }

        virtual public string PrintInfo()
        {
            return $"{Globals.JsonReader!["NAME"]}: {Name}\n" +
                $"{Globals.JsonReader!["DESCRIPTION"]}: {Description}\n" +
                $"{Globals.JsonReader!["TYPE"]}: {Type}\n" +
                $"{Globals.JsonReader!["VALUE"]}: {Value}";
        }

        static public void InsertInstances()
        {
            Item AD13 = new("AD13", $"{Globals.JsonReader!["ITEM.AD13.NAME"]}", ItemType.Quest, $"{Globals.JsonReader!["ITEM.AD13.DESCRIPTION"]}", 50);
            Item AccessCard = new("AccessCard", $"{Globals.JsonReader!["ITEM.ACCESS_CARD.NAME"]}", ItemType.Misc, $"{Globals.JsonReader!["ITEM.ACCESS_CARD.DESCRIPTION"]}", 0);

            Globals.Items[AD13.ID] = AD13;
            Globals.Items[AccessCard.ID] = AccessCard;

            Weapon.InsertInstances();
        }
    }
}
