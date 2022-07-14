using Lya.Objects;

namespace Lya.AST
{
    public class VarDeclaration: IExpression
    {
        public string VarName;
        public VariableType Type;
        public dynamic Value;
        
        public dynamic Eval(Env env)
        {
            throw new System.NotImplementedException();
        }
    }
}