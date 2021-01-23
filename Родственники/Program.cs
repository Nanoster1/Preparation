using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Podgotovka
{
    class Person
    {
        private int Id;
        private string BirthDate;
        private string FirstName;
        private string LastName;
        private bool Parent = false;
        public static void Check(string[] info, ref List<Person> people)
        {
            int numString = 0; //Отслеживаем номера строк
            string[] instruction = info[0].Split(";");
            for (int i = 1; !string.IsNullOrWhiteSpace(info[i]); i++)
            {
                string[] inform = info[i].Split(";");
                Person person = new Person();
                for (int k = 0; k < 4; k++)
                {
                    switch (instruction[k])
                    {
                        case ("Id"):
                            person.Id = Convert.ToInt32(inform[k]);
                            break;
                        case ("BirthDate"):
                            person.BirthDate = inform[k];
                            break;
                        case ("FirstName"):
                            person.FirstName = inform[k];
                            break;
                        case ("LastName"):
                            person.LastName = inform[k];
                            break;
                    }
                }
                people.Add(person);
                numString = i + 2;
            }
            for (int i = numString; info.Length > i; i++)
            {
                string[] inform = info[i].Split("=");
                string[] inform2 = inform[0].Split("<->");
                int id = int.Parse(inform2[0]);
                int id2 = int.Parse(inform2[1]);
                string role = inform[1];
                switch (role)
                {
                    case ("parent"):
                        people[id - 1].Parent = true;
                        break;
                    case ("spouse"):
                        people[id - 1].Parent = true;
                        people[id2 - 1].Parent = true;
                        break;
                }
            }
        }
        public static void Finding(string first, string second, List<Person> people)
        {
            int firstId = FoundPerson(first.Split(" "), people); //Ivanov Ivan
            int secondId = FoundPerson(second.Split(" "), people);
            if (people[firstId].Parent && people[secondId].Parent)
                Console.WriteLine("Spouse");
            else if (people[firstId].Parent && !people[secondId].Parent)
                Console.WriteLine("Parent");
            else if (!people[firstId].Parent && !people[secondId].Parent)
                Console.WriteLine("Sibling");
        }
        private static int FoundPerson(string[] first, List<Person> people)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].FirstName == first[1] && people[i].LastName == first[0])
                    return i;
            }
            return 0;
        }
    }
    class Program
    {
        static string[] info = File.ReadAllLines(Environment.CurrentDirectory + "\\Info.txt");
        static List<Person> people = new List<Person>();
        static void Main()
        {
            Person.Check(info, ref people);
            string first = "";
            string second = "";
            Greetings(ref first, ref second);
            Person.Finding(first, second, people);
        }

        static void Greetings(ref string first, ref string second)
        {
            Console.WriteLine("Введите 1-го Человека");
            first = Console.ReadLine();
            Console.WriteLine("Введите 2-го Человека");
            second = Console.ReadLine();
        }
    }
}