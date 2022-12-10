using System.Collections.Generic;

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
        throw new System.NotImplementedException();
    }
}