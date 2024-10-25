using System.Reflection;
using Nocturnal.core;
using Nocturnal.events.prologue;
using Nocturnal.services;

namespace Nocturnal.entitites
{
    public class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Fraction? Occupation { get; set; }
        public Func<Task>? Events { get; set; }
        public bool IsVisited { get; set; }
        public string? EventName { get; set; }
        public string? EventType { get; set; }

        public Location()
        {
            Id = "";
            Name = "";
            Occupation = null;
            Events = null;
            IsVisited = false;
            EventName = "";
            EventType = "";
        }

        private Location(string id, string name, Fraction occupation, Func<Task> events)
        {
            Id = id;
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
            Id = id;
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

            EventName = methodInfo.Name;
            var declaringType = methodInfo.DeclaringType;

            if (declaringType != null) {
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
                new("DarkAlley", LocalizationService.GetString("LOCATION.DARK_ALLEY"), null!, PrologueEvents.DarkAlley),
                new("Street", LocalizationService.GetString("LOCATION.STREET"), Globals.Fractions["Police"], PrologueEvents.Street),
                new("GunShop", LocalizationService.GetString("LOCATION.GUN_SHOP"), Globals.Fractions["Police"], PrologueEvents.GunShop),
                new("NightclubEden", LocalizationService.GetString("LOCATION.NIGHTCLUB_EDEN"), Globals.Fractions["Police"], PrologueEvents.NightclubEden)
            };

            Globals.Locations = locations.ToDictionary(location => location.Id);
        }

        public dynamic ToJson()
        {
            return new
            {
                ID = Id,
                Name,
                Occupation,
                IsVisited,
                EventName,
                EventType
            };
        }
    }
}
