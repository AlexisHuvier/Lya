using Lya.Objects;
using Lya.Objects.Function;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lya.Utils;

public class Scope
{
    public readonly List<IFunction> Functions;
    public readonly List<Variable> Variables;

    public Scope()
    {
        Functions = new List<IFunction>();
        Variables = new List<Variable>();
    }

    public Scope(List<IFunction> functions, List<Variable> variables)
    {
        Functions = functions;
        Variables = variables;
    }

    public void AddFunctions(IEnumerable<IFunction> functions) => Functions.AddRange(functions);
    public void AddFunction(IFunction function) => Functions.Add(function);
    public bool IsFunctionDefine(string name) => Functions.Any(x => x.Name == name);
    public IFunction GetFunction(string name) => Functions.Find(x => x.Name == name);
    
    public void AddVariables(IEnumerable<Variable> variables) => Variables.AddRange(variables);
    public void AddVariable(Variable variable) => Variables.Add(variable);
    public bool IsVariableDefine(string name) => Variables.Any(x => x.Name == name);
    public Variable GetVariable(string name) => Variables.Find(x => x.Name == name);

    public bool IsDefine(string name) => IsFunctionDefine(name) || IsVariableDefine(name);
}
