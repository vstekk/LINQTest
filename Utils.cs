using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

interface IUtils {
    string filePath {get; set; }
    List<string> currentList {get; set; }
    void ReadFile();
    void ClearFile();
    void SaveChanges();
    void AddItems();
    void SortAlphabetically();
    void SortByLength();
    void ShorterThan();
    void LongerThan();
    void PrintList(List<string> list);

}

namespace LINQTest
{
    class Utils : IUtils
    {
        
        public string filePath {get; set;}
        public List<string> currentList {get; set; }
        List<string> newList = new List<string>();
        bool confirmation = false;
        int letters;

        public Utils() {
            filePath = @"obj\list.txt";
            currentList = new List<string>();

        }
        public void ReadFile()
        {
            currentList.Clear();
            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        currentList.Add(line);
                    }
                }
            } catch (Exception e) {
                Console.WriteLine("\nThe file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        public void ClearFile()
        {
            Console.Write("\nAre you sure? ");
            YesOrNoBlock();
            if (confirmation == true)
            {                
                newList.Clear();
                OverwriteFile();
                Console.WriteLine("\nYour list is now empty.");
            }
        }
        public void AddItems()
        {
            newList.Clear();
            bool hasEnough = false;
            
            while (!hasEnough)
            {
                Console.WriteLine("\nWrite a word:");
                newList.Add(Console.ReadLine());

                Console.Write("\nDo you want to add another word? ");
                YesOrNoBlock();
                if (confirmation == false)
                {
                    hasEnough = true;
                }
            }
            Console.WriteLine("\nYou are going to add this to your list:");
            PrintList(newList);

            Console.Write("\nAre you sure? ");
            YesOrNoBlock();
            if (confirmation == true)
            {
                AppendToFile();
            }
        }
        public void SortAlphabetically()
        {
            IEnumerable<string> sorted = 
                from i in currentList
                orderby i
                select i;
            newList = sorted.ToList();
            Console.WriteLine("\nThis is your list sorted alphabetically:");
            PrintList(sorted);
        }
        public void SortByLength()
        {
            IEnumerable<string> sorted =
                from i in currentList
                orderby i.Length
                select i;
            newList = sorted.ToList();
            Console.WriteLine("\nThis is your list sorted by length:");
            PrintList(sorted);
        }
        public void ShorterThan()
        {
            InputNumber();
            IEnumerable<string> sorted =
                from i in currentList
                where i.Length < letters
                select i;
            newList = sorted.ToList();
            Console.WriteLine($"\nThese words are shorter than " + letters + ":");
            PrintList(sorted);
        }
        public void LongerThan()
        {
            InputNumber();
            IEnumerable<string> sorted =
                from i in currentList
                where i.Length > letters
                select i;
            newList = sorted.ToList();
            Console.WriteLine($"\nThese words are longer than " + letters + ":");
            PrintList(sorted);
        }
        public void SaveChanges()
        {
            Console.Write("\nSave changes? ");
            YesOrNoBlock();
            if (confirmation == true)
            {
                OverwriteFile();
            }
        }
        private void AppendToFile()
        {
            System.IO.File.AppendAllLines(filePath, newList);
        }
        private void OverwriteFile()
        {
            System.IO.File.WriteAllLines(filePath, newList);
        }
        private void YesOrNoBlock()
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
         public void PrintList(List<string> list)
        {
            foreach (string i in list)
            {
                Console.WriteLine(i);
            }
        }
    }
}