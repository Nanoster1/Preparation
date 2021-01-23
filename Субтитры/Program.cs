using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace Субтитры
{
    class Word
    {
        private string Text;
        private ConsoleColor Color;
        private string Place;
        private string Start;
        private string End;
        public static void CheckFile(string[] file, List<Word> words)
        {
            for (int i = 0; i < file.Length; i++)
            {
                Word word = new Word();
                string[] str = file[i].Split(" ");
                word.Start = "00:" + str[0];
                word.End = "00:" + str[2];
                if (str[3].Contains("["))
                {
                    word.Place = str[3];
                    switch (str[4])
                    {
                        case ("Red]"):
                            word.Color = ConsoleColor.Red;
                            break;
                        case ("Green]"):
                            word.Color = ConsoleColor.Green;
                            break;
                        case ("Blue]"):
                            word.Color = ConsoleColor.Blue;
                            break;
                    }
                    word.Text = str[5];
                    for (int k = 6; k < str.Length; k++)
                    {
                        word.Text += " " + str[k];
                    }
                }
                else
                {
                    word.Place = "[Bottom,";
                    word.Color = ConsoleColor.White;
                    word.Text = str[3];
                    for (int k = 4; k < str.Length; k++)
                    {
                        word.Text += " " + str[k];
                    }
                }
                words.Add(word);
            }
        }
        public static void DrowWord(List<Word> words)
        {
            int i = 0;
            int k = 0;
            DateTime start = DateTime.Now;
            while (k < words.Count)
            {
                string[] date = (DateTime.Now - start).ToString().Split('.');
                if (date[0] == words[i].Start)
                {
                    if (i == words.Count - 1)
                        Drowing.DrowWord(words[i].Place, words[i].Color, words[i].Text);
                    else
                    {
                        Drowing.DrowWord(words[i].Place, words[i].Color, words[i].Text);
                        i++;
                    }
                }
                if (date[0] == words[k].End)
                {
                    Drowing.CleanWord(words[k].Place, words[k].Color, words[k].Text);
                    k++;
                }
            }
        }
    }
    class Program
    {
        static string[] file = File.ReadAllLines(Environment.CurrentDirectory + "\\file.txt");
        static List<Word> words = new List<Word>();
        static void Main()
        {
            Console.CursorVisible = false;
            Drowing.DrowRam();
            Word.CheckFile(file, words);
            Word.DrowWord(words);
        }
    }
    class Drowing
    {
        public static void DrowRam()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 20, Console.WindowHeight / 2 - 5);
            Console.Write("┌");
            Console.Write(new string('─', 40));
            Console.Write("┐");
            int k = 4;
            for (int i = 1; i < 10; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 20, Console.WindowHeight / 2 + k);
                Console.Write("│");
                Console.Write(new string(' ', 40));
                Console.Write("│");
                k--;
            }
            Console.SetCursorPosition(Console.WindowWidth / 2 - 20, Console.WindowHeight / 2 + 5);
            Console.Write("└");
            Console.Write(new string('─', 40));
            Console.Write("┘");
        }
        public static void DrowWord(string place, ConsoleColor color, string word)
        {
            switch (place)
            {
                case ("[Top,"):
                    Console.SetCursorPosition(Console.WindowWidth / 2 - word.Length / 2, Console.WindowHeight / 2 - 4);
                    Console.ForegroundColor = color;
                    Console.WriteLine(word);
                    break;
                case ("[Bottom,"):
                    Console.SetCursorPosition(Console.WindowWidth / 2 - word.Length / 2, Console.WindowHeight / 2 + 4);
                    Console.ForegroundColor = color;
                    Console.WriteLine(word);
                    break;
                case ("[Right,"):
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 20 - word.Length, Console.WindowHeight / 2);
                    Console.ForegroundColor = color;
                    Console.WriteLine(word);
                    break;
                case ("[Left,"):
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 20 + word.Length, Console.WindowHeight / 2);
                    Console.ForegroundColor = color;
                    Console.WriteLine(word);
                    break;
            }
        }
        public static void CleanWord(string place, ConsoleColor color, string word)
        {
            switch (place)
            {
                case ("[Top,"):
                    Console.SetCursorPosition(Console.WindowWidth / 2 - word.Length / 2, Console.WindowHeight / 2 - 4);
                    Console.ForegroundColor = color;
                    Console.Write(new string(' ', word.Length));
                    break;
                case ("[Bottom,"):
                    Console.SetCursorPosition(Console.WindowWidth / 2 - word.Length / 2, Console.WindowHeight / 2 + 4);
                    Console.ForegroundColor = color;
                    Console.Write(new string(' ', word.Length));
                    break;
                case ("[Right,"):
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 20 - word.Length, Console.WindowHeight / 2);
                    Console.ForegroundColor = color;
                    Console.Write(new string(' ', word.Length));
                    break;
                case ("[Left,"):
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 20 + word.Length, Console.WindowHeight / 2);
                    Console.ForegroundColor = color;
                    Console.Write(new string(' ', word.Length));
                    break;
            }
        }
    }
}
