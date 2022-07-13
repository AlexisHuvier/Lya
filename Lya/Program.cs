using System;

namespace Lya
{
    class Program
    {
        private static void Main(string[] args)
        {
            foreach (var token in Lexer.Tokenize("int i = \"test\";"))
                Console.WriteLine(token);
        }
    }
}