using NUnit.Framework;

namespace LyaTests;

public class LexerTest
{
    [Test]
    public void TestTokens()
    {
        foreach(var token in Lya.Lexer.Tokenize("int i = 0;\nprint(i);", "_"))
            System.Console.WriteLine(token);
        Assert.Pass();
    }
}