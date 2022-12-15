using Lya.Utils;
using System;

namespace Lya.AST;

public class MathOperation: IExpression
{
    public enum Operators
    {
        Add,
        Sub,
        Mul,
        Div,
        Mod
    }
    
    public IExpression First;
    public IExpression Second;
    public Operators Operator;
    public string File { get; }
    public int Line { get; }

    public MathOperation(IExpression first, IExpression second, Operators op, string file, int line)
    {
        First = first;
        Second = second;
        Operator = op;
        File = file;
        Line = line;
    }

    public dynamic Eval(Env env)
    {
        return Operator switch
        {
            Operators.Sub => First.Eval(env) - Second.Eval(env),
            Operators.Mul => First.Eval(env) * Second.Eval(env),
            Operators.Div => First.Eval(env) / Convert.ToSingle(Second.Eval(env)),
            Operators.Mod => First.Eval(env) % Second.Eval(env),
            _ => First.Eval(env) + Second.Eval(env)
        };
    }
}
