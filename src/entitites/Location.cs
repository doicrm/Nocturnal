using Nocturnal.src.core;
using Nocturnal.src.events.prologue;
using Nocturnal.src.ui;
using System.Reflection;

namespace Nocturnal.src.entitites
{
    public class Location
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Fraction? Occupation { get; set; }
        public Func<Task>? Events { get; set; }
        public bool IsVisited { get; set; }
        public string? EventName { get; set; }
        public string? EventType { get; set; }

        public Location()
        {
            ID = "";
            Name = "";
            Occupation = null;
            Events = null;
            IsVisited = false;
            EventName = "";
            EventType = "";
        }

        public Location(string id, string name, Fraction occupation, Func<Task> events)
        {
            ID = id;
            Name = name;
            Occupation = occupation;
            Events = events;
            IsVisited = false;
            EventName = "";
            EventType = "";
            SetEventNameAndType();
        }

        public Location(string id, string name, Fraction occupation, Func<Task> events, bool isVisited)
        {
            ID = id;
            Name = name;
            Occupation = occupation;
            Events = events;
            IsVisited = isVisited;
            EventName = "";
            EventType = "";

            SetEventNameAndType();
            SetEvent();
        }

        private void SetEventNameAndType()
        {
            if (Events == null) return;

            var methodInfo = Events.GetMethodInfo();
            if (methodInfo == null) return;

            EventName = methodInfo.Name;
            var declaringType = methodInfo.DeclaringType;

            if (declaringType != null)
            {
                EventType = $"{declaringType.Namespace}.{declaringType.Name}";
            }
        }

        public void SetEvent()
        {
            if (string.IsNullOrEmpty(EventType) || string.IsNullOrEmpty(EventName))
            {
                Events = null;
                return;
            }

            var type = Type.GetType(EventType);
            if (type == null)
            {
                Events = null;
                return;
            }

            var methodInfo = type.GetMethod(EventName);
            if (methodInfo != null)
            {
                Events = (Func<Task>)Delegate.CreateDelegate(typeof(Func<Task>), methodInfo);
                return;
            }

            Events = null;
        }

        public static void InsertInstances()
        {
            var locations = new List<Location>
            {
                new("DarkAlley", Display.GetJsonString("LOCATION.DARK_ALLEY"), null!, PrologueEvents.DarkAlley),
                new("Street", Display.GetJsonString("LOCATION.STREET"), Globals.Fractions["Police"], PrologueEvents.Street),
                new("GunShop", Display.GetJsonString("LOCATION.GUN_SHOP"), Globals.Fractions["Police"], PrologueEvents.GunShop),
                new("NightclubEden", Display.GetJsonString("LOCATION.NIGHTCLUB_EDEN"), Globals.Fractions["Police"], PrologueEvents.NightclubEden)
            };

            Globals.Locations = locations.ToDictionary(location => location.ID);
        }

        public dynamic ToJson()
        {
            return new
            {
                ID,
                Name,
                Occupation,
                IsVisited,
                EventName,
                EventType
            };
        }
    }
}
