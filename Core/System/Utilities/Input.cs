namespace Nocturnal.Core.System.Utilities
{
    public class Input
    {
        public static int GetChoice()
        {
            Display.Write("\t> ", 25);
            bool result = int.TryParse(Console.ReadLine()?.Trim(), out int choice);
            if (result)
                return choice;
            else
                return -1;
        }

        public static string GetString()
        {
            Display.Write("\t> ", 25);
            string text = Console.ReadLine()!.Trim();
            return text;
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
