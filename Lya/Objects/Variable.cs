namespace Lya.Objects;

public class Variable
{
    public string Name;
    public VariableType Type;
    public dynamic Value;

    public Variable(string name, VariableType type, dynamic value)
    {
        Name = name;
        Type = type;
        Value = value;
    }

    public bool SetValue(dynamic val)
    {
        var temp = Value;
        Value = val;
        if (IsTypeValid())
            return true;
        Value = temp;
        return false;
    }

    public bool IsTypeValid()
    {
        if (Type == VariableType.Null)
            return Value is null;
        if (Type == VariableType.Integer)
            return Value is int;
        if (Type == VariableType.Float)
            return Value is float;
        if (Type == VariableType.String)
            return Value is string;
        if (Type == VariableType.Bool)
            return Value is bool;
        return false;
    }
}