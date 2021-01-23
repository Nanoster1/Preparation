using System;
using System.IO;
using System.Collections.Generic;

namespace Интерпретатор
{

    class Program
    {
        static string[] file = File.ReadAllLines(Environment.CurrentDirectory + "\\interpr.txt");
        static Dictionary<string, string> nums = new Dictionary<string, string>();
        static void Main()
        {
            CheckFile();
            foreach (var num in nums)
                Console.WriteLine("{0} - {1}", num.Key, num.Value);
        }
        static void CheckFile()
        {
            for (int i = 0; i < file.Length; i++)
            {
                string[] stroke = file[i].Split(" ");
                stroke[1] = stroke[1].Replace(",", "");
                if (file[i].Contains("mov"))
                {
                    if (double.TryParse(stroke[2], out double c))
                        nums.Add(stroke[1], stroke[2]);
                    else
                        nums.Add(stroke[1], nums[stroke[2]]);
                }
                else if (file[i].Contains("add"))
                {
                    if (double.TryParse(stroke[2], out double c))
                        nums[stroke[1]] = (double.Parse(nums[stroke[1]]) + double.Parse(stroke[2])).ToString();
                    else
                        nums[stroke[1]] = (double.Parse(nums[stroke[1]]) + double.Parse(nums[stroke[2].Replace(",", "")])).ToString();
                }
                else if (file[i].Contains("sub"))
                {
                    if (double.TryParse(stroke[2], out double c))
                        nums[stroke[1]] = (double.Parse(nums[stroke[1]]) - double.Parse(stroke[2])).ToString();
                    else
                        nums[stroke[1]] = (double.Parse(nums[stroke[1]]) - double.Parse(nums[stroke[2].Replace(",", "")])).ToString();
                }
                else if (file[i].Contains("div"))
                {
                    if (double.TryParse(stroke[2], out double c))
                        nums[stroke[1]] = (double.Parse(nums[stroke[1]]) / double.Parse(stroke[2])).ToString();
                    else
                        nums[stroke[1]] = (double.Parse(nums[stroke[1]]) / double.Parse(nums[stroke[2].Replace(",", "")])).ToString();
                }
                else if (file[i].Contains("mul"))
                {   
                    if (double.TryParse(stroke[2], out double c))
                        nums[stroke[1]] = (double.Parse(nums[stroke[1]]) * double.Parse(stroke[2])).ToString();
                    else
                        nums[stroke[1]] = (double.Parse(nums[stroke[1]]) * double.Parse(nums[stroke[2].Replace(",", "")])).ToString();
                }
            }
        }
    }
}
