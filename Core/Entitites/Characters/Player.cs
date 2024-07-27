﻿using Nocturnal.Core.Entitites.Properties;
using Nocturnal.Core.Entitites.Items;
using Nocturnal.Core.Events;

namespace Nocturnal.Core.Entitites.Characters
{
    public class Player : Npc
    {
        public float Money { get; set; }
        public Weapon? Weapon { get; set; }
        public Journal? Journal = new();

        public Player() : base()
        {
            ID = "Player";
            Money = 0.0f;
            Weapon = null;
            Journal = new();
        }

        public Player(float money, Weapon weapon, /*Clothes clothes,*/ Inventory inventory, Journal journal) : base()
        {
            ID = "Player";
            Money = money;
            Weapon = weapon;
            //Clothes = clothes;
            Inventory = inventory;
            Journal = journal;
        }

        public static async Task Kill() => await Event.HeroDeath();
        public new bool IsDead() { return Attributes.Stamina < 0; }
        public async Task AddQuest(Quest quest) => await Journal!.AddQuest(quest);
        public async Task EndQuest(Quest quest, QuestStatus status) => await Journal!.EndQuest(quest, status);
    }
}
