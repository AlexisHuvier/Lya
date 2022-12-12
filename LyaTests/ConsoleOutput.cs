namespace LyaTests;

public class ConsoleOutput : IDisposable
{
    StringWriter _stringWriter;
    TextWriter _originalOut;

    public ConsoleOutput()
    {
        _stringWriter = new StringWriter();
        _originalOut = Console.Out;
        Console.SetOut(_stringWriter);
    }

    public string GetOut() => _stringWriter.ToString();
    public string[] GetOutLines() => _stringWriter.ToString().Replace("\r", "").Split("\n");
    
    public void Dispose()
    {
        Console.SetOut(_originalOut);
        _stringWriter.Dispose();
        GC.SuppressFinalize(this);
    }
}