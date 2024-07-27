using Nocturnal.Core.System;
using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Entitites
{
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
            if (Status == QuestStatus.Running) return;

            IsCompleted = false;
            IsRunning = true;
            Status = QuestStatus.Running;

            Console.ResetColor();
            Console.WriteLine($"\n\n\t{Display.GetJsonString("NEW_QUEST")}: {Name}");
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
                return Display.GetJsonString("QUEST_STATUS.RUNNING").ToLower();
            }
            if (Status is QuestStatus.Success)
            {
                return Display.GetJsonString("QUEST_STATUS.SUCCESS").ToLower();
            }
            if (Status is QuestStatus.Failed)
            {
                return Display.GetJsonString("QUEST_STATUS.FAILED").ToLower();
            }
            if (Status is QuestStatus.Obsolete)
            {
                return Display.GetJsonString("QUEST_STATUS.OBSOLETE").ToLower();
            }
            return Display.GetJsonString("QUEST_STATUS.NOT_STARTED").ToLower();
        }

        public string PrintInfo()
        {
            return $"{Display.GetJsonString("NAME")}: {Name}\n" +
                $"{Display.GetJsonString("DESCRIPTION")}: {Description}\n" +
                $"{Display.GetJsonString("STATUS")}: {PrintStatus()}";
        }

        public static void InsertInstances()
        {
            Quest KillHex = new("KillHex", Display.GetJsonString("QUEST.KILL_HEX.NAME"), Display.GetJsonString("QUEST.KILL_HEX.DESCRIPTION"));
            Quest ZedAccelerator = new("ZedAccelerator", Display.GetJsonString("QUEST.ZED_ACCELERATOR.NAME"), Display.GetJsonString("QUEST.ZED_ACCELERATOR.DESCRIPTION"));

            Globals.Quests[KillHex.ID] = KillHex;
            Globals.Quests[ZedAccelerator.ID] = ZedAccelerator;
        }
    }
}
