using System;

namespace Lya;

public static class Error
{
    public static void SendError(string name, string message, Token token, bool stop = false)
    {
        Console.WriteLine($"{name}: {message}");
        Console.WriteLine($"Token : {token}");
        if(stop)
            Environment.Exit(1);
    }
}