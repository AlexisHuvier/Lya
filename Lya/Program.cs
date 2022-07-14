using System;
using System.Collections.Generic;
using Lya.AST;

namespace Lya
{
    class Program
    {
        private static void Main(string[] args)
        {
            var temp = Parser.Parse(Lexer.Tokenize("int i = 3; i = 5;"));
            if (temp is List<IExpression> expressions)
            {
                foreach (var expression in expressions)
                    Console.WriteLine(expression);
            }
            else
                Console.WriteLine(temp);
        }
    }
}