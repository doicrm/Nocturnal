using System.Reflection;

namespace Nocturnal.Core.Entitites
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

        public void SetEventNameAndType()
        {
            if (Events == null) return;

            MethodInfo methodInfo = Events.GetMethodInfo();
            string methodName = methodInfo.Name;
            EventName = methodName;

            if (methodInfo == null) return;

            Type declaringType = methodInfo.DeclaringType!;
            string typeName = declaringType.Namespace!;
            typeName += "." + declaringType.Name;
            EventType = typeName;
        }

        public void SetEvent()
        {
            if (EventType == null) return;

            Type type = Type.GetType(EventType)!;

            if (type != null && EventName != null)
            {
                MethodInfo? methodInfo = type.GetMethod(EventName!);

                if (methodInfo != null)
                {
                    Events = (Func<Task>)Delegate.CreateDelegate(typeof(Func<Task>), methodInfo);
                }
            }
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
