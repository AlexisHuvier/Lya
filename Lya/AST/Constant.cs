using Lya.Utils;

namespace Lya.AST;

public class Constant: IExpression
{
    public dynamic Value;
    public string File { get; }
    public int Line { get; }

    public Constant(dynamic value, string file, int line)
    {
        Value = value;
        File = file;
        Line = line;
    }

    public dynamic Eval(Env env) => Value;
}