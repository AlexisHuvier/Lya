using System.Collections.Generic;
using Lya.Objects;

namespace Lya.AST;

public class WhileExpression: Expression
{
    private readonly Expression _condition;
    private readonly List<Expression> _expressions;
    
    public WhileExpression(Expression condition, List<Expression> expressions, string file, int line)
    {
        _condition = condition;
        _expressions = expressions;
        File = file;
        Line = line;
    }

    public override dynamic Eval(Env env)
    {
        env.Scopes.Push(new Scope());
        
        while (_condition.Eval(env))
        {
            foreach (var expression in _expressions)
                expression.Eval(env);
        }

        env.Scopes.Pop();
        return null;
    }
}