using Lya;
using Lya.Utils;

namespace LyaTests;

public class LexerTests
{
    [Test]
    public void PositionTests()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Lexer.Tokenize("int i = 0;", "_")[1].Column, Is.EqualTo(5));
            Assert.That(Lexer.Tokenize("int i = 0;\nprint(i);", "_")[5].Line, Is.EqualTo(2));
        });
    }
    
    [Test]
    public void TokenTypeTests()
    {
        Assert.Multiple(() =>
        {
            Assert.That(new Token { Column = 1, Line = 1, File = "_", Type = TokenType.KeywordType, Value = "int" }, Is.EqualTo(Lexer.Tokenize("int", "_")[0]));
            Assert.That(new Token { Column = 1, Line = 1, File = "_", Type = TokenType.Brace, Value = "{" }, Is.EqualTo(Lexer.Tokenize("{", "_")[0]));
            Assert.That(new Token { Column = 1, Line = 1, File = "_", Type = TokenType.Comma, Value = "," }, Is.EqualTo(Lexer.Tokenize(",", "_")[0]));
            Assert.That(new Token { Column = 1, Line = 1, File = "_", Type = TokenType.Hook, Value = "[" }, Is.EqualTo(Lexer.Tokenize("[", "_")[0]));
            Assert.That(new Token { Column = 1, Line = 1, File = "_", Type = TokenType.Paren, Value = "(" }, Is.EqualTo(Lexer.Tokenize("(", "_")[0]));
            Assert.That(new Token { Column = 1, Line = 1, File = "_", Type = TokenType.Identifier, Value = "identifier" }, Is.EqualTo(Lexer.Tokenize("identifier", "_")[0]));
            Assert.That(new Token { Column = 1, Line = 1, File = "_", Type = TokenType.Keyword, Value = "if" }, Is.EqualTo(Lexer.Tokenize("if", "_")[0]));
            Assert.That(new Token { Column = 1, Line = 1, File = "_", Type = TokenType.Number, Value = "4" }, Is.EqualTo(Lexer.Tokenize("4", "_")[0]));
            Assert.That(new Token { Column = 1, Line = 1, File = "_", Type = TokenType.Operator, Value = "=" }, Is.EqualTo(Lexer.Tokenize("=", "_")[0]));
            Assert.That(new Token { Column = 1, Line = 1, File = "_", Type = TokenType.SemiColon, Value = ";" }, Is.EqualTo(Lexer.Tokenize(";", "_")[0]));
            Assert.That(Lexer.Tokenize(" ", "_"), Is.Empty);
        });
    }
}
