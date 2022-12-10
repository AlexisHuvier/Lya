using Lya.Utils;

namespace Lya.AST;

public class VarCall: IExpression
{
    public string VarName;
    public string File { get; }
    public int Line { get; }

    public VarCall(string varName, string file, int line)
    {
        VarName = varName;
        File = file;
        Line = line;
    }
    
    public dynamic Eval(Env env)
    {
        if (env.IsVariableDefine(VarName))
            return env.GetVariable(VarName).Value;
        Error.SendError("Undefined", $"Undefined Variable : {VarName}", this, true);
        return null;
    }
}