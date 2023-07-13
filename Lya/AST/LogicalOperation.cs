using System;
using Lya.Objects;

namespace Lya.AST;

public class LogicalOperation: Expression
{
    public enum Operators
    {
        Less,
        Greater,
        LessEquals,
        GreaterEquals,
        Equals
    }

    private readonly Expression _first;
    private readonly Expression _second;
    private readonly Operators _operator;

    public LogicalOperation(Expression first, Expression second, Operators op, string file, int line)
    {
        _first = first;
        _second = second;
        _operator = op;
        File = file;
        Line = line;
    }

    public override dynamic Eval(Env env)
    {
        return _operator switch
        {
            Operators.Less => _first.Eval(env) < _second.Eval(env),
            Operators.Greater => _first.Eval(env) > _second.Eval(env),
            Operators.LessEquals => _first.Eval(env) <= _second.Eval(env),
            Operators.GreaterEquals => _first.Eval(env) >= _second.Eval(env),
            _ => _first.Eval(env) == _second.Eval(env)
        };
    }
}