using System;
using System.IO;
using Lya.Objects;

namespace Lya;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 1 && File.Exists(args[0]))
            Interpreter.RunFile(args[0]);
        else
        {
            var env = new Env();
            while (true)
            {
                Console.Write($"Lya {Interpreter.Version} >>> ");
                var line = Console.ReadLine();
                if (line == "exit")
                    break;
                try
                {
                    Interpreter.Run(line, env);
                }
                catch (Error.LyaErrorException) { 
                    // ignored
                }
            }
        }
    }
}