namespace Nocturnal.core;

public class GameSettings
{
    private GameLanguage Language { get; set; } = new();

    public GameLanguages GetLanguage() {
        return Language.GetLanguage();
    }

    public void SetLanguage(GameLanguages newLanguage) {
        Language.SetLanguage(newLanguage);
    }
}