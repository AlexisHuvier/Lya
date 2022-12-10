using Lya.Utils;

namespace Lya.AST;

public class VarDeclaration: IExpression
{
    public string VarName;
    public VariableType Type;
    public IExpression Value;
    public string File { get; }
    public int Line { get; }

    public VarDeclaration(string varName, VariableType type, IExpression value, string file, int line)
    {
        VarName = varName;
        Type = type;
        Value = value;
        File = file;
        Line = line;
    }
    
    public dynamic Eval(Env env)
    {
        throw new System.NotImplementedException();
    }
}