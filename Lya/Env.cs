using Lya.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lya;

public class Env
{
    public static readonly List<string> KeywordTypes = new() { "int", "float", "bool", "string" };
    public static readonly List<string> Keywords = new() { "if" };

    public readonly List<Type> CoreMethods;
    public readonly List<Variable> Variables;

    public Env()
    {
        CoreMethods = new List<Type>();
        Variables = new List<Variable>();
    }
    
    public void AddCoreMethod(IEnumerable<Type> types) => CoreMethods.AddRange(types);
    public void AddVariables(IEnumerable<Variable> vars) => Variables.AddRange(vars);
    public bool IsCoreMethodDefine(string name) => CoreMethods.Any(x => x.Name == name);
    public bool IsVariableDefine(string name) => Variables.Any(x => x.Name == name);
    public bool IsDefine(string name) => IsCoreMethodDefine(name) || IsVariableDefine(name);

    public static Env GetStandartEnv()
    {
        var env = new Env();
        
        env.AddCoreMethod(new Type[]
        {
            
        });
        
        env.AddVariables(new[]
        {
            new Variable("true", VariableType.Bool, true), new Variable("false", VariableType.Bool, false)
        });

        return env;
    }
}