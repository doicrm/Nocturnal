using Nocturnal.src.entitites;

namespace Nocturnal.src.services
{
    public class SaveInfoPrinter
    {
        public static string GetName(string name)
        {
            return !string.IsNullOrEmpty(name) ? name : LocalizationService.GetString("UNKNOWN");
        }

        public static string GetChapterToString(uint chapter)
        {
            if (chapter == 0)
                return LocalizationService.GetString("PROLOGUE");

            if (chapter >= 1 && chapter <= 3)
                return $"{LocalizationService.GetString("CHAPTER")} {chapter}";

            return LocalizationService.GetString("EPILOGUE");
        }

        public static string GetLocationName(Location location)
        {
            return SaveService.locationNames.TryGetValue(location.ID, out var locationKey)
                ? $": {LocalizationService.GetString(locationKey)}"
                : string.Empty;
        }
    }
}
