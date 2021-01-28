using System;
using System.IO;
using System.Collections.Generic;

namespace Виселица
{
    class Program
    {
        static void Main()
        {   
            Drower.Greetings();
            Console.SetCursorPosition(25, 5);
            Drower.DrowUnderscore(Game.Word);
            while (Game.Victory == 0)
            {
                Drower.DrowLifes();
                Console.SetCursorPosition(0, 1);
                Game.EnterLetter();
                Game.CheckSymbol();
                for (int i = 0; i < Game.Sym.Places.Count; i++)
                {
                    Console.SetCursorPosition(Game.Sym.Places[i], 5);
                    Drower.DrowWord();
                }
                Game.CheckVictory();
                Console.SetCursorPosition(0, 1);
                Console.WriteLine(new string(' ', 1));
            }
            Console.ReadLine();
        } 
    }
    static class Game
    {
        static string[] file = File.ReadAllLines(Environment.CurrentDirectory + "\\file.txt");
        public static string Word { get; private set; } = GetWord();
        private static string NewWord = "";
        public static Symbol Sym { get; private set; }
        public static int Lifes { get; private set; } = 3;
        public static int Victory { get; private set; }
        public static void EnterLetter()
        {
            string letter = Console.ReadLine().ToLower();
            Sym = new Symbol(char.Parse(letter), Word);
        }
        public static void CheckSymbol()
        {
            if (Word.Contains(Sym.Designation.ToString()))
            {
                if (!NewWord.Contains(Sym.Designation))
                    NewWord += new string(Sym.Designation, Sym.Places.Count);
                Sym.Guess = true;
            }
            else
                Lifes -= 1;
        }
        public static void CheckVictory()
        {
            if (Lifes == 0)
                Victory = -1;
            else if (NewWord.Length == Word.Length)
                Victory = 1;
            if (Victory == 1)
                Drower.DrowVictoryDefeat("Поздравляю, вы победили", ConsoleColor.Green);
            else if (Victory == -1)
                Drower.DrowVictoryDefeat("Сожалею, но вы повесились. Видимо, эта игра слишком сложная для вас, попробуйте лучше доту", ConsoleColor.Red);
        }
        static string GetWord()
        {
            Random random = new Random();
            int numberOfWord = random.Next(0, 3);
            return file[numberOfWord].ToLower();
        }
    }
    class Symbol
    {
        public char Designation { get; private set; }
        public bool Guess = false;
        public Symbol(char symbol, string word)
        {
            Designation = symbol;
            Places = GetPlaces(word);
        }
        public List<int> Places { get; private set; }
        public List<int> GetPlaces(string word)
        {
            List<int> places = new List<int>();
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == Designation)
                    places.Add(i + 25);
            }
            return places;
        }
    }
    class Drower
    {
        public static void DrowUnderscore(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                Console.Write('_');
            }
        }
        public static void DrowWord()
        {
            {
                if (Game.Sym.Guess)
                    Console.Write(Game.Sym.Designation);
            }
        }
        public static void Greetings()
        {
            Console.WriteLine("Приветствую в \"супер\" игре, введите букву:");
        }
        public static void DrowVictoryDefeat(string text, ConsoleColor color)
        {
            Console.Clear();
            Console.ForegroundColor = color;
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2);
            Console.WriteLine(text);
        }
        public static void DrowLifes()
        {
            Console.SetCursorPosition(0, 15);
            Console.Write($"Lifes: {Game.Lifes}");
        }
    }
}