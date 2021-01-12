using System;

interface IFileEditor 
{
    void MainMenuLoader();
}

namespace LINQTest
{
    class FileEditor : IFileEditor
    {
        IUtils utils;
        IGraphics graphics;
        bool exit = false;
        
        public FileEditor() {
            utils = new Utils();
            graphics = new Graphics();
        }
        public void MainMenuLoader()
        {
            Console.Clear();
            exit = false;
            while (exit == false) 
            {
                graphics.Header("list editor");
                utils.ReadFile();
                if (utils.currentList.Count == 0)
                {
                    graphics.Underline("Your list is empty");
                } else {
                    graphics.Underline("This is your list:");
                    utils.PrintList(utils.currentList);
                }   
                MainMenu();
            }   
        }
        private void MainMenu()
        {
            Console.WriteLine();              
            graphics.Bar(30, "-");
            Console.WriteLine("[1] Add new lines");
            Console.WriteLine("[2] Sort alphabetically");
            Console.WriteLine("[3] Sort by length");
            Console.WriteLine("[4] Items shorter than a number");
            Console.WriteLine("[5] Items longer than a number");
            Console.WriteLine("[6] Empty the list");
            Console.WriteLine("[e] Exit");
            graphics.Bar(30, "=");
            ConsoleKey response;
            do
            {   
                response = Console.ReadKey(false).Key;
            } while (response != ConsoleKey.E 
                    && response != ConsoleKey.D1 
                    && response != ConsoleKey.D2 
                    && response != ConsoleKey.D3 
                    && response != ConsoleKey.D4 
                    && response != ConsoleKey.D5 
                    && response != ConsoleKey.D6);

            switch (response)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    utils.AddItems();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    utils.SortAlphabetically();
                    utils.SaveChanges();
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    utils.SortByLength();
                    utils.SaveChanges();
                    break;
                case ConsoleKey.D4:
                    Console.Clear();
                    utils.ShorterThan();
                    utils.SaveChanges();
                    break; 
                case ConsoleKey.D5:
                    Console.Clear();
                    utils.LongerThan();
                    utils.SaveChanges();
                    break;
                case ConsoleKey.D6:
                    Console.Clear();
                    utils.ClearList();
                    utils.SaveChanges();
                    break;
                case ConsoleKey.E:
                    Console.Clear();
                    exit = true;
                    break;
            }
            Console.Clear();
        }
        
    }
}
