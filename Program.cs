using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQTest
{
    class Program
    {
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
                    Console.WriteLine("[1] to sort alphabetically");
                    Console.WriteLine("[2] to sort by length");
                    Console.WriteLine("[3] to filter out words shorter than a number");
                    Console.WriteLine("[4] to filter out words longer than a number");
                    Console.WriteLine("[5] to add more words to your list");
                    Console.WriteLine("[6] to reset your list and start all over");
                    Console.WriteLine("[e] to exit this tremendous app");

                    response = Console.ReadKey(true).Key;
                    Console.WriteLine($"\nIn your infinite wisdom you have chosen " + response + ".");
                } while (response != ConsoleKey.E && response != ConsoleKey.D1 && response != ConsoleKey.D2 && response != ConsoleKey.D3 && response != ConsoleKey.D4 && response != ConsoleKey.D5 && response != ConsoleKey.D6);
                
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
                    inputList = inputList.Concat(newList).ToList();
                } 
            }
            OperationSelector();
        }
        private void OverwriteList()
        {
            inputList.Clear();
            foreach (string i in newList)
                {
                    inputList.Add(i);
                }
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
            Console.WriteLine($"\nThese words are shorter than " + letters + ".");
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
            Console.WriteLine($"\nThese words are longer than " + letters + ".");
            PrintList(sorted);
        }
        private void StartOver()
        {
            Console.Write("Are you sure you want to empty your list? ");
            ConfirmChoice();
            if (confirmation == true)
            {
                hasEnough = false;
                inputList.Clear();
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
