namespace Nocturnal.Core.Entitites
{
    internal class InteractiveMenu
    {
        public bool IsSelected { get; set; } = false;
        public int OptionNr { get; set; }
        public int Choice { get; set; }
        public ConsoleKeyInfo Key { get; set; }
        public IDictionary<int, KeyValuePair<string, Func<Task>>> Options { get; private set; }

        public InteractiveMenu()
        {
            Options = new Dictionary<int, KeyValuePair<string, Func<Task>>>();
            ClearOptions();
        }

        public InteractiveMenu(Dictionary<string, Func<Task>> options)
        {
            Options = new Dictionary<int, KeyValuePair<string, Func<Task>>>();
            AddOptions(options);
            InputChoice().GetAwaiter().GetResult();
        }

        public void ActionOption(int nr, string text)
        {
            if ((Choice+1) != nr)
            {
                Console.ResetColor();
                Console.Write($"\n\t[{nr}] {text}");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write($"\n\t[{nr}] {text}");
            Console.ResetColor();
        }

        public void ShowHeroChoice()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine($"\n\t> {Options[Choice].Key}\n");
            Console.ResetColor();
        }

        public void ClearOptions()
        {
            Options.Clear();
            OptionNr = 1;
            Choice = 0;
        }

        public void AddOptions(Dictionary<string, Func<Task>> options)
        {
            ClearOptions();

            options
                .Select((option, index) => new { OptionNr = index + 1, option.Key, option.Value })
                .ToList()
                .ForEach(x => Options.Add(x.OptionNr, new KeyValuePair<string, Func<Task>>(x.Key, x.Value)));
        }

        public void ShowOptions()
        {
            if (Options.Count == 0) return;

            Console.ResetColor();
            Console.WriteLine();

            foreach (var option in Options)
                ActionOption(option.Key, option.Value.Key);
        }

        public async Task InputChoice()
        {
            Choice = 0;
            Console.WriteLine();
            (int left, int top) = Console.GetCursorPosition();

            while (!IsSelected)
            {
                Console.SetCursorPosition(left, top);

                ShowOptions();

                Key = Console.ReadKey(true);

                switch (Key.Key)
                {
                    case ConsoleKey.DownArrow:
                        Choice = Choice == (Options.Count-1) ? 0 : Choice + 1;
                        break;
                    case ConsoleKey.UpArrow:
                        Choice = Choice == 0 ? (Options.Count - 1) : Choice - 1;
                        break;
                    case ConsoleKey.Enter:
                        IsSelected = true;
                        break;
                }
            }

            Choice++;

            if (Options.ContainsKey(Choice))
                await ExecuteSelectedAction();
        }

        public async Task ExecuteSelectedAction()
        {
            var selectedAction = Options[Choice].Value;
            Console.Clear();
            ShowHeroChoice();
            await selectedAction();
        }
    }
}
