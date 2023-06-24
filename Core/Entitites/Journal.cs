using Nocturnal.Core.Entitites.Items;
using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Entitites;

public class Journal
{
    public IList<Quest> Quests { get; set; }

    public Journal()
    {
        Quests = new List<Quest>();
    }

    public void AddQuest(Quest quest)
    {
        if (!quest.IsRunning && !quest.IsCompleted)
        {
            Quests.Add(quest);

            foreach (Quest q in Quests)
            {
                if (q == quest) quest.Start();
            }

            UpdatedJournalFile();
            SaveManager.UpdateSave();
        }
    }

    public void EndQuest(Quest quest, QuestStatus status)
    {
        if (!quest.IsCompleted)
        {
            quest.End(status);
            UpdatedJournalFile();
            SaveManager.UpdateSave();
        }
    }

    public void UpdatedJournalFile()
    {
        string path = $"{Directory.GetCurrentDirectory()}\\journal.txt";

        using StreamWriter output = new(path);

        if (IsEmpty())
        {
            output.WriteLine(Display.GetJsonString("JOURNAL.NO_QUESTS"));
            return;
        }

        foreach (Quest quest in Quests)
        {
            output.WriteLine(quest.PrintInfo());
            output.WriteLine("..............................................................................");
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

    public void ClearJournal()
    {
        if (IsEmpty())
        {
            Quests.Clear();
            UpdatedJournalFile();
            SaveManager.UpdateSave();
        }
    }
}
