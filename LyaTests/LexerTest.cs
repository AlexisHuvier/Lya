using NUnit.Framework;

namespace LyaTests
{
    public class LexerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestTokens()
        {
            foreach(var token in Lya.Lexer.Tokenize("int i = 0;\nprint(i);"))
                System.Console.WriteLine(token);
            Assert.Pass();
        }
    }
}