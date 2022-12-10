using Lya.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Lya.AST;

public class FunctionCall: IExpression
{
    public string FunctionName;
    public List<IExpression> Arguments;
    public string File { get; }
    public int Line { get; }

    public FunctionCall(string functionName, List<IExpression> arguments, string file, int line)
    {
        FunctionName = functionName;
        Arguments = arguments;
        File = file;
        Line = line;
    }
    
    public dynamic Eval(Env env)
    {
        if (env.IsFunctionDefine(FunctionName))
            return env.GetFunction(FunctionName).Eval(env, Arguments.Select(x => x.Eval(env)));
        Error.SendError("Undefined", $"Undefined Function : {FunctionName}", this, true);
        return null;
    }
}