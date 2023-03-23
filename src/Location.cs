namespace Nocturnal.src
{
    internal class Location
    {
        private string Name { get; set; }
        private Fraction Occupation { get; set; }
        public Action Events { get; set; }
        private bool IsVisited { get; set; }

        public Location (string name, Fraction occupation, Action events)
        {
            Name = name;
            Occupation = occupation;
            Events = events;
            IsVisited = false;
        }
    }
}
