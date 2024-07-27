﻿using Nocturnal.Core.System.Utilities;

namespace Nocturnal.Core.Entitites
{
    public class Menu
    {
        public int OptionNr { get; set; }
        public int Choice { get; set; }
        public IDictionary<int, KeyValuePair<string, Func<Task>>> Options { get; private set; }

        public Menu()
        {
            Options = new Dictionary<int, KeyValuePair<string, Func<Task>>>();
            ClearOptions();
        }

        public Menu(Dictionary<string, Func<Task>> options)
        {
            Options = new Dictionary<int, KeyValuePair<string, Func<Task>>>();
            AddOptions(options);
            ShowOptions().GetAwaiter().GetResult();
            InputChoice().GetAwaiter().GetResult();
        }

        public static async Task ActionOption(int nr, string text, int seconds = 25)
            => await Display.Write($"\n\t[{nr}] {text}", seconds);

        public void ShowHeroChoice()
        {
            Console.ResetColor();
            Console.WriteLine($"\n\t> {Options[Choice].Key}\n");
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

            foreach (var option in options)
            {
                Options.Add(OptionNr, new KeyValuePair<string, Func<Task>>(option.Key, option.Value));
                OptionNr += 1;
            }
        }

        public async Task ShowOptions(int seconds = 25)
        {
            if (Options.Count == 0) return;

            Console.ResetColor();
            Console.WriteLine();

            foreach (var option in Options)
            {
                await ActionOption(option.Key, option.Value.Key, seconds);
            }
        }

        public async Task InputChoice()
        {
            //Console.WriteLine();

            //while (true)
            //{
            //    Choice = Input.GetChoice();

            //    if (Choice <= Options.Count && Choice > 0)
            //    {
            //        ExecuteSelectedAction();
            //        break;
            //    }

            //    continue;
            //}

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

        public async Task ExecuteSelectedAction()
        {
            //Action selectedAction = Options[Choice].Value;
            //Console.Clear();
            //ShowHeroChoice();
            //selectedAction.Invoke();

            Func<Task> selectedAction = Options[Choice].Value;
            Console.Clear();
            ShowHeroChoice();
            await selectedAction();
        }
    }
}
