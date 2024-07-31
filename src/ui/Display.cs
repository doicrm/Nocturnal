using Nocturnal.src.services;

namespace Nocturnal.src.ui
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

        public static string GetJsonString(string stringName)
        {
            return JsonService.GetJsonString(stringName);
        }
    }
}
