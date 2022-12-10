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
        return $"{nameof(Value)}: {Value}, {nameof(Type)}: {Type}, {nameof(Line)}: {Line}, {nameof(Column)}: {Column}, {nameof(File)}: {File}";
    }

    public static bool operator ==(Token t1, Token t2) => t1?.Value == t2?.Value && t1?.Type == t2?.Type &&
                                                          t1?.File == t2?.File && t1?.Line == t2?.Line &&
                                                          t1?.Column == t2?.Column;
    public static bool operator !=(Token t1, Token t2) => !(t1 == t2);
    
    protected bool Equals(Token other) => Value == other.Value && Type == other.Type && File == other.File && Line == other.Line && Column == other.Column;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Token)obj);
    }

    public override int GetHashCode() => HashCode.Combine(Value, (int)Type, File, Line, Column);
}