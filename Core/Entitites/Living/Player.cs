using Nocturnal.Core.Entitites.Items;

namespace Nocturnal.Core.Entitites.Living;

public class Player : Npc
{
    private int HP { get; set; }
    public float Money { get; set; }
    public Weapon? Weapon { get; set; }
    //public Clothes Clothes { get; set; }
    public Inventory? Inventory = new();
    public Journal? Journal = new();

    public Player()
    {
        HP = 60;
        Money = 0;
    }

    public Player(int hp, float money, Weapon weapon, /*Clothes clothes,*/ Inventory inventory, Journal journal)
    {
        HP = hp;
        Money = money;
        Weapon = weapon;
        //Clothes = clothes;
        Inventory = inventory;
        Journal = journal;
    }

    public void AddHP(int hp) => HP = hp;

    public void RemoveHP(int hp)
    {
        HP -= hp;
        if (HP < 0) Kill();
    }

    public static void Kill() => Event.HeroDeath();

    public new bool IsDead() { return HP < 0; }

    public void AddItem(Item item) => Inventory!.AddItem(item);

    public void RemoveItem(Item item) => Inventory!.RemoveItem(item);

    public void AddQuest(Quest quest) => Journal!.AddQuest(quest);

    public void EndQuest(Quest quest, QuestStatus status) => Journal!.EndQuest(quest, status);

    public void ShowInventory() => Inventory!.Show();

    public void ClearInventory() => Inventory!.Clear();

    public bool HasItem(Item item) { return Inventory!.HasItem(item); }
}
