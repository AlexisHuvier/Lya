using System.Collections.Generic;
using System.Linq;
using Lya.Objects;

namespace Lya.AST;

public class FunctionCall: Expression
{
    private readonly string _functionName;
    private readonly List<Expression> _arguments;

    public FunctionCall(string functionName, List<Expression> arguments, string file, int line)
    {
        _functionName = functionName;
        _arguments = arguments;
        File = file;
        Line = line;
    }
    
    public override dynamic Eval(Env env)
    {
        if (env.IsFunctionDefine(_functionName))
            return env.GetFunction(_functionName).Eval(env, _arguments.Select(x => x.Eval(env)).ToList());
        Error.SendError("Undefined", $"Undefined Function : {_functionName}", this, true);
        return null;
    }
}