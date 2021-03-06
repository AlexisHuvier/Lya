using System;
using System.Collections.Generic;
using Lya.Objects;

namespace Lya
{
    public class Env
    {
        public static readonly List<string> KeywordTypes = new() { "int", "float", "bool", "string" };
        public static readonly List<string> Keywords = new() { "if" };

        public readonly Dictionary<string, Type> core_methods;
        public readonly Dictionary<string, Variable> variables;

        public Env()
        {
            core_methods = new Dictionary<string, Type>();
            variables = new Dictionary<string, Variable>();
        }
        
        public void AddCoreMethod(string[] names, Type[] types)
        {
            for (var i = 0; i < names.Length; i++)
                core_methods.Add(names[i], types[i]);
        }

        public void AddVariables(string[] names, Variable[] vars)
        {
            for (var i = 0; i < names.Length; i++)
                variables.Add(names[i], vars[i]);
        }

        public bool IsDefine(string name) => core_methods.ContainsKey(name) || variables.ContainsKey(name);

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
}