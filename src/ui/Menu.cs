using Nocturnal.core.utils;

namespace Nocturnal.ui;

public class MenuOptions : Dictionary<string, Func<Task>>;

public class Menu
{
    private int Choice { get; set; }
    private IDictionary<int, KeyValuePair<string, Func<Task>>> Options { get; set; } = new Dictionary<int, KeyValuePair<string, Func<Task>>>();

    public Menu() { }

    public Menu(MenuOptions options)
    {
        AddOptions(options);
        ShowOptions().GetAwaiter().GetResult();
        InputChoice().GetAwaiter().GetResult();
    }

    private static async Task ActionOption(int nr, string text, int speed = 25)
        => await Display.Write($"\n\t[{nr}] {text}", speed);

    private void ShowHeroChoice()
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
        Choice = 0;
    }

    public void AddOptions(MenuOptions options)
    {
        ClearOptions();
        Options = options
            .Select((option, index) => new KeyValuePair<int, KeyValuePair<string, Func<Task>>>(index + 1, option))
            .ToDictionary(x => x.Key, x => x.Value);
    }

    public async Task ShowOptions(int seconds = 25)
    {
        if (Options.Count == 0) return;

        Console.ResetColor();
        Console.WriteLine();

        foreach (var option in Options) {
            await ActionOption(option.Key, option.Value.Key, seconds);
        }
    }

    public async Task InputChoice()
    {
        Console.WriteLine();
        while (true)
        {
            Choice = await Input.GetChoice();
            if (Options.ContainsKey(Choice))
            {
                await ExecuteSelectedAction();
                break;
            }
            Console.WriteLine("Invalid choice. Please select a valid option.");
        }
    }

    public async ValueTask<int> GetInputChoice()
    {
        Choice = await Input.GetChoice();
        return Choice;
    }

    private async Task ExecuteSelectedAction()
    {
        var selectedAction = Options[Choice].Value;
        Console.Clear();
        ShowHeroChoice();
        await selectedAction();
    }
}