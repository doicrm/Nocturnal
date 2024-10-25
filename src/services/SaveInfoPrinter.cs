using Nocturnal.entitites;

namespace Nocturnal.services
{
    public static class SaveInfoPrinter
    {
        public static string GetName(string name)
        {
            return !string.IsNullOrEmpty(name) ? name : LocalizationService.GetString("UNKNOWN");
        }

        public static string GetChapterToString(uint chapter)
        {
            return chapter switch
            {
                0 => LocalizationService.GetString("PROLOGUE"),
                >= 1 and <= 3 => $"{LocalizationService.GetString("CHAPTER")} {chapter}",
                _ => LocalizationService.GetString("EPILOGUE")
            };
        }

        public static string GetLocationName(Location location)
        {
            return SaveService.LocationNames.TryGetValue(location.Id, out var locationKey)
                ? $": {LocalizationService.GetString(locationKey)}"
                : string.Empty;
        }
    }
}
