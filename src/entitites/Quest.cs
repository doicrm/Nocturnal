using System.Text;
using Nocturnal.core;
using Nocturnal.services;

namespace Nocturnal.entitites
{
    public enum QuestStatus { NotStarted, Running, Success, Failed, Obsolete }

    public class Quest
    {
        private string Id { get; set; }
        public string Name { get; set; }
        private string Description { get; set; }
        private QuestStatus Status { get; set; }
        public bool IsRunning { get; private set; }
        public bool IsCompleted { get; private set; }

        public Quest()
        {
            Id = "";
            Name = "";
            Description = "";
            Status = QuestStatus.NotStarted;
            IsRunning = false;
            IsCompleted = false;
        }

        private Quest(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = QuestStatus.NotStarted;
            IsRunning = false;
            IsCompleted = false;
        }

        public void Start()
        {
            if (Status == QuestStatus.Running) return;

            IsCompleted = false;
            IsRunning = true;
            Status = QuestStatus.Running;

            Console.ResetColor();
            Console.WriteLine($"\n\n\t{LocalizationService.GetString("NEW_QUEST")}: {Name}");
        }

        public void End(QuestStatus status)
        {
            IsCompleted = true;
            IsRunning = false;
            Status = status;
        }

        public string PrintStatus()
        {
            return (Status switch
            {
                QuestStatus.Running => LocalizationService.GetString("QUEST_STATUS.RUNNING"),
                QuestStatus.Success => LocalizationService.GetString("QUEST_STATUS.SUCCESS"),
                QuestStatus.Failed => LocalizationService.GetString("QUEST_STATUS.FAILED"),
                QuestStatus.Obsolete => LocalizationService.GetString("QUEST_STATUS.OBSOLETE"),
                _ => LocalizationService.GetString("QUEST_STATUS.NOT_STARTED"),
            }).ToLower();
        }

        public string PrintInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{LocalizationService.GetString("NAME")}: {Name}");
            sb.AppendLine($"{LocalizationService.GetString("DESCRIPTION")}: {Description}");
            sb.AppendLine($"{LocalizationService.GetString("STATUS")}: {PrintStatus()}");

            return sb.ToString();
        }

        public static void InsertInstances()
        {
            var quests = new[]
            {
                new Quest("KillHex", LocalizationService.GetString("QUEST.KILL_HEX.NAME"), LocalizationService.GetString("QUEST.KILL_HEX.DESCRIPTION")),
                new Quest("ZedAccelerator", LocalizationService.GetString("QUEST.ZED_ACCELERATOR.NAME"), LocalizationService.GetString("QUEST.ZED_ACCELERATOR.DESCRIPTION"))
            };

            Globals.Quests = quests.ToDictionary(quest => quest.Id);
        }
    }
}
