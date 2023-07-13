using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lya.Objects.FunctionObjects;

public class PrintFunction: Function
{
    public PrintFunction()
    {
        Name = "print";
    }

    public override dynamic Eval(Env env, List<dynamic> arguments)
    {
        Console.WriteLine(string.Join(" ", arguments.Select(x => x is float s ? s.ToString("G", CultureInfo.InvariantCulture) : x.ToString())));
        return null;
    }
}
