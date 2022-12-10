namespace Lya.Utils;

public class VariableType
{
    public readonly string Name;

    public VariableType(string name) => Name = name;

    public static readonly VariableType Integer = new("int");
    public static readonly VariableType Bool = new("bool");
    public static readonly VariableType Float = new("float");
    public static readonly VariableType String = new("string");
    public static readonly VariableType Null = new("_");
    
    public static VariableType GetVariableTypeFromKeyword(string keyword)
    {
        return keyword switch
        {
            "int" => Integer,
            "bool" => Bool,
            "float" => Float,
            "string" => String,
            _ => Null
        };
    }

    public static bool operator ==(VariableType type1, VariableType type2) => type1?.Name == type2?.Name;
    public static bool operator !=(VariableType type1, VariableType type2) => !(type1 == type2);
    
    protected bool Equals(VariableType other) => Name == other.Name;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((VariableType)obj);
    }

    public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

    public override string ToString() => Name;
}
