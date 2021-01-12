using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

interface IFolderEditor
{
    void FolderMenuLoader();

}
namespace LINQTest
{
    class FolderEditor : IFolderEditor
    {
        IFileEditor editor;
        IUtils utils;
        IGraphics graphics;
        public string filePath;
        List<string> files = new List<string>();
        bool exit = false;

        public FolderEditor()
        {
            utils = new Utils();
            graphics = new Graphics();
            editor = new FileEditor();
        }

        public void FolderMenuLoader()
        {
            if (!Directory.Exists("lists"))
            {
                Directory.CreateDirectory("lists");
            }
            while (exit == false)
            {
                graphics.Header("your files");
                CheckFolder();
                FolderMenu();
            }
            
        }
        private void FolderMenu()
        {
            Console.WriteLine();
            graphics.Bar(30, "-");
            Console.WriteLine("[1] Open list");
            Console.WriteLine("[2] Create new list");
            Console.WriteLine("[3] Delete list");
            Console.WriteLine("[e] Exit");
            graphics.Bar(30, "-");
            ConsoleKey response;
            do 
            {
                response = Console.ReadKey(false).Key;
            }   while (response != ConsoleKey.E 
                    && response != ConsoleKey.D1 
                    && response != ConsoleKey.D2 
                    && response != ConsoleKey.D3);
            switch (response)
            {
                case ConsoleKey.D1:
                    OpenFile();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    CreateFile();
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    DeleteFile();
                    break;
                case ConsoleKey.E:
                    Console.Clear();
                    Exit();
                    break;
            }
            Console.Clear();
        }

        private void SetFilePath(string filename)
        {
            filePath = @"lists\" + filename + ".txt";
        }
        private void CheckFolder()
        {
            var lists = from i in Directory.EnumerateFiles(@"lists\")
                select Path.GetFileNameWithoutExtension(i);
                files = lists.ToList();

            if (files.Count == 0)
            {
                graphics.Underline("You have no lists.");
            } else {
                graphics.Underline("Your lists:");
                utils.PrintList(files);
            }
            
        }
        private void OpenFile()
        {
            string filename = Console.ReadLine();

            if (files.Contains(filename))
            {
                SetFilePath(filename);
                
            } else {
                graphics.Underline(filename + " does not exist. Do you want to create it?");
                if (utils.YesOrNoBlock())
                {
                    SetFilePath(filename);
                    File.Create(filePath);
                    
                } else return;
            }
            editor.MainMenuLoader();
        }
        private void CreateFile()
        {
            graphics.Header("new list");
            graphics.Underline("Enter list name:");
            string filename = Console.ReadLine();
            graphics.Underline("Create " + filename + " ?");
            if (utils.YesOrNoBlock())
            {
                SetFilePath(filename);
                File.Create(filePath);
            }
            editor.MainMenuLoader();
        }
        private void DeleteFile()
        {
            graphics.Header("delete file");
            graphics.Underline("Enter a name of a list you want to delete:");
            string filename = Console.ReadLine();
            if (files.Contains(filename))
            {
                SetFilePath(filename);
                File.Delete(utils.filePath);
            } else {
                graphics.Underline("File does not exist.");
                Console.Write("Press any key to continue. ");
                Console.ReadKey();
            }
        }
        
        private void Exit() {
            {
                graphics.Header("leaving");
                Console.Write("Are you sure you want to exit?");
                if (utils.YesOrNoBlock())
                {
                    exit = true;
                }
            }
        }
    }
}