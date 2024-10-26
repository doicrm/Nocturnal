using static Nocturnal.services.JsonService;

namespace Nocturnal.services;

public class Localizator
{
    public static string GetString(string stringName) {
        return GetJsonStringAsync(stringName).ConfigureAwait(false).GetAwaiter().GetResult();
    }
}