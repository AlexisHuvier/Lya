using Lya.Objects;

namespace Lya.AST;

public class Constant: Expression
{
    private readonly dynamic _value;

    public Constant(dynamic value, string file, int line)
    {
        _value = value;
        File = file;
        Line = line;
    }

    public override dynamic Eval(Env env) => _value;
}