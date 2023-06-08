using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Entitites;

public class Menu
{
    private int OptionNr = 1;
    private int Choice = 0;
    private readonly Dictionary<int, KeyValuePair<string, Action>> Options = new();

    public Menu(Dictionary<string, Action> options)
    {
        ClearOptions();
        AddOptions(options);
        ShowOptions();
        InputChoice();
    }

    public static void ActionOption(int nr, string text)
        => Display.Write($"\n\t[{Convert.ToString(nr)}] {text}", 25);

    public void ShowHeroChoice()
    {
        Console.ResetColor();
        Console.WriteLine($"\n\t> {Options[Choice].Key}\n\n");
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

    public void ShowOptions()
    {
        if (Options.Count == 0) return;

        Console.ResetColor();

        foreach (dynamic option in Options)
        {
            ActionOption(option.Key, option.Value.Key);
        }
    }

    public void InputChoice()
    {
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
