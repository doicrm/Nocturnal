namespace Nocturnal.src
{
    public enum ItemType { None, Weapon, Clothes, Food, Quest, Written, Misc }
    public abstract class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
    }
}
