using Nocturnal.events;

namespace Nocturnal.entitites
{
    public class Player : Npc
    {
        public float Money { get; set; }
        public Weapon? Weapon { get; set; }
        public readonly Journal? Journal;

        public Player() : base()
        {
            Id = "Player";
            Money = 0.0f;
            Weapon = null;
            Journal = new Journal();
        }

        public Player(float money, Weapon weapon, /*Clothes clothes,*/ Inventory inventory, Journal journal) : base()
        {
            Id = "Player";
            Money = money;
            Weapon = weapon;
            //Clothes = clothes;
            Inventory = inventory;
            Journal = journal;
        }

        public static async Task Kill() => await Event.HeroDeath();
        public new bool IsDead() { return Attributes.Stamina < 0; }

        public async Task AddQuest(Quest quest)
            => await Journal!.AddQuest(quest);

        public async Task EndQuest(Quest quest, QuestStatus status)
            => await Journal!.EndQuest(quest, status);
    }
}
