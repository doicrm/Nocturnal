using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Entitites
{
    public class Journal
    {
        public IList<Quest> Quests { get; set; }

        public Journal()
        {
            Quests = new List<Quest>();
        }

        public async Task AddQuest(Quest quest)
        {
            if (!quest.IsRunning && !quest.IsCompleted)
            {
                Quests.Add(quest);

                foreach (Quest q in Quests)
                {
                    if (q == quest) quest.Start();
                }

                await UpdatedJournalFile();
                await SaveManager.UpdateSave();
            }
        }

        public async Task EndQuest(Quest quest, QuestStatus status)
        {
            if (quest.IsCompleted) return;

            quest.End(status);
            await UpdatedJournalFile();
            await SaveManager.UpdateSave();
        }

        public async Task UpdatedJournalFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "journal.txt");

            using (StreamWriter output = new StreamWriter(path))
            {
                if (IsEmpty())
                {
                    await output.WriteLineAsync(Display.GetJsonString("JOURNAL.NO_QUESTS"));
                    return;
                }

                foreach (Quest quest in Quests)
                {
                    await output.WriteLineAsync(quest.PrintInfo());
                    await output.WriteLineAsync("..............................................................................");
                }
            }
        }

        public void Show()
        {
            if (IsEmpty()) return;

            foreach (var quest in Quests)
            {
                Console.WriteLine(quest.Name);
            }
        }

        public bool IsEmpty()
        {
            return !Quests.Any();
        }

        public async Task ClearJournal()
        {
            if (IsEmpty()) return;
          
            Quests.Clear();
            await UpdatedJournalFile();
            await SaveManager.UpdateSave();
        }
    }
}