using Lya.Objects;
using System.Collections.Generic;

namespace Lya.AST;

public class VarDeclaration: Expression
{
    private readonly string _varName;
    private readonly VariableType _type;
    private readonly Expression _value;

    public VarDeclaration(string varName, VariableType type, IReadOnlyList<Expression> value, string file, int line)
    {
        _varName = varName;
        _type = type;
        _value = value[0];
        File = file;
        Line = line;
    }
    
    public override dynamic Eval(Env env)
    {
        if (env.IsVariableDefine(_varName))
        {
            Error.SendError("AlreadyDefined", $"Already Defined Variable : {_varName}", this, true);
            return null;
        }

        var var = new Variable(_varName, _type, 0);
        if(!var.SetValue(_value.Eval(env)))
            Error.SendError("WrongType", $"Wrong Type for Variable : {_varName}. Expected : {var.Type}", this, true);
        env.GetCurrentScope().AddVariable(var);
        return var.Value;
    }
}