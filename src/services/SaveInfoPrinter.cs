using Nocturnal.entitites;

namespace Nocturnal.services;

public static class SaveInfoPrinter
{
    public static string GetName(string name) {
        return !string.IsNullOrEmpty(name) ? name : Localizator.GetString("UNKNOWN");
    }

    public static string GetChapterToString(uint chapter)
    {
        return chapter switch
        {
            0 => Localizator.GetString("PROLOGUE"),
            >= 1 and <= 3 => $"{Localizator.GetString("CHAPTER")} {chapter}",
            _ => Localizator.GetString("EPILOGUE")
        };
    }

    public static string GetLocationName(Location location)
    {
        var locationKey = "LOCATION." + 
                          string.Concat(location.Id.Select(c => char.IsUpper(c) ? "_" + c : c.ToString()))
                              .TrimStart('_')
                              .ToUpper();
        return $": {Localizator.GetString(locationKey)}";
    }
}