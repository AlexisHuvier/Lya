using Lya.Objects;

namespace Lya.AST;

public abstract class Expression
{
    public string File { get; protected init; }
    public int Line { get; protected init; }
    
    public abstract dynamic Eval(Env env);
}