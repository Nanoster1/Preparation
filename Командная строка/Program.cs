using System;
using System.Collections.Generic;
using System.IO;

namespace Командная_строка
{
    class Program
    {
        static void Main()
        {
            Drower.ShowFiles(Catalogue.catalog);
            Drower.ShowFolders(Catalogue.catalog);
            while (true)
            {
                string text = Console.ReadLine();
                Commands.Command = text;
                Commands.RunCommand(Catalogue.catalog);
            }
        }
    }
    static class Catalogue
    {
        public static Catalog catalog = new Catalog(Environment.CurrentDirectory);
    }
    static class Drower
    {
        public static void ShowFiles(Catalog catalog)
        {
            foreach (FileInfo file in catalog.Files)
            {
                string[] fileStr = file.ToString().Split("/");
                Console.WriteLine(fileStr[fileStr.Length - 1]);
            }
        }
        public static void ShowFolders(Catalog catalog)
        {
            foreach (DirectoryInfo directory in catalog.Catalogs)
            {
                Console.WriteLine(directory);
            }
        }
    }
    class Catalog
    {
        public string Path { get; private set; }
        public DirectoryInfo Directory { get; private set; }
        public FileInfo[] Files { get; private set; }
        public DirectoryInfo[] Catalogs { get; private set; }
        public Catalog(string path)
        {
            Path = path;
            Directory = new DirectoryInfo(path);
            Files = Directory.GetFiles();
            Catalogs = Directory.GetDirectories();
        }
    }
    static class Commands
    {
        static public string Command { get; set; }
        static public void EnterCommand(string command)
        {
            Command = command;
        }
        static public void RunCommand(Catalog catalog)
        {
            if (Command.Contains("cd"))
            {
                Catalogue.catalog = RunCd(catalog, Command);
                Drower.ShowFiles(catalog);
                Drower.ShowFolders(catalog);
            }
            else if (Command.Contains("new"))
            {
                string file = Command.Split(" ")[1];
                CreateFile(catalog, file);
            }
            else
            {
                string file = Command.Split(" ")[1];
                DeleteFile(catalog, file);
            }
        }
        private static Catalog RunCd(Catalog catalog, string text)
        {
            if (Command.Contains("."))
            {
                catalog = new Catalog(catalog.Directory.Parent.ToString());
                return catalog;
            }
            else
            {
                string[] oldStrings = catalog.Path.Split(" ");
                string oldFolder = oldStrings[1];                     
                string[] newStrings = text.Split(" ");
                string newFolder = newStrings[1];
                catalog = new Catalog(catalog.Path.Replace(oldFolder, newFolder));
                return catalog;
            }
        }
        private static void CreateFile(Catalog catalog, string file)
        {
            File.Create(catalog.Path + "\\" + file);
        }
        private static void DeleteFile(Catalog catalog, string file)
        {
            File.Delete(catalog.Path + "\\" + file);
        }
    }
}
