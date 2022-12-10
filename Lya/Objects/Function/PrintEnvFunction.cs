using Lya.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lya.Objects.Function;

public class PrintEnvFunction: IFunction
{
    public string Name => "printenv";

    public dynamic Eval(Env env, IEnumerable<dynamic> arguments)
    {
        Console.WriteLine("Env :");
        foreach (var scope in env.Scopes)
        {
            Console.WriteLine("== Scope ==");
            Console.WriteLine($"Functions : {string.Join(", ", scope.Functions.Select(x => x.Name))}");
            Console.WriteLine($"Variables : {string.Join(", ", scope.Variables.Select(x => $"{x.Name} ({x.Value})"))}");
            Console.WriteLine("===========");
        }

        return null;
    }
}
