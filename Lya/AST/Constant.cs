namespace Lya.AST;

public class Constant: IExpression
{
    public dynamic Value;
    
    public dynamic Eval(Env env)
    {
        throw new System.NotImplementedException();
    }
}