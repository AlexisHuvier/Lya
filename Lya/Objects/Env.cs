using System.Collections.Generic;
using System.Linq;
using Lya.Objects.FunctionObjects;

namespace Lya.Objects;

public class Env
{
    
    public static readonly List<string> KeywordTypes = new() { "int", "float", "bool", "string" };
    public static readonly List<string> Keywords = new() { "if" };

    public readonly Stack<Scope> Scopes;

    public Env()
    {
        Scopes = new Stack<Scope>();
        Scopes.Push(new Scope(new List<Function>
        {
            new PrintFunction(), new PrintEnvFunction(), new InputFunction(), new CastFunction()
        }, new List<Variable>
        {
            new("true", VariableType.Bool, true), new("false", VariableType.Bool, false)
        }));
    }

    public Scope GetCurrentScope() => Scopes.ElementAt(0);
    
    public void AddGlobalFunctions(IEnumerable<Function> functions) => Scopes.ElementAt(0).AddFunctions(functions);
    public void AddGlobalFunction(Function function) => Scopes.ElementAt(0).AddFunction(function);
    public void AddGlobalVariables(IEnumerable<Variable> vars) => Scopes.ElementAt(0).AddVariables(vars);
    public void AddGlobalVariable(Variable var) => Scopes.ElementAt(0).AddVariable(var);

    public Function GetFunction(string name) => Scopes.Where(scope => scope.IsFunctionDefine(name)).Select(scope => scope.GetFunction(name)).FirstOrDefault();
    public Variable GetVariable(string name) => Scopes.Where(scope => scope.IsVariableDefine(name)).Select(scope => scope.GetVariable(name)).FirstOrDefault();
    
    public bool IsFunctionDefine(string name) => Scopes.Any(scope => scope.IsFunctionDefine(name));
    public bool IsVariableDefine(string name) => Scopes.Any(scope => scope.IsVariableDefine(name));
    public bool IsDefine(string name) => IsFunctionDefine(name) || IsVariableDefine(name);
}