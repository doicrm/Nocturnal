using Nocturnal.Core.Entitites;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue
{
    public static class GunShopEvents
    {
        // ************************************************************
        // 		ZED'S GUN SHOP
        // ************************************************************

        public static void EnterGunShop()
        {
            Display.WriteNarration("\n\tThe front door hisses open before you.");

            if (!Globals.Npcs["Zed"].IsKnowHero)
            {
                Thread.Sleep(1000);
                Display.WriteNarration(" You step over the threshold and enter a small room\n\twith a counter opposite the entrance. Behind it stands a tall, thin man with fatigue painted\n\ton his terribly oblong face.");
                Thread.Sleep(1500);
                Display.WriteNarration(" He is dressed in an old corporate commando\n\tsuit. On the wall behind his back hangs a lot of weapons.");
                Display.WriteDialogue("\n\t- 'How can I help you, my friend?'");
                Globals.Npcs["Zed"].IsKnowHero = true;
                DialogueWithZed();
            }
            else
            {
                Thread.Sleep(1000);
                Display.WriteNarration(" From behind the counter, Zed is already smiling at\n\tyou.");

                if (Globals.Player.HasItem(Globals.Items["Pistol"]))
                    Display.WriteDialogue("\n\t- 'What's up? How's the gun working out?'");

                Console.WriteLine();
                DialogueWithZed();
            }
        }

        public static void DialogueWithZed()
        {
            while (true)
            {
                Console.WriteLine();

                Menu dialogueWithZedMenu = new();
                dialogueWithZedMenu.ClearOptions();
                Dictionary<string, Action> options = new()
                {
                    { "'What do you have?'", DialogueWithZed_01 },
                    { "'How's business going?'", DialogueWithZed_02 }
                };

                if (Program.Game!.StoryGlobals.Bob_RecommendsZed && !Program.Game!.StoryGlobals.Zed_KnowsAboutBobAndZed)
                    options.Add("'You're Zed? I come from Bob.'", DialogueWithZed_03);

                if (Globals.Quests["ZedAccelerator"].IsRunning && Globals.Player.HasItem(Globals.Items["AD13"]))
                    options.Add("'I have an accelerator for you.'", DialogueWithZed_04);

                options.Add("'I have to go...'", DialogueWithZed_05);
                dialogueWithZedMenu.AddOptions(options);
                dialogueWithZedMenu.ShowOptions();
                dialogueWithZedMenu.InputChoice();

                if (dialogueWithZedMenu.Choice <= dialogueWithZedMenu.Options.Count && dialogueWithZedMenu.Choice > 0)
                    break;
            }
        }

        public static void DialogueWithZed_01()
        {
            //Console.WriteLine();
            ZedTrade();
        }

        public static void DialogueWithZed_02()
        {
            if (Program.Game!.StoryGlobals.PC_TalkedAboutBusinessWithZed)
                Display.WriteDialogue("\t- 'Hey, what's up? Are you sclerotic or something? We already talked about this, haha!'\n");
            else
            {
                Program.Game!.StoryGlobals.PC_TalkedAboutBusinessWithZed = true;
                Display.WriteDialogue("\t- 'What kind of question is that anyway? Business is doing great! Everyone stops by\n\tevery now and then to rearm. It's the natural order of things.'");
            }

            DialogueWithZed();
        }

        public static void DialogueWithZed_03()
        {
            Program.Game!.StoryGlobals.Zed_KnowsAboutBobAndZed = true;
            Display.WriteDialogue("\t- 'Yes, that is correct. I'm Zed, and this is my little shop.");
            Thread.Sleep(1000);
            Display.WriteDialogue(" Since you know Bob,\n\tyou can get a small discount here.'");
            Globals.Npcs["Zed"].SetAttitude(Attitudes.Friendly);
            DialogueWithZed();
        }

        public static void DialogueWithZed_04()
        {
            ZedGetsAnAccelerator();
            DialogueWithZed();
        }

        public static void DialogueWithZed_05()
        {
            Display.WriteDialogue("\t- 'No problem. See you later!'\n");
            Console.Clear();

            if (!Globals.Npcs["Caden"].IsKnowHero && !Globals.Npcs["CadensPartner"].IsKnowHero)
                StreetEvents.MeetingWithPolicemans();
            else
                Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }

        public static void ZedGetsAnAccelerator()
        {
            Globals.Player.RemoveItem(Globals.Items["AD13"]);
            Display.WriteDialogue("\n\t- 'Well done!");
            Thread.Sleep(1000);
            Display.WriteDialogue(" I don't know where you found it, but now the gun is yours.'\n");

            Globals.Player.AddItem(Globals.Items["Pistol"]);
            Console.ForegroundColor = ConsoleColor.Blue;
            Display.Write("\n\t" + Globals.Items["AD13"].Name);
            Console.ResetColor();
            Display.Write(" given and ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Display.Write(Globals.Items["Pistol"].Name!);
            Console.ResetColor();
            Display.Write(" gained.\n");

            Globals.Player.EndQuest(Globals.Quests["ZedAccelerator"], QuestStatus.Success);
        }

        public static void ZedTrade()
        {
            if (!Globals.Player.HasItem(Globals.Items["Pistol"]))
            {
                if (!Program.Game!.StoryGlobals.Zed_TellsAboutWeapons)
                {
                    Program.Game!.StoryGlobals.Zed_TellsAboutWeapons = true;
                    Display.WriteDialogue("\t- 'Better ask what I don't have!");
                    Thread.Sleep(1000);
                    Display.WriteDialogue(" Look - ");
                    Thread.Sleep(1000);
                    Display.WriteDialogue("rifles, pistols, machine guns, shotguns. I\n\thave a melee weapons as well. Knives, hammers, long blades like katanas...");
                    Thread.Sleep(1500);
                    Display.WriteDialogue(" Anything\n\tyou want, my friend!");
                    Thread.Sleep(1500);
                    Display.WriteDialogue(" Tell me, what do you like?'\n");

                    Menu buyPistolMenu = new(new Dictionary<string, Action>()
                    {
                        { $"Buy: pistol (250 $).", BuyPistol },
                        { $"'I have made up my mind.'", ZedTrade_01 }
                    });
                }
                else
                {
                    Console.WriteLine();

                    Menu buyPistolMenu = new(new Dictionary<string, Action>()
                    {
                        { $"Buy: pistol (250 $).", BuyPistol },
                        { $"'I have made up my mind.'", ZedTrade_01 }
                    });
                }
            }
            else
            {
                Console.ResetColor();
                Display.Write("\tYou have everything you need. There's no point in bothering Zed.");
                DialogueWithZed();
            }
        }

        public static void ZedTrade_01()
        {
            Display.WriteNarration("\tZed looks at you pityingly and shrugs his shoulders.");
            Display.WriteDialogue("\n\t- 'No problem. It can happen to anyone.'");
            DialogueWithZed();
        }

        public static void BuyPistol()
        {
            if (Globals.Player.Money != 250.0f)
            {
                Display.WriteDialogue("\n\t- 'I see you're low on cash.");
                Thread.Sleep(1000);
                Display.WriteDialogue(" But don't worry, we'll sort it out somehow.");
                Thread.Sleep(1500);

                if (Program.Game!.StoryGlobals.Zed_KnowsAboutBobAndZed)
                {
                    Display.WriteDialogue(" Hmm, you know Old Bob, that already means something.");
                    Thread.Sleep(1000);
                    Display.WriteDialogue(" Let's just say I'll loan you this gun on a friendly basis.");

                    Globals.Player.AddItem(Globals.Items["Pistol"]);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Display.Write("\n\t" + Globals.Items["Pistol"].Name);
                    Console.ResetColor();
                    Display.Write(" gained.\n");
                }
                else
                {
                    Display.WriteDialogue(" It so happens that\n\tI have been looking for a good accelerator for some time.");
                    Thread.Sleep(1000);
                    Display.WriteDialogue(" I don't mean the crap produced by\n\tcorporations these days.");
                    Thread.Sleep(1000);
                    Display.WriteDialogue(" I mean the good old accelerator!");
                    Thread.Sleep(1500);
                    Display.WriteDialogue(" Find me such a device and you will\n\tget that gun. Okay?");
                    Globals.Player.AddQuest(Globals.Quests["ZedAccelerator"]);
                }
            }
            else
            {
                Display.WriteDialogue("\n\t- 'A pistol is a good start. Here, it's yours.'");

                Globals.Player.AddItem(Globals.Items["Pistol"]);
                Console.ForegroundColor = ConsoleColor.Blue;
                Display.Write("\n\t" + Globals.Items["Pistol"].Name);
                Console.ResetColor();
                Display.Write(" was bought.\n");
            }
        }

        public static void Crossroads()
        {
            EnterGunShop();
        }
    }
}
