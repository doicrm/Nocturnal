﻿namespace Nocturnal.src
{
    class Display
    {
        public static void Write(string text, int speed = 50)
        {
            foreach (char letter in text)
            {
                Console.Write(letter);
                Console.Out.Flush();
                Thread.Sleep(speed);
            }
        }

        public static void WriteNarration(string text, int speed = 50)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Write(text, speed);
            Console.ResetColor();
        }

        public static void WriteDialogue(string text, int speed = 50)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Write(text, speed);
            Console.ResetColor();
        }
    }

    class Input
    {
        public static int GetChoice()
        {
            int choice;
            Display.Write("\n\t> ", 25);
            choice = Convert.ToInt32(Console.ReadLine());
            Console.Out.Flush();
            return choice;
        }

        public static string GetString()
        {
            string text;
            Display.Write("\n\t> ", 25);
            text = Convert.ToString(Console.ReadLine());
            Console.Out.Flush();
            return text;
        }
    }
}
