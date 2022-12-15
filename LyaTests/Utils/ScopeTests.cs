using Lya.Objects;
using Lya.Objects.Function;
using Lya.Utils;

namespace LyaTests.Utils;

public class ScopeTests
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
        var scope = new Scope();
        Assert.Multiple(() =>
        {
            Assert.That(scope.IsFunctionDefine("temp"), Is.False);
            Assert.That(scope.IsVariableDefine("temp"), Is.False);
            Assert.That(scope.IsDefine("temp"), Is.False);
        });
        var temp = new Function("temp");
        var v = new Variable("v", VariableType.String, "v");
        scope.AddFunction(temp);
        scope.AddFunctions(new []{new Function("temp2")});
        scope.AddVariable(v);
        scope.AddVariables(Array.Empty<Variable>());
        
        Assert.Multiple(() =>
        {
            Assert.That(scope.IsFunctionDefine("temp"), Is.True);
            Assert.That(scope.IsVariableDefine("temp"), Is.False);
            Assert.That(scope.IsDefine("temp"), Is.True);
            Assert.That(scope.GetFunction("temp"), Is.EqualTo(temp));
            Assert.That(scope.GetVariable("v"), Is.EqualTo(v));
        });
    }
}
