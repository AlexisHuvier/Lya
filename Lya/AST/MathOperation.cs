using System;
using Lya.Objects;

namespace Lya.AST;

public class MathOperation: Expression
{
    public enum Operators
    {
        Add,
        Sub,
        Mul,
        Div,
        Mod
    }

    private readonly Expression _first;
    private readonly Expression _second;
    private readonly Operators _operator;

    public MathOperation(Expression first, Expression second, Operators op, string file, int line)
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
            Operators.Sub => _first.Eval(env) - _second.Eval(env),
            Operators.Mul => _first.Eval(env) * _second.Eval(env),
            Operators.Div => _first.Eval(env) / Convert.ToSingle(_second.Eval(env)),
            Operators.Mod => _first.Eval(env) % _second.Eval(env),
            _ => _first.Eval(env) + _second.Eval(env)
        };
    }
}
