using Nocturnal.src.services;
using Nocturnal.src.ui;
using System.Text;

namespace Nocturnal.src.entitites
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
                await SaveService.UpdateSave();
            }
        }

        public async Task EndQuest(Quest quest, QuestStatus status)
        {
            if (quest.IsCompleted) return;

            quest.End(status);
            await UpdatedJournalFile();
            await SaveService.UpdateSave();
        }

        public async Task UpdatedJournalFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "journal.txt");

            var sb = new StringBuilder();

            if (IsEmpty())
            {
                sb.AppendLine(Display.GetJsonString("JOURNAL.NO_QUESTS"));
                await File.WriteAllTextAsync(path, sb.ToString());
                return;
            }

            foreach (Quest quest in Quests)
            {
                sb.AppendLine(quest.PrintInfo());
                sb.AppendLine("..............................................................................");
            }

            await File.WriteAllTextAsync(path, sb.ToString());
        }

        public void Show()
        {
            if (IsEmpty()) return;

            foreach (var quest in Quests)
            {
                Console.WriteLine(quest.Name);
            }
        }

        public bool IsEmpty() => !Quests.Any();

        public async Task ClearJournal()
        {
            if (IsEmpty()) return;
          
            Quests.Clear();
            await UpdatedJournalFile();
            await SaveService.UpdateSave();
        }
    }
}