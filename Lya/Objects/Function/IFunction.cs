using Lya.Utils;
using System.Collections.Generic;

namespace Lya.Objects.Function;

public interface IFunction
{
    public string Name { get; }
    public dynamic Eval(Env env, IEnumerable<dynamic> arguments);
}
