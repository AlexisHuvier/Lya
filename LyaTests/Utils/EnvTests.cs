using Lya.Objects;
using Lya.Objects.Function;
using Lya.Utils;

namespace LyaTests.Utils;

public class EnvTests
{
    public class Function : IFunction
    {
        public string Name { get; }

        public Function(string name) => Name = name;
        public dynamic Eval(Env env, IEnumerable<dynamic> arguments) => Name;
    }
    
    [Test]
    public void EnvironmentTests()
    {
        var env = new Env();
        Assert.Multiple(() =>
        {
            Assert.That(env.IsFunctionDefine("temp"), Is.False);
            Assert.That(env.IsVariableDefine("temp"), Is.False);
            Assert.That(env.IsDefine("temp"), Is.False);
        });
        var temp = new Function("temp");
        var v = new Variable("v", VariableType.String, "v");
        env.AddGlobalFunction(temp);
        env.AddGlobalFunctions(new []{new Function("temp2")});
        env.AddGlobalVariable(v);
        env.AddGlobalVariables(Array.Empty<Variable>());
        
        Assert.Multiple(() =>
        {
            Assert.That(env.IsFunctionDefine("temp"), Is.True);
            Assert.That(env.IsVariableDefine("temp"), Is.False);
            Assert.That(env.IsDefine("temp"), Is.True);
            Assert.That(env.GetFunction("temp"), Is.EqualTo(temp));
            Assert.That(env.GetVariable("v"), Is.EqualTo(v));
        });
    }
}
