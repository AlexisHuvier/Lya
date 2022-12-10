namespace Lya.AST;

public class VarAffectation: IExpression
{
    public string VarName;
    public IExpression Value;
    public string File { get; }
    public int Line { get; }

    public VarAffectation(string varName, IExpression value, string file, int line)
    {
        VarName = varName;
        Value = value;
        File = file;
        Line = line;
    }
    
    public dynamic Eval(Env env)
    {
        throw new System.NotImplementedException();
    }
}