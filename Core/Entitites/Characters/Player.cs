using Nocturnal.Core.Entitites.Properties;
using Nocturnal.Core.Entitites.Items;

namespace Nocturnal.Core.Entitites.Characters;

public class Player : Npc
{
    public float Money { get; set; }
    public Weapon? Weapon { get; set; }
    //public Clothes Clothes { get; set; }
    public Journal? Journal = new();

    public Player() : base()
    {
        Money = 0.0f;
    }

    public Player(float money, Weapon weapon, /*Clothes clothes,*/ Inventory inventory, Journal journal) : base()
    {
        Money = money;
        Weapon = weapon;
        //Clothes = clothes;
        Inventory = inventory;
        Journal = journal;
    }

    public static void Kill() => Event.HeroDeath();
    public new bool IsDead() { return Attributes.Stamina < 0; }
    public void AddQuest(Quest quest) => Journal!.AddQuest(quest);
    public void EndQuest(Quest quest, QuestStatus status) => Journal!.EndQuest(quest, status);
}
