using System;
using System.Collections.Generic;

namespace Дроби
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "";
            string[] fractions = null;
            char sign = ' ';
            Greetings(ref text);
            SplitText(text, ref fractions, ref sign);
            Fraction fraction1 = new Fraction(fractions[0]);
            Fraction fraction2 = new Fraction(fractions[1]);
            Console.WriteLine(Operation.Calculate(fraction1, fraction2, sign));
        }
        static void Greetings(ref string text)
        {
            Console.WriteLine("Введите выражение (Деление задаётся знаком \":\"");
            text = Console.ReadLine();
        }
        static string[] SplitText(string text, ref string[] fractions, ref char sign)
        {
            if (text.Contains('+'))
            {
                fractions = text.Split("+");
                sign = '+';
            }
            else if (text.Contains('-'))
            {
                fractions = text.Split("-");
                sign = '-';
            }
            else if (text.Contains('*'))
            {
                fractions = text.Split("*");
                sign = '*';
            }
            else
            {
                fractions = text.Split(":");
                sign = '/';
            }
            return fractions;
        }
    }
    class Operation
    {
        public static string Calculate(Fraction fraction1, Fraction fraction2, char sign)
        {
            int numerator = 0;
            int denominator = 0;
            if (sign == '+')
            {
                if (fraction1.Denominator == fraction1.Denominator)
                {
                    numerator = fraction1.Numerator + fraction2.Numerator;
                    denominator = fraction1.Denominator;    
                }
                else if (fraction1.Denominator > fraction2.Denominator)
                {
                    if (fraction1.Denominator % fraction2.Denominator == 0)
                    {
                        numerator = fraction1.Numerator + fraction2.Denominator * (fraction1.Denominator / fraction2.Denominator);
                        denominator = fraction1.Denominator;
                    }
                    else
                    {
                        numerator = fraction1.Numerator * fraction2.Denominator + fraction2.Numerator * fraction1.Denominator;
                        denominator = fraction1.Denominator * fraction2.Denominator;
                    }
                }
                else
                {
                    if (fraction2.Denominator % fraction1.Denominator == 0)
                    {
                        numerator = fraction2.Numerator + fraction1.Denominator * (fraction2.Denominator / fraction1.Denominator);
                        denominator = fraction2.Denominator;
                    }
                    else
                    {
                        numerator = fraction1.Numerator * fraction2.Denominator + fraction2.Numerator * fraction1.Denominator;
                        denominator = fraction1.Denominator * fraction2.Denominator;
                    }
                }
            }
            else if (sign == '-')
            {
                if (fraction1.Denominator == fraction1.Denominator)
                {
                    numerator = fraction1.Numerator - fraction2.Numerator;
                    denominator = fraction1.Denominator;
                }
                else if (fraction1.Denominator > fraction2.Denominator)
                {
                    if (fraction1.Denominator % fraction2.Denominator == 0)
                    {
                        numerator = fraction1.Numerator - fraction2.Denominator * (fraction1.Denominator / fraction2.Denominator);
                        denominator = fraction1.Denominator;
                    }
                    else
                    {
                        numerator = fraction1.Numerator * fraction2.Denominator - fraction2.Numerator * fraction1.Denominator;
                        denominator = fraction1.Denominator * fraction2.Denominator;
                    }
                }
                else
                {
                    if (fraction2.Denominator % fraction1.Denominator == 0)
                    {
                        numerator = fraction2.Numerator - fraction1.Denominator * (fraction2.Denominator / fraction1.Denominator);
                        denominator = fraction2.Denominator;
                    }
                    else
                    {
                        numerator = fraction1.Numerator * fraction2.Denominator - fraction2.Numerator * fraction1.Denominator;
                        denominator = fraction1.Denominator * fraction2.Denominator;
                    }
                }
            }
            else if (sign == '*')
            {
                numerator = fraction1.Numerator * fraction2.Numerator;
                denominator = fraction1.Denominator * fraction2.Denominator;
            }
            else
            {
                numerator = fraction1.Numerator * fraction2.Denominator;
                denominator = fraction1.Denominator * fraction2.Numerator;
            }
            return new Fraction(numerator.ToString() + "/" + denominator.ToString()).GetReduce();
        }
    }
    class Fraction
    {
        public int Numerator { get; private set; } //Числитель
        public int Denominator { get; private set; } //Знаменатель 
        public Fraction(string text)
        {
            int i = 0; //Счётчик
            string firstNum = "0";
            string num = "";
            if (text.Contains(" "))
            {
                firstNum = "";
                while(text[i] != ' ')
                {
                    firstNum += text[i];
                    i++;
                }
            }
            i++;
            while (text[i] != '/')
            {
                num += text[i];
                i++;
            }
            Numerator = int.Parse(num);
            num = "";
            i++;
            while (i < text.Length)
            {
                num += text[i];
                i++;
            }
            Denominator = int.Parse(num);
            Numerator += int.Parse(firstNum) * Denominator;
        }
        public string GetReduce() //Вынос целого
        {
            if (Numerator / Denominator == 0)
                return $"{Numerator}/{Denominator}";
            else
            {
                if (Numerator % Denominator == 0)
                    return $"{Numerator / Denominator}";
                else
                    return $"{Numerator / Denominator} {Numerator % Denominator}/{Denominator}";
            }        
        } 
    }
}
