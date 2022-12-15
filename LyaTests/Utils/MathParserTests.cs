using Lya.AST;
using Lya.Utils;

namespace LyaTests.Utils;

public class MathParserTests
{
    [Test]
    public void GetOperandTests()
    {
        var constant = new Constant(1, "_", 1);
        var token = new Token { Type = TokenType.Number, Value = "1", Column = 1, File = "_", Line = 1};
        var tokens = new List<Token>()
        {
            new() { Type = TokenType.Identifier, Value = "i", Column = 1, Line = 1, File = "_" },
            new() { Type = TokenType.Operator, Value = "=", Column = 2, Line = 1, File = "_" },
            new() { Type = TokenType.Number, Value = "1", Column = 3, Line = 1, File = "_" }
        };
        Assert.Multiple(() =>
        {
            Assert.That(MathParser.GetOperand(constant), Is.EqualTo(constant));
            Assert.That(MathParser.GetOperand(token), Is.InstanceOf<Constant>());
            Assert.That(MathParser.GetOperand(tokens), Is.InstanceOf<VarAffectation>());
        });
    }

    [Test]
    public void GetOperatorTests()
    {
        Assert.Multiple(() =>
        {
            Assert.That(MathParser.GetOperator(new Token {Value = "+"}), Is.EqualTo(MathOperation.Operators.Add));
            Assert.That(MathParser.GetOperator(new Token {Value = "-"}), Is.EqualTo(MathOperation.Operators.Sub));
            Assert.That(MathParser.GetOperator(new Token {Value = "*"}), Is.EqualTo(MathOperation.Operators.Mul));
            Assert.That(MathParser.GetOperator(new Token {Value = "/"}), Is.EqualTo(MathOperation.Operators.Div));
            Assert.That(MathParser.GetOperator(new Token {Value = "%"}), Is.EqualTo(MathOperation.Operators.Mod));
            Assert.Throws<Exception>(() => MathParser.GetOperator(new Token {Value = "bla"}));
        });
    }
}
