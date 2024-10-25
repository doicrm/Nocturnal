namespace Nocturnal.core
{
    public class StoryGlobals
    {
        ///////////////////////////////////////////////////////////////////////
        //	PROLOGUE
        ///////////////////////////////////////////////////////////////////////

        public bool Bob_RecommendsZed;
        public bool PC_IsOnDanceFloor;
        public bool PC_IsAtBar;
        public bool PC_KnowsHexCode;
        public bool PC_TalkedAboutBusinessWithZed;
        public bool Zed_KnowsAboutBobAndZed;
        public bool Zed_TellsAboutWeapons;
        public int Jet_Points;
        public bool Jet_WarnedPlayer;
        public bool Jet_BeatedPlayer;


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
