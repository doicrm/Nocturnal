using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Entitites;

public class Menu
{
    public int OptionNr { get; set; }
    public int Choice { get; set; }
    public IDictionary<int, KeyValuePair<string, Action>> Options { get; set; }

    public Menu()
    {
        Options = new Dictionary<int, KeyValuePair<string, Action>>();
        ClearOptions();
    }

    public Menu(Dictionary<string, Action> options)
    {
        Options = new Dictionary<int, KeyValuePair<string, Action>>();
        ClearOptions();
        AddOptions(options);
        ShowOptions();
        InputChoice();
    }

    public static void ActionOption(int nr, string text, int seconds = 25)
        => Display.Write($"\n\t[{Convert.ToString(nr)}] {text}", seconds);

    public void ShowHeroChoice()
    {
        Console.ResetColor();
        Console.WriteLine($"\n\t> {Options[Choice].Key}\n");
    }

    public void ClearOptions()
    {
        Options?.Clear();
        OptionNr = 1;
        Choice = 0;
    }

    public void AddOptions(Dictionary<string, Action> options)
    {
        ClearOptions();

        foreach (dynamic option in options)
        {
            Options.Add(OptionNr, new KeyValuePair<string, Action>(option.Key, option.Value));
            OptionNr += 1;
        }
    }

    public void ShowOptions(int seconds = 25)
    {
        if (Options.Count == 0) return;

        Console.ResetColor();

        foreach (dynamic option in Options)
        {
            ActionOption(option.Key, option.Value.Key, seconds);
        }
    }

    public void InputChoice()
    {
        Console.WriteLine();

        while (true)
        {
            Choice = Input.GetChoice();

            if (Choice <= Options.Count && Choice > 0)
            {
                CallFunction();
                break;
            }

            continue;
        }
    }

    public int GetInputChoice()
    {
        Choice = Input.GetChoice();
        return Choice;
    }

    public void CallFunction()
    {
        Action func = Options[Choice].Value;
        Console.Clear();
        ShowHeroChoice();
        func.Invoke();
    }
}
