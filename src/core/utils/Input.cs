using Nocturnal.src.ui;

namespace Nocturnal.src.core.utils
{
    public class Input
    {
        public static async ValueTask<int> GetChoice()
        {
            await Display.Write("\t> ", 25);

            string input = await Task.Run(() => Console.ReadLine()?.Trim() ?? string.Empty);
            bool result = int.TryParse(input, out int choice);

            return result ? choice : -1;
        }

        public static async ValueTask<string> GetString()
        {
            await Display.Write("\t> ", 25);
            return await Task.Run(() => Console.ReadLine()!.Trim());
        }

        public static string ConvertToCamelCase(string input)
        {
            return string.Concat(input.Split(' ')
                .Select((word, index) => index == 0 ? word : CapitalizeFirstLetter(word)));
        }

        public static string CapitalizeFirstLetter(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            return $"{char.ToUpper(word[0])}{word.AsSpan(1).ToString()}";
        }
    }
}
