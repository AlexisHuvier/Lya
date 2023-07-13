using System.Collections.Generic;
using Lya.Objects;

namespace Lya.AST;

public class IfExpression: Expression
{
    private readonly Expression _condition;
    private readonly List<Expression> _expressions;
    
    public IfExpression(Expression condition, List<Expression> expressions, string file, int line)
    {
        _condition = condition;
        _expressions = expressions;
        File = file;
        Line = line;
    }

    public override dynamic Eval(Env env)
    {
        if (!_condition.Eval(env)) return false;
        
        env.Scopes.Push(new Scope());
        foreach (var expression in _expressions)
            expression.Eval(env);
        env.Scopes.Pop();
        return true;

    }
}