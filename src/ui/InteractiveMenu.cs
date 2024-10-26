namespace Nocturnal.ui;

public class InteractiveMenu
{
    private bool IsSelected { get; set; }
    public int OptionNr { get; set; }
    private int Choice { get; set; }
    private ConsoleKeyInfo Key { get; set; }
    private IDictionary<int, KeyValuePair<string, Func<Task>>> Options { get; set; } = new Dictionary<int, KeyValuePair<string, Func<Task>>>();


    public InteractiveMenu() => ClearOptions();

    public InteractiveMenu(MenuOptions options)
    {
        AddOptions(options);
        InputChoice().GetAwaiter().GetResult();
    }

    private void ActionOption(int nr, string text)
    {
        var isSelectedOption = Choice + 1 == nr;
        Console.ForegroundColor = isSelectedOption ? ConsoleColor.Black : Console.ForegroundColor;
        Console.BackgroundColor = isSelectedOption ? ConsoleColor.White : Console.BackgroundColor;
        Console.Write($"\n\t[{nr}] {text}");
        Console.ResetColor();
    }

    private void ShowHeroChoice()
    {
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

    public void AddOptions(MenuOptions options)
    {
        ClearOptions();
        Options = options.Select((option, index) => new KeyValuePair<int, KeyValuePair<string, Func<Task>>>(index + 1, option)).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public void ShowOptions()
    {
        if (Options.Count == 0) return;

        Console.ResetColor();
        Console.WriteLine();

        foreach (var option in Options) {
            ActionOption(option.Key, option.Value.Key);
        }
    }

    public async Task InputChoice()
    {
        Choice = 0;
        Console.WriteLine();
        var (left, top) = Console.GetCursorPosition();

        while (!IsSelected)
        {
            Console.SetCursorPosition(left, top);

            ShowOptions();

            Key = Console.ReadKey(true);

            switch (Key.Key)
            {
                case ConsoleKey.DownArrow:
                    Choice = Choice == (Options.Count - 1) ? 0 : (Choice + 1);
                    break;
                case ConsoleKey.UpArrow:
                    Choice = Choice == 0 ? (Options.Count - 1) : (Choice - 1);
                    break;
                case ConsoleKey.Enter:
                    IsSelected = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        while (!IsSelected)
        {
            Console.SetCursorPosition(left, top);
            ShowOptions();

            Key = Console.ReadKey(true);
            switch (Key.Key)
            {
                case ConsoleKey.DownArrow:
                    Choice = (Choice + 1) % Options.Count;
                    break;
                case ConsoleKey.UpArrow:
                    Choice = (Choice - 1 + Options.Count) % Options.Count;
                    break;
                case ConsoleKey.Enter:
                    IsSelected = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        Choice++;

        if (Options.ContainsKey(Choice)) {
            await ExecuteSelectedAction();
        }
    }

    private async Task ExecuteSelectedAction()
    {
        Console.Clear();
        ShowHeroChoice();
        await Options[Choice].Value();
    }
}