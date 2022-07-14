namespace Lya.AST
{
    public class VarAffectation: IExpression
    {
        public string VarName;
        public dynamic Value;
        
        public dynamic Eval(Env env)
        {
            throw new System.NotImplementedException();
        }
    }
}