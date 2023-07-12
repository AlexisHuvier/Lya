namespace Lya.Objects;

public class VariableType
{
    private readonly string _name;

    private VariableType(string name) => _name = name;

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

    public static bool operator ==(VariableType type1, VariableType type2) => type1?._name == type2?._name;
    public static bool operator !=(VariableType type1, VariableType type2) => !(type1 == type2);

    private bool Equals(VariableType other) => _name == other._name;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((VariableType)obj);
    }

    public override int GetHashCode() => (_name != null ? _name.GetHashCode() : 0);

    public override string ToString() => _name;
}
