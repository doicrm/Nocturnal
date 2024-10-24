﻿using Nocturnal.core;

namespace Nocturnal.ui
{
    public static class Display
    {
        public static async Task Write(string text, int speed = 50)
        {
            foreach (var letter in text)
            {
                Console.Write(letter);
                await Task.Delay(speed).ConfigureAwait(false);
            }
        }

        public static async Task WriteColoredText(string text, ConsoleColor color, int speed = 50)
        {
            Console.ForegroundColor = color;
            await Write(text, speed).ConfigureAwait(false);
            Console.ResetColor();
        }

        public static async Task WriteNarration(string text, int speed = 50) {
            await WriteColoredText(text, ConsoleColor.Gray, speed);
        }

        public static async Task WriteDialogue(string text, int speed = 50) {
            await WriteColoredText(text, ConsoleColor.White, speed);
        }

        public static async Task WriteLogo()
        {
            Console.WriteLine();
            foreach (var s in Constants.GameLogo)
                await WriteColoredText(s, ConsoleColor.Blue, 1);
        }

        public static async Task LoadLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            foreach (var s in Constants.GameLogo)
                Console.Write(s);
            Console.ResetColor();
            await Game.Instance.MainMenu().ConfigureAwait(false);
        }
    }
}
