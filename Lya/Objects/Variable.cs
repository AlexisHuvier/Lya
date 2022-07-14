namespace Lya.Objects
{
    public enum VariableType
    {
        Null,
        String,
        Integer,
        Float,
        Bool
    }
    
    public class Variable
    {
        public string Name;
        public readonly VariableType Type;
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
            switch (Type)
            {
                case VariableType.Null:
                    return Value is null;
                case VariableType.String:
                    return Value is string;
                case VariableType.Integer:
                    return Value is int;
                case VariableType.Float:
                    return Value is float;
                case VariableType.Bool:
                    return Value is bool;
            }

            return false;
        }
    }
}