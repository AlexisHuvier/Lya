using Lya;
using Lya.Utils;
using NUnit.Framework;

namespace LyaTests;

public class LexerTest
{
    [Test]
    public void TestTokens()
    {
        var tokens = Lexer.Tokenize("int i = 0;\nprint(i);", "_");
        Assert.AreEqual(tokens[0], new Token { Column = 1, Line = 1, File = "_", Type = TokenType.KeywordType, Value = "int" });
        Assert.AreEqual(tokens[2], new Token { Column = 7, Line = 1, File = "_", Type = TokenType.Operator, Value = "=" });
        Assert.AreEqual(tokens[5], new Token { Column = 1, Line = 2, File = "_", Type = TokenType.Identifier, Value = "print" });
        Assert.AreEqual(tokens[7], new Token { Column = 7, Line = 2, File = "_", Type = TokenType.Identifier, Value = "i" });
        Assert.AreEqual(tokens[9], new Token { Column = 9, Line = 2, File = "_", Type = TokenType.SemiColon, Value = ";" });
    }
}