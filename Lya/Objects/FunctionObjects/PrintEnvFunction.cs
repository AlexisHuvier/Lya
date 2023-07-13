using System;
using System.Collections.Generic;
using System.Linq;

namespace Lya.Objects.FunctionObjects;

public class PrintEnvFunction: Function
{
    public PrintEnvFunction()
    {
        Name = "printenv";
    }

    public override dynamic Eval(Env env, List<dynamic> arguments)
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
