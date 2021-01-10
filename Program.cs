using System;

namespace LINQTest
{
    class Program
    {
        IStateHandler stateHandler;
        public Program() {
            stateHandler = new StateHandler();
        }
        static void Main()
        {
            var program = new Program();

            Console.Clear();

            Console.WriteLine("Welcome.");

            program.stateHandler.SwitchState();
            
            Console.WriteLine("\nBye bye baby.\n");        
        }
    }
}
