using System;
using System.Text;
using System.IO;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace Конвертер_Файлов
{
    enum Operation                  //Какая операция
    {
        Encode, Decode
    }
    enum Source                     //Откуда берём файл
    {
        File, Buffer
    }
    class FileEncoder
    {
        private readonly string filePath;
        private readonly string file64Path;
        private readonly Operation operation = new Operation();
        private readonly Source source = new Source();
        public FileEncoder(string path, Operation operation, Source source)
        {
            if (operation == Operation.Encode)
            {
                filePath = path;
                file64Path = Get64Path(path);
            }
            else
            {
                file64Path = path;
                filePath = GetASCIIPath(path);
            }
            this.operation = operation;
            this.source = source;
        }
        private string TransformTo64(string stroke)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(stroke);
            return Convert.ToBase64String(buffer);
        }
        private string TransformToASCII(string stroke)
        {
            byte[] buffer = Convert.FromBase64String(stroke);
            return Encoding.ASCII.GetString(buffer);
        }
        private string Get64Path(string filepath)
        {          
            return Environment.CurrentDirectory + filePath.Replace(".", "_") + ".txt";
        }
        private string GetASCIIPath(string filepath)
        {

            return Environment.CurrentDirectory + file64Path.Replace("_", ".").Replace(".txt", "");
        }
        public void Encode()
        {
            if (source == Source.File)
            {
                string[] file = File.ReadAllLines(filePath);
                string[] stringForNewFile = new string[file.Length];
                if (operation == Operation.Encode)
                {
                    for (int i = 0; i < file.Length; i++)
                    {
                        stringForNewFile[i] = TransformTo64(file[i]);
                    }
                    File.WriteAllLines(file64Path, stringForNewFile);
                }
                else
                {                                                                                                              
                    for (int i = 0; i < file.Length; i++)
                    {
                        stringForNewFile[i] = TransformToASCII(file[i]);
                    }
                    File.WriteAllLines(filePath, stringForNewFile);
                }
            }
            else
            {
                string buffer = Clipboard.GetText();
                if (operation == Operation.Encode)
                    File.WriteAllText(file64Path, buffer);
                else
                    File.WriteAllText(filePath, buffer);
            }
        }
    }
    class Program
    {
        [STAThread]
        static void Main(string[] args)                             
        {
            Operation operation;
            Source source = Source.Buffer;
            string file = "";
            if (args.Contains("-s") || args.Contains("--source"))
            {
                if (args[3] == "file")
                    source = Source.File;
            }
            if (args.Contains("encode"))
                operation = Operation.Encode;
            else
                operation = Operation.Decode;
            if (args.Contains("-f") || args.Contains("--file"))
            {
                int index = Array.IndexOf(args, "-f");
                if (index == -1)
                    index = Array.IndexOf(args, "--file");
                file = args[index + 1];
            }
            new FileEncoder(file, operation, source).Encode();
        }                                                           
    }                                                               
}                                                                   
                                                                
                                                                
                                                                
                                                                
                                                                
                                                                