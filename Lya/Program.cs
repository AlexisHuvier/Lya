using System.IO;

namespace Lya;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 1 && File.Exists(args[0]))
            Interpreter.Run(args[0]);
    }
}