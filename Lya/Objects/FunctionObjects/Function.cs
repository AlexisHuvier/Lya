using System.Collections.Generic;

namespace Lya.Objects.FunctionObjects;

public abstract class Function
{
    public string Name { get; protected init; }
    
    public abstract dynamic Eval(Env env, IEnumerable<dynamic> arguments);
}
