using System;

namespace Lya.Utils;

public class Token
{
    public string Value = null;
    public TokenType Type = TokenType.Unknown;
    public string File = "_";
    public int Line = -1;
    public int Column = -1;

    public override string ToString()
    {
        return $"{nameof(Value)}: {Value}, {nameof(Type)}: {Type}, {nameof(Line)}: {Line}, {nameof(Column)}: {Column}";
    }
}