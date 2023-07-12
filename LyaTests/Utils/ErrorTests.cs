using Lya;
using Lya.AST;
using Lya.Objects.TokenObjects;

namespace LyaTests.Utils;

public class ErrorTests
{
    [Test]
    public void SendErrorTests()
    {
        try
        {
            Error.SendError("Error", "This is an error", new Token());
            Error.SendError("Error", "This is an error", new Constant(1, "_", 1));
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got: " + ex.Message);
        }
        Assert.Throws<Error.LyaErrorException>(() => Error.SendError("Error", "This is an error", new Token(), true));
        Assert.Throws<Error.LyaErrorException>(() => Error.SendError("Error", "This is an error", new Constant(1, "_", 1), true));
    }
}
