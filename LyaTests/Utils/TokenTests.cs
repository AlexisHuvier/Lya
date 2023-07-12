using Lya.Objects.TokenObjects;

namespace LyaTests.Utils;

public class TokenTests
{
    [Test]
    public void Tests()
    {
        var token = new Token { Type = TokenType.Comma, Column = 1, Line = 1, File = "_", Value = "," };
        var token2 = new Token { Type = TokenType.Comma, Column = 1, Line = 2, File = "_", Value = "," };
        Assert.Multiple(() =>
        {
            Assert.That(token == token2, Is.False);
            Assert.That(token != token2, Is.True);
            Assert.That(token.Equals(token2), Is.False);
            Assert.That(token.Equals(null), Is.False);
            Assert.That(token?.Equals(token), Is.True);
            Assert.That(token?.GetHashCode(), Is.EqualTo(HashCode.Combine(token?.Value, (int)token?.Type, token?.File, token?.Line, token?.Column)));
            Assert.That(token?.ToString(), Is.EqualTo("Value: ,, Type: Comma, Line: 1, Column: 1, File: _"));
        });
    }
}
