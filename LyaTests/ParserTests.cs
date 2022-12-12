using Lya;
using Lya.AST;

namespace LyaTests;

public class ParserTests
{
    [Test]
    public void ErrorTests()
    {
        var outConsole = new ConsoleOutput();
        Assert.Multiple(() =>
        {
            Assert.Catch(typeof(Exception), () => Parser.Parse(Lexer.Tokenize("int i =;")));
            Assert.That(outConsole.GetOutLines()[0], Is.EqualTo("Invalid: Incomplete declaration"));
            Assert.Catch(typeof(Exception), () => Parser.Parse(Lexer.Tokenize("i =;")));
            Assert.That(outConsole.GetOutLines()[2], Is.EqualTo("SyntaxError: Missing value for affection"));
            Assert.Catch(typeof(Exception), () => Parser.Parse(Lexer.Tokenize("print(;")));
            Assert.That(outConsole.GetOutLines()[4], Is.EqualTo("SyntaxError: Missing closing parenthesis"));
            Assert.Catch(typeof(Exception), () => Parser.Parse(Lexer.Tokenize("print i ijn;")));
            Assert.That(outConsole.GetOutLines()[6][..27], Is.EqualTo("Unknown: Unknown expression"));
        });
    }

    [Test]
    public void ExpressionsTests()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Parser.Parse(Lexer.Tokenize("4"))[0], Is.InstanceOf(typeof(Constant)));
            Assert.That(Parser.Parse(Lexer.Tokenize("print();"))[0], Is.InstanceOf(typeof(FunctionCall)));
            Assert.That(Parser.Parse(Lexer.Tokenize("i = 4;"))[0], Is.InstanceOf(typeof(VarAffectation)));
            Assert.That(Parser.Parse(Lexer.Tokenize("i"))[0], Is.InstanceOf(typeof(VarCall)));
            Assert.That(Parser.Parse(Lexer.Tokenize("int i = 4;"))[0], Is.InstanceOf(typeof(VarDeclaration)));
        });
    }
}
