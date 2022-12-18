using Lya.Objects;
using Lya.Objects.Function;
using System.Collections.Generic;
using System.Linq;

namespace Lya.Utils;

public class Env
{
    
    public static readonly List<string> KeywordTypes = new() { "int", "float", "bool", "string" };
    public static readonly List<string> Keywords = new() { "if" };

    public readonly Stack<Scope> Scopes;

    public Env()
    {
        Scopes = new Stack<Scope>();
        Scopes.Push(new Scope(new List<IFunction>
        {
            new PrintFunction(), new PrintEnvFunction(), new InputFunction()
        }, new List<Variable>
        {
            new("true", VariableType.Bool, true), new("false", VariableType.Bool, false)
        }));
    }

    public Scope GetCurrentScope() => Scopes.ElementAt(Scopes.Count - 1);
    
    public void AddGlobalFunctions(IEnumerable<IFunction> functions) => Scopes.ElementAt(0).AddFunctions(functions);
    public void AddGlobalFunction(IFunction function) => Scopes.ElementAt(0).AddFunction(function);
    public void AddGlobalVariables(IEnumerable<Variable> vars) => Scopes.ElementAt(0).AddVariables(vars);
    public void AddGlobalVariable(Variable var) => Scopes.ElementAt(0).AddVariable(var);

    public IFunction GetFunction(string name) => Scopes.Where(scope => scope.IsFunctionDefine(name)).Select(scope => scope.GetFunction(name)).FirstOrDefault();
    public Variable GetVariable(string name) => Scopes.Where(scope => scope.IsVariableDefine(name)).Select(scope => scope.GetVariable(name)).FirstOrDefault();
    
    public bool IsFunctionDefine(string name) => Scopes.Any(scope => scope.IsFunctionDefine(name));
    public bool IsVariableDefine(string name) => Scopes.Any(scope => scope.IsVariableDefine(name));
    public bool IsDefine(string name) => IsFunctionDefine(name) || IsVariableDefine(name);
}