using Lya;
using Lya.AST;
using NUnit.Framework;
using System.Collections.Generic;

namespace LyaTests;

public class ParserTest
{
    [Test]
    public void TestExpressions()
    {
        var expressions = Parser.Parse(Lexer.Tokenize("int i = 3; i = 5;print(i);", "_"));
        Assert.IsInstanceOf(typeof(List<IExpression>), expressions);
        Assert.IsInstanceOf(typeof(VarDeclaration), expressions[0]);
        Assert.IsInstanceOf(typeof(VarAffectation), expressions[1]);
        Assert.IsInstanceOf(typeof(FunctionCall), expressions[2]);
    }
}
