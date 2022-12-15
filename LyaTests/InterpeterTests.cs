using Lya;

namespace LyaTests;

public class InterpeterTests
{
    [Test]
    public void RunFileTests()
    {
        var consoleOutput = new ConsoleOutput();
        Interpreter.Run("print(1);");
        Interpreter.RunFile("basic.lya");
        Assert.Multiple(() =>
        {
            Assert.That(consoleOutput.GetOutLines()[0], Is.EqualTo("1"));
            Assert.That(consoleOutput.GetOutLines()[1], Is.EqualTo(consoleOutput.GetOutLines()[0]));
        });
    }

    [Test]
    public void ProgramTests()
    {
        var consoleOutput = new ConsoleOutput();
        var temp = Console.In;
        Console.SetIn(new StringReader("print(1);\ni = 3;\nexit"));
        Program.Main(new []{"basic.lya"});
        Program.Main(Array.Empty<string>());
        
        Assert.Multiple(() =>
        {
            Assert.That(consoleOutput.GetOutLines()[0], Is.EqualTo("1"));
            Assert.That(consoleOutput.GetOutLines()[1], Is.EqualTo("Lya 1.0.0 >>> 1"));
            Assert.That(consoleOutput.GetOutLines()[2], Is.EqualTo("Lya 1.0.0 >>> Undefined: Undefined Variable : i"));
        });
        Console.SetIn(temp);
        
    }
}
