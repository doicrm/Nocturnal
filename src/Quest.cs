using Nocturnal.src.Utilities;

namespace Nocturnal.src;

public enum QuestStatus { NotStarted, Running, Success, Failed, Obsolete }

public class Quest
{
    public string Name { get; set; } = "None";
    public string Description { get; set; } = "None";
    public QuestStatus Status { get; set; } = QuestStatus.NotStarted;
    public bool IsRunning { get; set; } = false;
    public bool IsCompleted { get; set; } = false;

    public Quest(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public Quest(string name, string description, QuestStatus status, bool isRunning, bool isCompleted)
    {
        Name = name;
        Description = description;
        Status = status;
        IsRunning = isRunning;
        IsCompleted = isCompleted;
    }

    public void Start()
    {
        IsCompleted = false;
        IsRunning = true;
        Status = QuestStatus.Running;

        Console.ResetColor();
        Display.Write($"{Globals.JsonReader!["JOURNAL.ENTRY"]}: {Name}");
    }

    public void End(QuestStatus status)
    {
        IsCompleted = true;
        IsRunning = false;
        Status = status;
    }

    public string PrintStatus()
    {
        if (Status == QuestStatus.Running)
        {
            return $"{Globals.JsonReader!["QUEST_STATUS.RUNNING"]!.ToString().ToLower()}";
        }
        else if (Status == QuestStatus.Success)
        {
            return $"{Globals.JsonReader!["QUEST_STATUS.SUCCESS"]!.ToString().ToLower()}";
        }
        else if (Status == QuestStatus.Failed)
        {
            return $"{Globals.JsonReader!["QUEST_STATUS.FAILED"]!.ToString().ToLower()}";
        }
        else if (Status == QuestStatus.Obsolete)
        {
            return $"{Globals.JsonReader!["QUEST_STATUS.OBSOLETE"]!.ToString().ToLower()}";
        }
        return $"{Globals.JsonReader!["QUEST_STATUS.NOT_STARTED"]!.ToString().ToLower()}";
    }

    public string PrintInfo()
    {
        return ($"{Globals.JsonReader!["NAME"]}: {Name}\n" +
            $"{Globals.JsonReader!["DESCRIPTION"]}: {Description}\n" +
            $"{Globals.JsonReader!["STATUS"]}: {PrintStatus()}");
    }
}
