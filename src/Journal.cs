namespace Nocturnal.src
{
    public class Journal
    {
        private List<Quest> Quests = new();

        public void AddQuest(Quest quest)
        {
            Quests.Add(quest);

            //foreach (Quest q in Quests)
            //{
            //    if (q == quest) Console.WriteLine($"\n\t{quest.Name} added to journal.\n");
            //}

            quest.Start();
            UpdatedJournalFile();
        }

        public void EndQuest(Quest quest, QuestStatus status)
        {
            if (!quest.IsCompleted)
            {
                quest.End(status);
                UpdatedJournalFile();
            }
        }

        public void UpdatedJournalFile()
        {
            string path = $"{Directory.GetCurrentDirectory()}\\journal.txt";

            using StreamWriter output = new(path);

            if (Quests.Any())
            {
                output.WriteLine($"{Globals.JsonReader!["JOURNAL.NO_QUESTS"]}");
                return;
            }

            foreach (Quest quest in Quests)
            {
                output.WriteLine(quest.PrintInfo());
                output.WriteLine("..............................................................................");
            }
        }

        public void ClearJournal()
        {
            if (Quests.Any())
            {
                Quests.Clear();
                UpdatedJournalFile();
            }
        }
    }
}
