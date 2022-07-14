using System.Collections.Generic;

namespace Lya.AST
{
    public class FunctionCall: IExpression
    {
        public string FunctionName;
        public List<IExpression> Arguments;
        
        public dynamic Eval(Env env)
        {
            throw new System.NotImplementedException();
        }
    }
}