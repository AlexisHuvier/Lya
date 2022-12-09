namespace Lya.AST;

public interface IExpression
{
    public dynamic Eval(Env env);
}