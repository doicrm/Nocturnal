using Nocturnal.src.core;

namespace Nocturnal.src.entitites
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
                Body = Constants.DEFAULT_ATTRIBUTE,
                Reflex = Constants.DEFAULT_ATTRIBUTE,
                Technical = Constants.DEFAULT_ATTRIBUTE,
                Empathy = Constants.DEFAULT_ATTRIBUTE,
                Luck = Constants.DEFAULT_ATTRIBUTE,
                Stamina = (Constants.DEFAULT_ATTRIBUTE * 6)
            };
        }
    }

}