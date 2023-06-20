namespace Nocturnal.Core.System
{
    public class StoryGlobals
    {
        public bool Bob_RecommendsZed;
        public bool PC_IsOnDanceFloor;
        public bool PC_IsAtBar;
        public bool PC_KnowsVincentCode;
        public bool PC_TalkedAboutBusinessWithZed;
        public bool Zed_KnowsAboutBobAndZed;
        public bool Zed_TellsAboutWeapons;

        private static StoryGlobals? instance = null;

        public static StoryGlobals Instance
        {
            get
            {
                instance ??= new StoryGlobals();
                return instance;
            }
        }
    }
}
