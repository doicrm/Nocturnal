namespace Nocturnal.Core.Entitites;

public class Location
{
    public string Name { get; set; }
    public Fraction? Occupation { get; set; }
    public Action Events { get; set; }
    public bool IsVisited { get; set; }

    public Location(string name, Fraction occupation, Action events)
    {
        Name = name;
        Occupation = occupation;
        Events = events;
        IsVisited = false;
    }
}
