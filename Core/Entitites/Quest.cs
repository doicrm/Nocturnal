using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Entitites;

public enum QuestStatus { NotStarted, Running, Success, Failed, Obsolete }

public class Quest
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public QuestStatus Status { get; set; }
    public bool IsRunning { get; set; }
    public bool IsCompleted { get; set; }

    public Quest()
    {
        ID = "";
        Name = "";
        Description = "";
        Status = QuestStatus.NotStarted;
        IsRunning = false;
        IsCompleted = false;
    }

    public Quest(string id, string name, string description)
    {
        ID = id;
        Name = name;
        Description = description;
        Status = QuestStatus.NotStarted;
        IsRunning = false;
        IsCompleted = false;
    }

    public Quest(string id, string name, string description, QuestStatus status, bool isRunning, bool isCompleted)
    {
        ID = id;
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
        if (Status is QuestStatus.Running)
        {
            return $"{Globals.JsonReader!["QUEST_STATUS.RUNNING"]!.ToString().ToLower()}";
        }
        else if (Status is QuestStatus.Success)
        {
            return $"{Globals.JsonReader!["QUEST_STATUS.SUCCESS"]!.ToString().ToLower()}";
        }
        else if (Status is QuestStatus.Failed)
        {
            return $"{Globals.JsonReader!["QUEST_STATUS.FAILED"]!.ToString().ToLower()}";
        }
        else if (Status is QuestStatus.Obsolete)
        {
            return $"{Globals.JsonReader!["QUEST_STATUS.OBSOLETE"]!.ToString().ToLower()}";
        }
        return $"{Globals.JsonReader!["QUEST_STATUS.NOT_STARTED"]!.ToString().ToLower()}";
    }

    public string PrintInfo()
    {
        return $"{Globals.JsonReader!["NAME"]}: {Name}\n" +
            $"{Globals.JsonReader!["DESCRIPTION"]}: {Description}\n" +
            $"{Globals.JsonReader!["STATUS"]}: {PrintStatus()}";
    }

    public static void InsertInstances()
    {
        Quest KillHex = new("KillHex", $"{Globals.JsonReader!["QUEST.KILL_HEX.NAME"]}", $"{Globals.JsonReader!["QUEST.KILL_HEX.DESCRIPTION"]}");
        Quest ZedAccelerator = new("ZedAccelerator", $"{Globals.JsonReader!["QUEST.ZED_ACCELERATOR.NAME"]}", $"{Globals.JsonReader!["QUEST.ZED_ACCELERATOR.DESCRIPTION"]}");

        Globals.Quests[KillHex.ID] = KillHex;
        Globals.Quests[ZedAccelerator.ID] = ZedAccelerator;
    }
}
