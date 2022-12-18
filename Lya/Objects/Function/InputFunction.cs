using Lya.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lya.Objects.Function;

public class InputFunction: IFunction
{
    public string Name => "input";

    public dynamic Eval(Env env, IEnumerable<dynamic> arguments)
    {
        Console.Write(string.Join(" ", arguments.Select(x => x is float s ? s.ToString("G", CultureInfo.InvariantCulture) : x.ToString())));
        return Console.ReadLine();
    }
}
