namespace Lya.AST;

public class VarCall: IExpression
{
    public string VarName;
    
    public dynamic Eval(Env env)
    {
        throw new System.NotImplementedException();
    }
}