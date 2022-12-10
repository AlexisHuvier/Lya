using Lya.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lya.Objects.Function;

public class PrintFunction: IFunction
{
    public string Name => "print";

    public dynamic Eval(Env env, IEnumerable<dynamic> arguments)
    {
        Console.WriteLine(string.Join(" ", arguments.Select(x => x.ToString())));
        return null;
    }
}
