using System;

interface IFileEditor {
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
            while (exit == false) 
            {
                utils.ReadFile();
                if (utils.currentList.Count == 0)
                {
                    graphics.Underline("-", "Your list is empty.");
                } else {
                    graphics.Underline("-", "This is your list:");
                    utils.PrintList(utils.currentList);
                }   
                MainMenu();
            }   
        }
        private void MainMenu()
        {
            ConsoleKey response;
            do
            {   
                Console.WriteLine();              
                graphics.Underline("=", "What do you want to do?");
                Console.WriteLine("[1] Add new lines");
                Console.WriteLine("[2] Sort alphabetically");
                Console.WriteLine("[3] Sort by length");
                Console.WriteLine("[4] Filter out words shorter than a number");
                Console.WriteLine("[5] Filter out words longer than a number");
                Console.WriteLine("[6] Empty the list");
                Console.WriteLine("[e] Exit");

                response = Console.ReadKey(true).Key;
                Console.WriteLine($"\nSelected option: " + response + ".");
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
                    utils.ClearList();
                    break;
                case ConsoleKey.E:
                    exit = true;
                    break;
            }
            Console.Clear();
        }
    }
}
