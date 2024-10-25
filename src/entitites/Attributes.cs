using Nocturnal.core;

namespace Nocturnal.entitites
{
    public class Attributes
    {
        public int? Body { get; set; }
        public int? Reflex { get; set; }
        public int? Technical { get; set; }
        public int? Empathy { get; set; }
        public int? Luck { get; set; }
        public int? Stamina { get; set; }

        public static Attributes Default()
        {
            return new Attributes()
            {
                Body = Constants.DefaultAttribute,
                Reflex = Constants.DefaultAttribute,
                Technical = Constants.DefaultAttribute,
                Empathy = Constants.DefaultAttribute,
                Luck = Constants.DefaultAttribute,
                Stamina = (Constants.DefaultAttribute * 6)
            };
        }
    }

}