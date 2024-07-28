using Newtonsoft.Json;

namespace Nocturnal.Core.System.Utilities
{
    public class Display
    {
        public static async Task Write(string text, int speed = 50)
        {
            foreach (char letter in text)
            {
                Console.Write(letter);
                await Task.Delay(speed);
            }
        }

        public static async Task WriteColoredText(string text, ConsoleColor color, int speed = 50)
        {
            Console.ForegroundColor = color;
            await Write(text, speed);
            Console.ResetColor();
        }

        public static async Task WriteNarration(string text, int speed = 50)
            => await WriteColoredText(text, ConsoleColor.Gray, speed);

        public static async Task WriteDialogue(string text, int speed = 50)
            => await WriteColoredText(text, ConsoleColor.White, speed);

        public static async Task<string> GetJsonStringAsync(string stringName)
        {
            try
            {
                if (Globals.JsonReader == null || !Globals.JsonReader!.ContainsKey(stringName))
                {
                    await Logger.WriteLog($"Key '{stringName}' not found in JsonReader.");
                    return string.Empty;
                }

                return Globals.JsonReader![stringName].ToString();
            }
            catch (JsonException e)
            {
                await Logger.WriteLog($"JsonException: {e.Message}");
                return string.Empty;
            }
            catch (Exception e)
            {
                await Logger.WriteLog($"Exception: {e.Message}");
                return string.Empty;
            }
        }

        public static string GetJsonString(string stringName)
        {
            return GetJsonStringAsync(stringName).GetAwaiter().GetResult();
        }
    }
}
