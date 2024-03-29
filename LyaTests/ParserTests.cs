﻿using Lya;
using Lya.AST;
using Lya.Objects;

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
            Assert.That(outConsole.GetOutLines()[0], Is.EqualTo("InvalidDeclaration: Incomplete declaration"));
            Assert.Catch(typeof(Exception), () => Parser.Parse(Lexer.Tokenize("i =;")));
            Assert.That(outConsole.GetOutLines()[2], Is.EqualTo("MissingValue: Missing value for affection"));
            Assert.Catch(typeof(Exception), () => Parser.Parse(Lexer.Tokenize("print(;")));
            Assert.That(outConsole.GetOutLines()[4], Is.EqualTo("MissingParenthesis: Missing closing parenthesis"));
            Assert.Catch(typeof(Exception), () => Parser.Parse(Lexer.Tokenize("print i ijn;")));
            Assert.That(outConsole.GetOutLines()[6][..27], Is.EqualTo("Unknown: Unknown expression"));
        });
    }

    [Test]
    public void MathParsingTest()
    {
        var env = new Env();
        env.AddGlobalVariable(new Variable("i", VariableType.Integer, 2));
        var outConsole = new ConsoleOutput();
        Interpreter.Run("print(1+1);");
        Interpreter.Run("print(1*5*(1+1));");
        Interpreter.Run("print(4%5+2);");
        Interpreter.Run("print(5/2);");
        Interpreter.Run("print(5/(2+3));");
        Interpreter.Run("print(1 + i);", env);

            Assert.Multiple(() =>
        {
            Assert.That(outConsole.GetOutLines()[0], Is.EqualTo("2"));
            Assert.That(outConsole.GetOutLines()[1], Is.EqualTo("10"));
            Assert.That(outConsole.GetOutLines()[2], Is.EqualTo("6"));
            Assert.That(outConsole.GetOutLines()[3], Is.EqualTo("2.5"));
            Assert.That(outConsole.GetOutLines()[4], Is.EqualTo("1"));
            Assert.That(outConsole.GetOutLines()[5], Is.EqualTo("3"));
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
