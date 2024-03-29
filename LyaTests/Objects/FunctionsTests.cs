﻿using Lya;

namespace LyaTests.Objects;

public class CoreFunctionsTests
{
    [Test]
    public void PrintTests()
    {
        var outConsole = new ConsoleOutput();
        Interpreter.Run("print(\"testing\");");
        Interpreter.Run("print(\"testing with\", \"many arguments\");");
        Interpreter.Run("int age = 18;print(\"Vous avez\", age, \"ans.\");");
        Interpreter.Run("print(2.5);");
        Assert.Multiple(() =>
        {
            Assert.That(outConsole.GetOutLines()[0], Is.EqualTo("testing"));
            Assert.That(outConsole.GetOutLines()[1], Is.EqualTo("testing with many arguments"));
            Assert.That(outConsole.GetOutLines()[2], Is.EqualTo("Vous avez 18 ans."));
            Assert.That(outConsole.GetOutLines()[3], Is.EqualTo("2.5"));
        });
    }

    [Test]
    public void PrintEnvTests()
    {
        var outConsole = new ConsoleOutput();
        Interpreter.Run("int i = 5; printenv();");
        Assert.Multiple(() =>
        {
            Assert.That(outConsole.GetOutLines()[0], Is.EqualTo("Env :"));
            Assert.That(outConsole.GetOutLines()[1], Is.EqualTo("== Scope =="));
            Assert.That(outConsole.GetOutLines()[2], Is.EqualTo("Functions : print, printenv, input, cast"));
            Assert.That(outConsole.GetOutLines()[3], Is.EqualTo("Variables : true (True), false (False), i (5)"));
            Assert.That(outConsole.GetOutLines()[4], Is.EqualTo("==========="));
        });
    }

    [Test]
    public void InputTests()
    {
        var outConsole = new ConsoleOutput();
        var temp = Console.In;
        Console.SetIn(new StringReader("input super cool"));
        Interpreter.Run("print(input(\"Mega test : \"));");
        Assert.That(outConsole.GetOut(), Is.EqualTo("Mega test : input super cool\r\n"));
        Console.SetIn(temp);
    }
}
