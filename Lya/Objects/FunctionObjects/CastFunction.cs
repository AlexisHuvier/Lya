using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lya.Objects.FunctionObjects;

public class CastFunction: Function
{
    public CastFunction()
    {
        Name = "cast";
    }

    public override dynamic Eval(Env env, List<dynamic> arguments)
    {
        if (arguments.Count == 2)
        {
            switch (arguments[0])
            {
                case "int":
                    return Convert.ToInt32(arguments[1]);
                case "string":
                    return Convert.ToString(arguments[1]);
                case "float":
                    return Convert.ToSingle(arguments[1]);
                case "bool":
                    return Convert.ToBoolean(arguments[1]);
                default:
                    Error.SendError("UnknownCast", $"Unknown cast : {arguments[0]}", true);
                    return null;
            }
        }

        Error.SendError("WrongNumberArguments", $"Cast Function want two arguments, {arguments.Count} passed", true);
        return null;
    }
}