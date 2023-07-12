using System.Collections.Generic;
using System.Linq;
using Lya.Objects.FunctionObjects;

namespace Lya.Objects;

public class Scope
{
    public readonly List<Function> Functions;
    public readonly List<Variable> Variables;

    public Scope()
    {
        Functions = new List<Function>();
        Variables = new List<Variable>();
    }

    public Scope(List<Function> functions, List<Variable> variables)
    {
        Functions = functions;
        Variables = variables;
    }

    public void AddFunctions(IEnumerable<Function> functions) => Functions.AddRange(functions);
    public void AddFunction(Function function) => Functions.Add(function);
    public bool IsFunctionDefine(string name) => Functions.Any(x => x.Name == name);
    public Function GetFunction(string name) => Functions.Find(x => x.Name == name);
    
    public void AddVariables(IEnumerable<Variable> variables) => Variables.AddRange(variables);
    public void AddVariable(Variable variable) => Variables.Add(variable);
    public bool IsVariableDefine(string name) => Variables.Any(x => x.Name == name);
    public Variable GetVariable(string name) => Variables.Find(x => x.Name == name);

    public bool IsDefine(string name) => IsFunctionDefine(name) || IsVariableDefine(name);
}
