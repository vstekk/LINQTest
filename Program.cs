using System;

namespace LINQTest
{
    class Program
    {
        IFolderEditor folderEditor;
        IGraphics graphics;
        public Program() {
            folderEditor = new FolderEditor();
            graphics = new Graphics();
        }
        static void Main()
        {
            var program = new Program();

            Console.Clear();

            program.folderEditor.FolderMenuLoader();
            
            program.graphics.Header("thank you for using me");        
        }
    }
}
