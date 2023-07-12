using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lya.Objects.FunctionObjects;

public class InputFunction: Function
{
    public InputFunction()
    {
        Name = "input";
    }

    public override dynamic Eval(Env env, IEnumerable<dynamic> arguments)
    {
        Console.Write(string.Join(" ", arguments.Select(x => x is float s ? s.ToString("G", CultureInfo.InvariantCulture) : x.ToString())));
        return Console.ReadLine();
    }
}
