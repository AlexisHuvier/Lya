using System;
using Lya.AST;
using Lya.Objects.TokenObjects;

namespace Lya;

public static class Error
{
    public class LyaErrorException: Exception {}
    
    public static void SendError(string name, string message, Token token, bool stop = false)
    {
        Console.WriteLine($"{name}: {message}");
        Console.WriteLine($"Token : {token}");
        if(stop)
            throw new LyaErrorException();
    }

    public static void SendError(string name, string message, Expression expression, bool stop = false)
    {
        Console.WriteLine($"{name}: {message}");
        Console.WriteLine($"Expression : {expression.GetType()} (File : {expression.File} - Line {expression.Line})");
        if(stop)
            throw new LyaErrorException();
    }
}