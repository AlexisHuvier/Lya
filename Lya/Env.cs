using Lya.Objects;
using System;
using System.Collections.Generic;

namespace Lya;

public class Env
{
    public static readonly List<string> KeywordTypes = new() { "int", "float", "bool", "string" };
    public static readonly List<string> Keywords = new() { "if" };

    public readonly Dictionary<string, Type> CoreMethods;
    public readonly Dictionary<string, Variable> Variables;

    public Env()
    {
        CoreMethods = new Dictionary<string, Type>();
        Variables = new Dictionary<string, Variable>();
    }
    
    public void AddCoreMethod(string[] names, Type[] types)
    {
        for (var i = 0; i < names.Length; i++)
            CoreMethods.Add(names[i], types[i]);
    }

    public void AddVariables(string[] names, Variable[] vars)
    {
        for (var i = 0; i < names.Length; i++)
            Variables.Add(names[i], vars[i]);
    }

    public bool IsDefine(string name) => CoreMethods.ContainsKey(name) || Variables.ContainsKey(name);

    public static Env GetStandartEnv()
    {
        var env = new Env();
        
        env.AddCoreMethod(new string[]
        {
            
        },new Type[]
        {
            
        });
        
        env.AddVariables(new []
        {
            "true", "false"
        }, new[]
        {
            new Variable("true", VariableType.Bool, true), new Variable("false", VariableType.Bool, false)
        });

        return env;
    }
}