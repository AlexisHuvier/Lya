using System.Collections.Generic;
using Lya.Objects;

namespace Lya.AST;

public class VarAffectation: Expression
{
    public string VarName;
    public Expression Value;

    public VarAffectation(string varName, IReadOnlyList<Expression> value, string file, int line)
    {
        VarName = varName;
        Value = value[0];
        File = file;
        Line = line;
    }
    
    public override dynamic Eval(Env env)
    {
        if (env.IsVariableDefine(VarName))
        {
            if(!env.GetVariable(VarName).SetValue(Value.Eval(env)))
                Error.SendError("WrongType", $"Wrong Type for Variable : {VarName}. Expected : {env.GetVariable(VarName).Type}", this, true);
            return env.GetVariable(VarName).Value;
        }

        Error.SendError("Undefined", $"Undefined Variable : {VarName}", this, true);
        return null;
    }
}