using Lya.Objects;
using Lya.Objects.FunctionObjects;

namespace LyaTests.Utils;

public class EnvTests
{
    public class FunctionBase : Function
    {

        public FunctionBase(string name) => Name = name;
        public override dynamic Eval(Env env, List<dynamic> arguments) => Name;
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
        var temp = new FunctionBase("temp");
        var v = new Variable("v", VariableType.String, "v");
        env.AddGlobalFunction(temp);
        env.AddGlobalFunctions(new []{new FunctionBase("temp2")});
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
