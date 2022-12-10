using Lya.Utils;
using System.Collections.Generic;

namespace Lya.AST;

public class VarAffectation: IExpression
{
    public string VarName;
    public IExpression Value;
    public string File { get; }
    public int Line { get; }

    public VarAffectation(string varName, IReadOnlyList<IExpression> value, string file, int line)
    {
        VarName = varName;
        Value = value[0];
        File = file;
        Line = line;
    }
    
    public dynamic Eval(Env env)
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