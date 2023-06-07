using Nocturnal.Core.Entitites.Items;

namespace Nocturnal.Core.Entitites;

public class Player : Npc
{
    private int HP;
    public float Money;
    //public Weapon Weapon;
    //public Clothes Clothes;
    public Inventory Inventory;
    public Journal Journal;

    public Player()
    {
        HP = 60;
        Money = 0;
    }

    public Player(int hP, float money, /*Weapon weapon, Clothes clothes,*/ Inventory inventory, Journal journal)
    {
        HP = hP;
        Money = money;
        //Weapon = weapon;
        //Clothes = clothes;
        Inventory = inventory;
        Journal = journal;
    }

    public void AddHP(int hp)
    {
        HP = hp;
    }

    public void RemoveHP(int hp)
    {
        HP -= hp;
        if (HP < 0) Kill();
    }

    public static void Kill()
    {
        Event.HeroDeath();
    }

    public new bool IsDead()
    {
        return HP < 0;
    }

    public void AddItem(Item item)
    {
        Inventory.AddItem(item);
    }

    public void RemoveItem(Item item)
    {
        Inventory.RemoveItem(item);
    }

    public void AddQuest(Quest quest)
    {
        Journal.AddQuest(quest);
    }

    public void EndQuest(Quest quest, QuestStatus status)
    {
        Journal.EndQuest(quest, status);
    }

    public void ShowInventory()
    {
        Inventory.Show();
    }

    public void ClearInventory()
    {
        Inventory.Clear();
    }

    public bool HasItem(Item item)
    {
        return Inventory.HasItem(item);
    }
}
