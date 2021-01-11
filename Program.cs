using System;

namespace LINQTest
{
    class Program
    {
        IFileEditor fileEditor;
        IGraphics graphics;
        public Program() {
            fileEditor = new FileEditor();
            graphics = new Graphics();
        }
        static void Main()
        {
            var program = new Program();

            Console.Clear();

            program.fileEditor.MainMenuLoader();
            
            program.graphics.Header("thank you for using me");        
        }
    }
}
