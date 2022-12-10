﻿using Lya.AST;
using System;
using System.Collections.Generic;

namespace Lya;

class Program
{
    static void Main()
    {
        var temp = Parser.Parse(Lexer.Tokenize("int i = 3; i = 5;print(i);", "_"));
        if (temp is List<IExpression> expressions)
        {
            foreach (var expression in expressions)
                Console.WriteLine(expression);
        }
        else
            Console.WriteLine(temp);
    }
}