using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LINQTest
{
    class Program
    {
        string filePath = @"obj\list.txt";
        List<string> inputList = new List<string>();
        List<string> newList = new List<string>();
        bool hasEnough = false;
        bool confirmation = false;
        int letters;
        static void Main()
        {
            var program = new Program();
            Console.Clear();
            Console.WriteLine("Hi. Welcome to this groundbreaking experience.");
            
            program.OperationSelector();
            Console.WriteLine("\nBye bye baby.\n");        
        }
        private void OperationSelector()
        {
            ReadFile();
            if (inputList.Count == 0)
            {
                AddItemsToList();
            } else {
                Console.WriteLine("\nThis is your list:");
                PrintList(inputList);

                ConsoleKey response;
                do
                {
                    Console.WriteLine("\nWhat do you want to do?");
                    Console.WriteLine("[1] Sort alphabetically");
                    Console.WriteLine("[2] Sort by length");
                    Console.WriteLine("[3] Filter out words shorter than a number");
                    Console.WriteLine("[4] Filter out words longer than a number");
                    Console.WriteLine("[5] Add new lines");
                    Console.WriteLine("[6] Delete the list");
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
                
                if (response == ConsoleKey.D1)
                {
                    SortAlphabetically();
                    SaveChanges();
                } 
                if (response == ConsoleKey.D2)
                {
                    SortByLength();
                    SaveChanges();
                } 
                if (response == ConsoleKey.D3)
                {
                    ShorterThan();
                    SaveChanges();
                } 
                if (response == ConsoleKey.D4)
                {
                    LongerThan();
                    SaveChanges();
                } 
                if (response == ConsoleKey.D5)
                {
                    hasEnough = false;
                    AddItemsToList();
                }
                if (response == ConsoleKey.D6)
                {
                    StartOver();
                }
            }
        }
        private void ReadFile()
        {
            inputList.Clear();
            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        inputList.Add(line);
                    }
                }
            } catch (Exception e) {
                Console.WriteLine("\nThe file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
        private void AddItemsToList()
        {
            newList.Clear();
            while (!hasEnough)
            {
                Console.WriteLine("\nWrite a word:");
                newList.Add(Console.ReadLine());

                Console.Write("\nDo you want to add another word? ");
                ConfirmChoice();
                if (confirmation == false)
                {
                    hasEnough = true;
                }

            }
            if (inputList.Count == 0) 
            {
                OverwriteList();
            } else {
                Console.WriteLine("\nYou are going to add this to your list:");
                PrintList(newList);
                Console.Write("\nAre you sure? ");
                ConfirmChoice();
                if (confirmation == true)
                {
                    System.IO.File.AppendAllLines(filePath, newList);
                } 
            }
            OperationSelector();
        }
        private void OverwriteList()
        {
            System.IO.File.WriteAllLines(filePath, newList);
        }
        private void SaveChanges()
        {
            Console.Write("\nDo you want to save changes to your list? ");
            ConfirmChoice();
            if (confirmation == true)
            {
                OverwriteList();
            }
            OperationSelector();
        }
        private void SortAlphabetically()
        {
            IEnumerable<string> sorted = 
                from i in inputList
                orderby i
                select i;
            newList = sorted.ToList();
            Console.WriteLine("\nThis is your list sorted alphabetically:");
            PrintList(sorted);
        }
        private void SortByLength()
        {
            IEnumerable<string> sorted =
                from i in inputList
                orderby i.Length
                select i;
            newList = sorted.ToList();
            Console.WriteLine("\nThis is your list sorted by length:");
            PrintList(sorted);
        }
        private void ShorterThan()
        {
            InputNumber();
            IEnumerable<string> sorted =
                from i in inputList
                where i.Length < letters
                select i;
            newList = sorted.ToList();
            Console.WriteLine($"\nThese words are shorter than " + letters + ":");
            PrintList(sorted);
        }
        private void LongerThan()
        {
            InputNumber();
            IEnumerable<string> sorted =
                from i in inputList
                where i.Length > letters
                select i;
            newList = sorted.ToList();
            Console.WriteLine($"\nThese words are longer than " + letters + ":");
            PrintList(sorted);
        }
        private void StartOver()
        {
            Console.Write("\nAre you sure you want to empty your list? ");
            ConfirmChoice();
            if (confirmation == true)
            {
                hasEnough = false;
                newList.Clear();
                OverwriteList();
                Console.WriteLine("\nYour list is now empty.");
                AddItemsToList();
            } else {
                OperationSelector();
            }
        }
        private void ConfirmChoice()
        {
            Console.Write(" [y/n]  ");
            ConsoleKey response;
            do 
            {
                response = Console.ReadKey(false).Key;
            } while (response != ConsoleKey.Y && response != ConsoleKey.N);
            if (response == ConsoleKey.Y) 
            {
                confirmation = true;
            } else {
                confirmation = false;
            }
            Console.WriteLine();
        }
        private void InputNumber()
        {
            bool succesfull = false;
            do
            {
                Console.Write("\nChoose a number: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int number))
                {
                    letters = number;
                    succesfull = true;
                } else {
                    Console.WriteLine("This is not a number. Try again.");
                }
            } while (succesfull == false);
        }
        private void PrintList(IEnumerable<string> list)
        {
            foreach (string i in list)
            {
                Console.WriteLine(i);
            }
        }
         private void PrintList(List<string> list)
        {
            foreach (string i in list)
            {
                Console.WriteLine(i);
            }
        }
    }
}
