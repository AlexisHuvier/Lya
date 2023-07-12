using Lya;
using Lya.AST;
using Lya.Objects;

namespace LyaTests;

// ReSharper disable once InconsistentNaming
public class ASTTests
{
    [Test]
    public void MathOperationTests()
    {
        var constant = new Constant(1, "_", 1);
        var opAdd = new MathOperation(constant, constant, MathOperation.Operators.Add, "_", 1);
        var opMod = new MathOperation(constant, constant, MathOperation.Operators.Mod, "_", 1);
        var opSub = new MathOperation(constant, constant, MathOperation.Operators.Sub, "_", 1);
        var opMul = new MathOperation(constant, constant, MathOperation.Operators.Mul, "_", 1);
        var opDiv = new MathOperation(constant, constant, MathOperation.Operators.Div, "_", 1);
        Assert.Multiple(() =>
        {
            Assert.That(opAdd.File, Is.EqualTo("_"));
            Assert.That(opAdd.Line, Is.EqualTo(1));
            Assert.That(opAdd.Eval(new Env()), Is.EqualTo(2));
            Assert.That(opDiv.Eval(new Env()), Is.EqualTo(1));
            Assert.That(opSub.Eval(new Env()), Is.EqualTo(0));
            Assert.That(opMod.Eval(new Env()), Is.EqualTo(0));
            Assert.That(opMul.Eval(new Env()), Is.EqualTo(1));
        });
    }

    [Test]
    public void FunctionCallTests()
    {
        var funcExist = new FunctionCall("print", new List<Expression>(), "_", 1);
        var funcNotExist = new FunctionCall("printaikj", new List<Expression>(), "_", 1);
        Assert.Multiple(() =>
        {
            Assert.That(funcExist.File, Is.EqualTo("_"));
            Assert.That(funcExist.Line, Is.EqualTo(1));
            try
            {
                funcExist.Eval(new Env());
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
            Assert.Throws<Error.LyaErrorException>(() => funcNotExist.Eval(new Env()));
        });
    }

    [Test]
    public void VarCallTests()
    {
        var env = new Env();
        env.AddGlobalVariable(new Variable("i", VariableType.Integer, 2));
        var varExist = new VarCall("i", "_", 1);
        var varNotExist = new VarCall("io", "_", 1);
        Assert.Multiple(() =>
        {
            Assert.That(varExist.File, Is.EqualTo("_"));
            Assert.That(varExist.Line, Is.EqualTo(1));
            Assert.That(varExist.Eval(env), Is.EqualTo(2));
            Assert.Throws<Error.LyaErrorException>(() => varNotExist.Eval(new Env()));
        });
    }

    [Test]
    public void VarDeclarationTests()
    {
        var env = new Env();
        var varDeclarator = new VarDeclaration("i", VariableType.Integer, new[] { new Constant(1, "_", 1) }, "_", 1);
        var varWrongType = new VarDeclaration("i2", VariableType.Integer, new[] { new Constant("test", "_", 1) }, "_", 1);
        Assert.Multiple(() =>
        {
            Assert.That(varDeclarator.File, Is.EqualTo("_"));
            Assert.That(varDeclarator.Line, Is.EqualTo(1));
            Assert.That(varDeclarator.Eval(env), Is.EqualTo(1));
            Assert.Throws<Error.LyaErrorException>(() => varDeclarator.Eval(env));
            Assert.Throws<Error.LyaErrorException>(() => varWrongType.Eval(env));
        });
    }

    [Test]
    public void VarAffectationTests()
    {
        var env = new Env();
        env.AddGlobalVariable(new Variable("i", VariableType.Integer, 2));
        var varAffectation = new VarAffectation("i", new[] { new Constant(1, "_", 1) }, "_", 1);
        var varNotExist = new VarAffectation("i2", new[] { new Constant("test", "_", 1) }, "_", 1);
        var varWrongType = new VarAffectation("i", new[] { new Constant("test", "_", 1) }, "_", 1);
        Assert.Multiple(() =>
        {
            Assert.That(varAffectation.File, Is.EqualTo("_"));
            Assert.That(varAffectation.Line, Is.EqualTo(1));
            Assert.That(varAffectation.Eval(env), Is.EqualTo(1));
            Assert.Throws<Error.LyaErrorException>(() => varNotExist.Eval(env));
            Assert.Throws<Error.LyaErrorException>(() => varWrongType.Eval(env));
        });
    }
}
