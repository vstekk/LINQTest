using System;

interface IGraphics
{
    void Header(string text);
    void Underline(string character, string text);
    void Bar(int length, string character);

}
namespace LINQTest
{
    class Graphics : IGraphics
    {
        public void Header(string text)
        {
            Bar(text.Length + 10, "*");
            Console.Write("     ");
            Console.WriteLine(text.ToUpper());
            Bar(text.Length + 10, "*");
            Console.WriteLine();
        }
        public void Underline(string character, string text)
        {
            Console.WriteLine(text);
            Bar(text.Length, character);
        }
        public void Bar(int length, string character)
        {
            if (character.Length > 1)
            {
                character = character.Remove(1);
            }
            for (int i = 0; i < length; i++ )
            {
                Console.Write(character);
            }
            Console.WriteLine();            
        }


    }
}