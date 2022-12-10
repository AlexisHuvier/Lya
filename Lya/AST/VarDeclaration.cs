using Lya.Objects;
using Lya.Utils;
using System.Collections.Generic;

namespace Lya.AST;

public class VarDeclaration: IExpression
{
    public string VarName;
    public VariableType Type;
    public IExpression Value;
    public string File { get; }
    public int Line { get; }

    public VarDeclaration(string varName, VariableType type, IReadOnlyList<IExpression> value, string file, int line)
    {
        VarName = varName;
        Type = type;
        Value = value[0];
        File = file;
        Line = line;
    }
    
    public dynamic Eval(Env env)
    {
        if (env.IsVariableDefine(VarName))
        {
            Error.SendError("AlreadyDefined", $"Already Defined Variable : {VarName}", this, true);
            return null;
        }

        var var = new Variable(VarName, Type, 0);
        if(!var.SetValue(Value.Eval(env)))
            Error.SendError("WrongType", $"Wrong Type for Variable : {VarName}. Expected : {var.Type}", this, true);
        env.GetCurrentScope().AddVariable(var);
        return var.Value;
    }
}