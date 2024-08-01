namespace Nocturnal.src.core
{
    public class GameSettings
    {
        public GameLanguage Language { get; private set; }

        public GameSettings() { Language = new GameLanguage(); }

        public GameLanguages GetLanguage()
        {
            return Language.GetLanguage();
        }

        public void SetLanguage(GameLanguages newLanguage)
        {
            Language.SetLanguage(newLanguage);
        }
    }
}
