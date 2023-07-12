using Lya.Objects;

namespace Lya.AST;

public class VarCall: Expression
{
    private readonly string _varName;

    public VarCall(string varName, string file, int line)
    {
        _varName = varName;
        File = file;
        Line = line;
    }
    
    public override dynamic Eval(Env env)
    {
        if (env.IsVariableDefine(_varName))
            return env.GetVariable(_varName).Value;
        Error.SendError("Undefined", $"Undefined Variable : {_varName}", this, true);
        return null;
    }
}