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
            var value when value == VariableType.Null => Value is null,
            var value when value == VariableType.Integer => Value is int,
            var value when value == VariableType.Float => Value is float,
            var value when value == VariableType.String => Value is string,
            var value when value == VariableType.Bool => Value is bool,
            _ => false
        };
    }
}
