namespace Lya.AST;

public interface IExpression
{
    public string File { get; }
    public int Line { get; }
    
    public dynamic Eval(Env env);
}