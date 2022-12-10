using Lya.AST;
using System;

namespace Lya.Utils;

public static class Error
{
    public static void SendError(string name, string message, Token token, bool stop = false)
    {
        Console.WriteLine($"{name}: {message}");
        Console.WriteLine($"Token : {token}");
        if(stop)
            Environment.Exit(1);
    }

    public static void SendError(string name, string message, IExpression expression, bool stop = false)
    {
        Console.WriteLine($"{name}: {message}");
        Console.WriteLine($"Expression : {expression.GetType()} (File : {expression.File} - Line {expression.Line})");
        if(stop)
            Environment.Exit(1);
    }
}