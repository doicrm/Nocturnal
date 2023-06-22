using Nocturnal.Core.Entitites;
using Nocturnal.Core.Entitites.Characters;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Events.Prologue
{
    public static class NightclubEdenEvents
    {
        // ************************************************************
        // 		NIGHTCLUB 'EDEN'
        // ************************************************************

        public static void EnterClub()
        {
            Display.WriteNarration("\n\tAfter passing through the entrance your eardrums are slowly bursting from the loud music in\n\tthe club.");
            Thread.Sleep(1000);
            Display.WriteNarration(" You walk through a short lobby and so arrive at a crowded room full of dancing\n\tpeople.");
            Thread.Sleep(1000);
            Display.WriteNarration(" Disco balls hung from the ceiling net everything around with streaks of multi-colored\n\tlight.");
            Thread.Sleep(1000);
            Display.WriteNarration(" Next to the dance floor is a bar, and behind it are several mechanically streamlined\n\tbartenders.\n\n");

            Menu enterClubMenu = new(new Dictionary<string, Action>()
            {
                { $"Go to the dance floor.", ClubDanceFloor },
                { $"Go to the bar.", ClubBar },
                { $"Go upstairs.", ClubUpstairs },
                { $"Back on the street.", PrologueEvents.VisitStreet },
            });
        }

        public static void ClubDanceFloor()
        {
            if (!Globals.Npcs["Luna"].IsKnowHero)
            {
                Display.WriteNarration("\tYou get on the dance floor.");
                Thread.Sleep(1000);
                Display.WriteNarration(" A crowd of dancing people stretches out around you, rubbing\n\tagainst each other to the rhythm of the music.");
                Thread.Sleep(1500);
                Display.WriteNarration(" Half-naked beauties are dancing in places\n\tthat look like pillars supporting the ceiling. You are not sure if they are real. There\n\tis a definite possibility that they are women, but somewhere in the back of your mind you\n\tthink they are just androids.");
                Thread.Sleep(1500);
                Display.WriteNarration("\n\tYou start dancing yourself.");
                Thread.Sleep(1000);
                Display.WriteNarration(" You are doing quite well when an attractive girl appears in\n\tfront of you. She's wearing a see-through, brightly coloured dress.");
                Thread.Sleep(1500);
                Display.WriteNarration(" Is it a coincidence\n\tthat she has just appeared and is dancing so close to you?");

                Menu danceWithLunaMenu = new(new Dictionary<string, Action>()
                {
                    { $"'What do you want?'", ClubDanceFloor_01 },
                    { $"'Hey, baby.'", ClubDanceFloor_02 },
                    { $"Keep dancing with no words.", ClubDanceFloor_03 },
                });

                LunaMeeting();
            }
            else
            {
                Display.WriteNarration("\tYou get on the dance floor.");
                Thread.Sleep(1000);
                Display.WriteNarration(" It's quite crowded, but at least you can enjoy the beautiful views. You try to keep up with\n\tthe rest of the people dancing there. However, you quickly get tired and head for the exit.");
                Thread.Sleep(1500);
                Console.Clear();
                Crossroads();
            }
        }

        public static void ClubDanceFloor_01()
        {
            Display.WriteNarration("\tThe girl turns towards you and smiles with her snow-white teeth.");
            Display.WriteDialogue("\n\t- 'Nothing will escape your attention. I am Luna. I think you can help me.'");
        }

        public static void ClubDanceFloor_02()
        {
            Display.WriteNarration("\tThe girl turns towards you and smiles with her snow-white teeth.");
            Display.WriteDialogue("\n\t- 'Hi, stud. I've been watching you since you came in. I am Luna.'");
            Display.WriteNarration("\n\tWith the last word she comes closer to you turns her back on you and starts dancing very\n\tclose to you.");
            Display.WriteDialogue("\n\t- 'I am looking for someone like you. I think you can help me.'");
        }

        public static void ClubDanceFloor_03()
        {
            Display.WriteNarration("\tThe girl turns towards you and smiles with her snow-white teeth.");
            Display.WriteDialogue("\n\t- 'Hi, I am Luna. Hex, the owner, is my boyfriend. I think you can help me.'");
        }

        public static void ClubBar()
        {
            Program.Game!.StoryGlobals.PC_IsAtBar = true;
            Display.WriteNarration("\tWith a slow step, you approach the counter, settle comfortably on a stool, and lift your\n\tgaze to the barman in front of you.");
            Display.WriteDialogue("\n\t- 'What's for you?'");

            while (true)
            {
                Menu dialogueWithBartenderMenu = new(new Dictionary<string, Action>()
                {
                    { $"'Give me anything.'", ClubBar_01 },
                    { $"'Who's in charge?'", ClubBar_02 },
                    { $"'Bye.'", ClubBar_03 },
                });

                if (dialogueWithBartenderMenu.Choice <= dialogueWithBartenderMenu.Options.Count
                    && dialogueWithBartenderMenu.Choice > 0)
                    break;
            }
        }

        public static void ClubBar_01()
        {
            Display.WriteDialogue("\t- 'Okay.");
            Thread.Sleep(1000);
            Display.WriteDialogue(" Let's see...'");
            Thread.Sleep(1500);
            Display.WriteNarration("\n\tThe barman turns his back on you and looks through the bottles of alcohol. Finally, he selects\n\tone of them and pours its contents into a glass before placing it on the counter in front of\n\tyou.");
            Thread.Sleep(1500);
            Display.WriteNarration("\n\tYou reach for the vessel and empty it.");
            Thread.Sleep(1000);
            Display.WriteNarration(" You feel a pleasant warmth spreading up your throat and further down your gullet.\n\n");
            Display.WriteDialogue("\n\t- 'Anything else?'\n\n");
        }

        public static void ClubBar_02()
        {
            Display.WriteNarration("\tThe bartender squints, hearing your question.");
            Display.WriteDialogue("\n\t- 'You're not from around here, are you?");
            Thread.Sleep(1500);
            Display.WriteDialogue(" My boss is Hex Ramsey.");
            Thread.Sleep(1000);
            Display.WriteDialogue(" He's a tough guy, so you'd better not make trouble, or it could end badly for you.'\n\n");
        }

        public static void ClubBar_03()
        {
            Display.WriteNarration("\tThe bartender reaches for a glass from under the counter and starts wiping it down.");
            Display.WriteDialogue("\n\t- 'Yeah, have fun.'\n\n");
            Crossroads();
        }

        public static void LunaMeeting()
        {
            Console.Write("\n");

            Menu lunaMeetingMenu = new(new Dictionary<string, Action>()
            {
                { $"'What is it about?'", LunaMeeting_01 },
                { $"Be silent and let her speak.", LunaMeeting_02 }
            });

            Display.WriteDialogue("\n\t- 'Hex, the owner, is my boyfriend.");
            Thread.Sleep(1000);
            Display.WriteDialogue(" I want him dead.'");
            Globals.Player.AddQuest(Globals.Quests["KillHex"]);
            Thread.Sleep(1500);
            Display.WriteNarration("\n\tLuna takes your hand and leads you towards the toilet.");
            Display.WriteDialogue("\n\t- 'Do you have a gun?'");

            if (Globals.Player.HasItem(Globals.Items["Pistol"]))
            {
                Display.WriteDialogue("\n\t- 'Good.");
                Thread.Sleep(1000);
                Display.WriteDialogue(" So you already know what and how.'");
            }
            else
            {
                Display.WriteDialogue("\n\t- 'You know how to use it, don't you?");
                Thread.Sleep(1000);
                Display.WriteDialogue(" Get it for yourself, you'll need it. Here's 200 bucks.'");
                Globals.Player.Money = 200.0f;

                Console.ForegroundColor = ConsoleColor.Green;
                Display.Write("\n\t200$");
                Console.ResetColor();
                Display.Write(" has been received.");

                Display.WriteDialogue("\n\t- 'There is a gun shop nearby.");
                Display.WriteDialogue(" It's run by a guy named Zed. Visit him before you head upstairs.");
                Thread.Sleep(1000);
                Display.WriteDialogue("\n\tI'll meet you when you've sorted this out.'");
            }
        }

        public static void LunaMeeting_01()
        {
        }

        public static void LunaMeeting_02()
        {
        }

        public static void ClubUpstairs()
        {
            Display.WriteNarration("\tYou go up a winding staircase. At the end of a short banister you will see a closed door\n\tguarded by another bulky individual.\n");

            Menu clubUpstairsMenu = new(new Dictionary<string, Action>()
            {
                { $"Come closer.", ClubUpstairs_01 },
                { $"Go back downstairs.", ClubUpstairs_02 }
            });
        }

        public static void ClubUpstairs_01()
        {
            Display.WriteNarration("\tAre you brave or foolish enough to face the hammer man. You are stopped from taking another\n\tstep by his firm voice.");
            Display.WriteDialogue("\n\t- 'What here?'");
            Globals.Npcs["Jet"].IsKnowHero = true;
            DialogueWithJet();
        }

        public static void ClubUpstairs_02()
        {
            Display.WriteNarration("\tYou don't dare to come closer, so like the last coward you turn back and return to the kingdom\n\tof loud music and dancing people.\n");
            Crossroads();
        }

        static int jetPoints = 0;
        public static void DialogueWithJet()
        {
            Console.Write("\n\n");

            //Menu menu16;

            while (true)
            {
                JetGetsAngry(jetPoints);

                //std::vector < std::pair < std::string, std::function < void() >>> options;
                //options.push_back(std::make_pair("'I want to pass.'", dialogueWithJet_1));
                //options.push_back(std::make_pair("'What is behind that door?'", dialogueWithJet_2));

                //if (Hero::heroes["Hero"].hasItem(&Item::items["Pistol"]))
                //{
                //    options.push_back(std::make_pair("Kill him with a pistol.", dialogueWithJet_3));
                //    options.push_back(std::make_pair("Stun him with a pistol.", dialogueWithJet_4));
                //}

                //options.push_back(std::make_pair("'It's time for me to go.'", dialogueWithJet_5));
                //menu16.addOptions(options);
                //menu16.showOptions();
                //menu16.inputChoice();

                //if (menu16.getChoice() <= menu16.getOptionsSize() && menu16.getChoice() > 0)
                //{
                //    break;
                //}
            }
        }

        public static void DialogueWithJet_01()
        {
            Display.WriteNarration("\tWhen you say this a big paw blocks your way.");
            Display.WriteDialogue("\n\t- 'You don't get what you're looking for here, mate.'\n\n");
            jetPoints += 1;
        }

        public static void DialogueWithJet_02()
        {
            Display.WriteNarration("\tThe security guard instinctively peeks towards the door.");
            Display.WriteDialogue("\n\t- 'You shouldn't be interested in this.'\n\n");
            jetPoints += 1;
        }

        public static void DialogueWithJet_03()
        {
            // TODO: add description of killing Jet!
            HexOffice();
        }

        public static void DialogueWithJet_04()
        {
            // TODO: add description of Jet's stun!
            HexOffice();
        }

        public static void DialogueWithJet_05()
        {
            Display.WriteDialogue("\t- 'Yeah. Get lost.'");
            Display.WriteNarration("\n\tAnd so you turn back and return to the kingdom of loud music and dancing people.\n\n");
            Crossroads();
        }


        public static void JetGetsAngry(int angerPoints)
        {
            bool jetWarnHero = false, jetBeatsHero = false;

            if (angerPoints < 2 && !jetWarnHero && !jetBeatsHero)
            {
                return;
            }

            if (angerPoints == 2 && !jetWarnHero)
            {
                jetWarnHero = true;
                Globals.Npcs["Jet"].SetAttitude(Attitudes.Angry);
                Display.WriteDialogue("\t- 'I don't like your questions. Get out of here while you still can.'\n\n");
                return;
            }

            jetBeatsHero = true;
            Globals.Npcs["Jet"].SetAttitude(Attitudes.Hostile);
            Display.WriteDialogue("\t- 'I warned you. Now we're going to have some fun.'");
            Display.WriteNarration("\n\tBefore you can blink, you get a right hook to the stomach accompanied by a left hook aimed\n\tat the jaw.");
            Thread.Sleep(2500);
            Console.Clear();
            Thread.Sleep(2500);
            WakeUpBeforeMeetingWithJet();
        }

        public static void WakeUpBeforeMeetingWithJet()
        {
            Display.WriteNarration("\n\tEmptiness...", 65);
            Thread.Sleep(1000);
            Display.WriteNarration(" Various sounds are coming from the darkness", 60);
            Thread.Sleep(1500);
            Display.WriteNarration(", getting louder by the second.");
            Thread.Sleep(1500);
            Display.WriteNarration("\n\tFinally, single colours appear before your eyes.");
            Thread.Sleep(1000);
            Display.WriteNarration(" In the darkness of the night you see the\n\toutline of a street.");
            Thread.Sleep(500);
            Display.WriteNarration("You remember only how " + Globals.Npcs["Jet"].Name + " put you down with one blow...");
            Thread.Sleep(1000);
            Display.WriteNarration("\n\tWith difficulty you pick yourself up in the ground and, walking slowly, you come to a street\n\tbathed in light.");

            Random rnd = new(); int rand = rnd.Next(0, 10);

            if (rand > 5 && rand <= 10)
            {
                RandomEvents.NickHandDiscovered();
            }

            Program.Game!.SetCurrentLocation(Globals.Locations["Street"]);
        }

        public static void HexOffice()
        {
            Display.WriteNarration("\tYou enter the manager's office immersed in twilight. In the middle of the room stands a sizable desk, and on it are stacks of documents and computer. To the left of the entrance is a window to the street below. On the right you will notice a door to another room.\n\n");

            //    Menu menu17({
            //        std::make_pair("Open the door and go into the other room.", HexHideoutCode),
            //std::make_pair("Stay and search the office.", CheckHexDesk)
            //        });
        }

        public static void CheckHexDesk()
        {
            Program.Game!.StoryGlobals.PC_KnowsHexCode = true;
            Display.WriteNarration("\n\tYou walk up to the desk. You start flipping through the e-papers one by one and finally your gaze falls on the flickering blue monitor.");
            Display.Write("\n\t");
            HexHideoutCode();
        }

        public static void HexHideoutCode()
        {
            Display.WriteNarration("\n\tYou walk closer and spot the terminal. It looks like you'll need to use a code to get through.");
            Thread.Sleep(1500);

            if (!Program.Game!.StoryGlobals.PC_KnowsHexCode)
            {
                Display.WriteNarration(" You have to look around the office though, whether you want to or not.");
            }
            else
            {
                Console.Write("\n\n");

            //    Menu menu18({
            //        std::make_pair("Use code '2021'.", nullptr),
            //std::make_pair("Search the office.", nullptr)
            //        });
            }
        }

        public static void HexHideout()
        {
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Unconscious;
            Display.WriteNarration("\n\tThe door closes behind you, hissing quietly.");
            Thread.Sleep(2000);
            Display.WriteNarration(" The room you're in is full of smaller and larger\n\tcables that merge into a single monitor that hangs above the sim-chair. On it lies a big guy\n\tin a tailored suit and a stimulation helmet on his head that obscures his face.");
            Thread.Sleep(1500);
            Display.WriteNarration(" You guess it's\n\tHex, the club owner.");
            Thread.Sleep(2000);
            Display.WriteNarration("\n\tCreeping up, you come closer. Your goal is within reach. The question is what will you do?\n\n");

            //    Menu menu19({
            //        std::make_pair("Disconnect his consciousness from the neuronet. (Kill him)", HexHideout_01),
            //std::make_pair("Wait for his consciousness to leave the neuronet.", HexHideout_02)
            //        });
        }

        public static void HexHideout_01()
        {
            Display.WriteNarration("\tYou lean over Hex and, in a fluid motion without hesitation, pull the stimulation helmet\n\toff his head. You witness the nightclub owner being shaken by a wave of convulsions. Foam\n\tbegins to come out of his mouth and after a moment the man freezes.");
            Thread.Sleep(1500);
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Dead;
            Display.WriteNarration(" He's probably dead, just like\n\tLuna wanted.");
        }

        public static void HexHideout_02()
        {
            Display.WriteNarration("\tYou are not a coward.");
            Thread.Sleep(1000);
            Display.WriteNarration(" You don't stab people in the back. That's not your style. You prefer an\n\topen fight.");
            Thread.Sleep(1500);
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Normal;
            Display.WriteNarration(" After a while, you notice Hex moving slightly in his seat. He opens his eyes -\n\tthey are shining with the excitement of his online adventure.");
            Thread.Sleep(1000);
            Display.WriteNarration(" It is only a matter of time before\n\tHex notices your presence. There is no turning back now.");
            Display.WriteDialogue("\n\t- 'What the fuck are you doing here, dickhead?'\n");
            DialogueWithHex();
        }

        public static void DialogueWithHex()
        {
            Console.Write("\n");

        //    Menu menu20({
        //        std::make_pair("'Die!'", dialogueWithHex_1),
        //std::make_pair("'I don't want to fight with you.'", dialogueWithHex_2),
        //std::make_pair("'Luna wants you dead.'", dialogueWithHex_3)
        //        });

        //    Console.Write("\n\n");

        //    Menu menu21({
        //        std::make_pair("'It doesn't matter.'", dialogueWithHex_4),
        //std::make_pair("'Your girlfriend, Luna.'", dialogueWithHex_5)
        //        });

            Display.WriteNarration("\n\tThe passage behind your back is opened.");
            Thread.Sleep(1000);
            Display.WriteNarration(" You turn around, in front of you is Luna.");
            Display.WriteDialogue("\n\t- 'Did you do what I asked you to do...'");
            Thread.Sleep(1500);
            Display.WriteNarration("\n\tThe girl's gaze wanders from you to the body of her ex-boyfriend behind you.");
            Thread.Sleep(1000);
            Display.WriteDialogue("\n\t- 'You did it... You really did it... Is he - is he dead?'");
            Thread.Sleep(1000);
            Display.WriteNarration("\n\tLuna walks past you and kneels by the dead man. She starts searching his pockets for something.\n\n");

        //    Menu menu22({
        //        std::make_pair("Do nothing.", dialogueWithHex_6),
        //std::make_pair("'What is this all about?'", dialogueWithHex_7)
        //        });

            HexResurrection();
        }

        public static void DialogueWithHex_01()
        {
            Display.WriteNarration("\n\tWith a shout, you bring out your gun and aim it at Hex's chest.");
            Display.WriteDialogue("\n\t- 'Are you such a hero?");
            Thread.Sleep(1000);
            Display.WriteDialogue(" Would you kill an unarmed man?");
            Thread.Sleep(1000);
            Display.WriteDialogue(" All right, can I at least find out who\n\tsent you? I know you're not working alone. I'm seeing you for the first time in my life.'");
        }

        public static void DialogueWithHex_02()
        {
            Display.WriteNarration("\n\tYou are not a coward.");
            Thread.Sleep(1000);
            Display.WriteNarration(" You don't stab people in the back. That's not your style. You prefer an\n\topen fight.");
        }

        public static void DialogueWithHex_03()
        {
            Display.WriteNarration("\n\tYou are not a coward. You don't stab people in the back. That's not your style. You prefer an open fight.");
        }

        public static void DialogueWithHex_04()
        {
            Display.WriteDialogue("\n\t- 'Ugh, you little prick. I'll get you from beyond the grave. There will be no peace. I won't let that happen!'");
            Thread.Sleep(1500);
            HexDeath();
        }

        public static void DialogueWithHex_05()
        {
            Display.WriteDialogue("\n\t- 'Traitorous bitch! She'll get her due someday.");
            Display.WriteDialogue(" All right, shithead, let's get this over with.'");
            HexDeath();
        }

        public static void DialogueWithHex_06()
        {
            Display.WriteNarration("\n\tYou let the girl quietly plunder the corpse. This gives you more time to look at her shapes more closely.");
            Display.WriteDialogue("\n\t- 'Spare me, if you please.'");
        }

        public static void DialogueWithHex_07()
        {
            Display.WriteDialogue("\n\t- 'Well, now I think I owe you an explanation.'");
            Display.WriteNarration("\n\tA girl talks to you without even looking at you.\n");
        }

        public static void HexDeath()
        {
            Display.WriteNarration("\n\tThe last word spoken synchronizes with the bang of a gunshot as a bullet of energy pierces the club owner's chest.");
            Thread.Sleep(1000);
            Display.WriteNarration(" The recoil knocks him from his seat. The lifeless body clatters against the floor.");
            Thread.Sleep(1500);
            Display.WriteNarration(" He's dead, just like Luna wanted.");
            Globals.Npcs["HexFolstam"].Status = NpcStatus.Dead;
            Display.WriteNarration("\n\tOut of curiosity, you walk closer and spot the corpse holding a small pistol.");
            Thread.Sleep(1000);
            Display.WriteNarration(" That bastard was playing for time after all!");
        }

        public static void HexResurrection()
        {
            Display.WriteNarration("\n\tSuddenly, the maze of cables begins to vibrate and move in a strange dance.");
            Thread.Sleep(1000);
            Display.WriteNarration(" Hisses reach you and Luna, forming a gibberish that is difficult to understand.");
            Console.ForegroundColor = ConsoleColor.Blue;
            Display.Write("\n\t<You scum! You thought you got rid of me.");
            Thread.Sleep(1000);
            Display.Write(" But you didn't.");
            Thread.Sleep(1000);
            Display.Write(" I'm immortal now!>");
            Display.WriteNarration("\n\tYou exchange a look with Luna. You both can't believe what's happening.");
            Display.WriteDialogue("\n\t- 'What the fuck?!'");
            Console.ForegroundColor = ConsoleColor.Blue;
            Display.Write("\n\t<Before your new lover killed me, sweatheart, I managed to pour some of my consciousness into the net. I don't need my body anymore, nothing limits me anymore.");
            Thread.Sleep(1500);
            Console.ForegroundColor = ConsoleColor.Blue;
            Display.Write(" Now you will get what you deserve. The time for payment has come!");
            Thread.Sleep(1000);
            Display.Write(" DIE!>");
            Thread.Sleep(3000);
            Event.EndGame();
        }

        public static void Crossroads()
        {
            Menu enterClubMenu = new(new Dictionary<string, Action>()
            {
                { $"Go to the dance floor.", ClubDanceFloor },
                { $"Go to the bar.", ClubBar },
                { $"Go upstairs.", ClubUpstairs },
                { $"Back on the street.", PrologueEvents.VisitStreet },
            });
        }
    }
}
