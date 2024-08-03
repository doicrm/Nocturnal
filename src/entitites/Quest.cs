using Nocturnal.src.core;
using Nocturnal.src.ui;
using System.Text;

namespace Nocturnal.src.entitites
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
            var sb = new StringBuilder();

            sb.AppendLine($"{Display.GetJsonString("NAME")}: {Name}");
            sb.AppendLine($"{Display.GetJsonString("DESCRIPTION")}: {Description}");
            sb.AppendLine($"{Display.GetJsonString("STATUS")}: {PrintStatus()}");

            return sb.ToString();
        }

        public static void InsertInstances()
        {
            var quests = new[]
            {
                new Quest("KillHex", Display.GetJsonString("QUEST.KILL_HEX.NAME"), Display.GetJsonString("QUEST.KILL_HEX.DESCRIPTION")),
                new Quest("ZedAccelerator", Display.GetJsonString("QUEST.ZED_ACCELERATOR.NAME"), Display.GetJsonString("QUEST.ZED_ACCELERATOR.DESCRIPTION"))
            };

            Globals.Quests = quests.ToDictionary(quest => quest.ID);
        }
    }
}
