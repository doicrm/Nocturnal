namespace Nocturnal.core;

public class GameSettings
{
    private GameLanguage Language { get; } = new();

    public GameLanguages GetLanguage() {
        return Language.GetLanguage();
    }

    public void SetLanguage(GameLanguages newLanguage) {
        Language.SetLanguage(newLanguage);
    }
}