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
        return Type switch 
        {
            VariableType.Null => Value is null,
            VariableType.Integer => Value is int,
            VariableType.Float => Value is float,
            VariableType.String => Value is string,
            VariableType.Bool => Value is bool,
            _ => false
        };
    }
}
