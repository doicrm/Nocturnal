using System.Text;
using Nocturnal.services;

namespace Nocturnal.entitites
{
    public class Journal
    {
        private List<Quest> Quests { get; set; } = [];

        public Journal()
        {
        }

        public async Task AddQuest(Quest quest)
        {
            if (quest.IsRunning || quest.IsCompleted) return;

            Quests.Add(quest);
            quest.Start();

            await UpdatedJournalFile();
            await SaveService.UpdateSave();
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
            var path = Path.Combine(Directory.GetCurrentDirectory(), "journal.txt");

            var sb = new StringBuilder();

            if (IsEmpty())
            {
                sb.AppendLine(Localizator.GetString("JOURNAL.NO_QUESTS"));
                await File.WriteAllTextAsync(path, sb.ToString());
                return;
            }

            foreach (var quest in Quests)
            {
                sb.AppendLine(quest.PrintInfo());
                sb.AppendLine("..............................................................................");
            }

            await File.WriteAllTextAsync(path, sb.ToString());
        }

        public void Show()
        {
            if (IsEmpty()) return;
            Quests.ForEach(quest => Console.WriteLine(quest.Name));
        }

        public bool IsEmpty() => Quests.Count == 0;

        public async Task ClearJournal()
        {
            if (IsEmpty()) return;
          
            Quests.Clear();
            await UpdatedJournalFile();
            await SaveService.UpdateSave();
        }
    }
}