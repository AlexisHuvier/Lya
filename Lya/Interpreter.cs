using Lya.AST;
using Lya.Utils;
using System.Collections.Generic;
using System.IO;

namespace Lya;

public class Interpreter
{
    public const string Version = "1.0.0";

    static void Eval(List<IExpression> expressions, Env env = null)
    {
        env ??= new Env();
        foreach (var expression in expressions)
            expression.Eval(env);
    }

    public static void RunFile(string file) => Eval(Parser.Parse(Lexer.Tokenize(File.ReadAllText(file), file)));
    public static void Run(string program) => Eval(Parser.Parse(Lexer.Tokenize(program)));
}
