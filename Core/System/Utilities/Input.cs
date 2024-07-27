namespace Nocturnal.Core.System.Utilities
{
    public class Input
    {
        public static async ValueTask<int> GetChoice()
        {
            await Display.Write("\t> ", 25);
            string input = await Task.Run(() => Console.ReadLine()?.Trim());
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
            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (i > 0)
                {
                    words[i] = CapitalizeFirstLetter(words[i]);
                }
            }

            return string.Join("", words);
        }

        public static string CapitalizeFirstLetter(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            char[] letters = word.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }
    }
}
