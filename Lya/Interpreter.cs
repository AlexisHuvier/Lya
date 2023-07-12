using Lya.AST;
using System.Collections.Generic;
using System.IO;
using Lya.Objects;

namespace Lya;

public static class Interpreter
{
    public const string Version = "1.0.0";

    private static void Eval(List<Expression> expressions, Env env = null)
    {
        env ??= new Env();
        foreach (var expression in expressions)
            expression.Eval(env);
    }

    public static void RunFile(string file, Env env = null) => Eval(Parser.Parse(Lexer.Tokenize(File.ReadAllText(file), file)), env);
    public static void Run(string program, Env env = null) => Eval(Parser.Parse(Lexer.Tokenize(program)), env);
}
