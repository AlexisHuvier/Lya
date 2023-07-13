using System;
using System.Collections.Generic;
using Lya.AST;
using Lya.Objects.TokenObjects;

namespace Lya.Parsers;

public static class LogicParser
{
    public static LogicalOperation ParseLogicOperation(List<Token> expression) =>
        ToLogicOperation(expression);

    private static LogicalOperation.Operators GetOperator(List<Token> tokens)
    {
        return tokens[0].Value switch
        {
            "<" => tokens[1].Value == "=" ? LogicalOperation.Operators.LessEquals : LogicalOperation.Operators.Less,
            ">" => tokens[1].Value == "="
                ? LogicalOperation.Operators.GreaterEquals
                : LogicalOperation.Operators.Greater,
            "=" when tokens[1].Value == "=" => LogicalOperation.Operators.Equals,
            _ => throw new Exception()
        };
    }

    private static Expression GetOperand(dynamic op)
    {
        return op switch
        {
            List<Token> => Parser.Parse(op)[0],
            Token => Parser.Parse(new List<Token> { op })[0],
            _ => op
        };
    }

    private static LogicalOperation ToLogicOperation(List<Token> operation)
    {
        for (var i = 0; i < operation.Count - 1; i++)
        {
            if ((operation[i] is { Type: TokenType.Operator, Value: "=" } ||
                 operation[i] is { Type: TokenType.LogicOperator }) && i + 2 < operation.Count &&
                operation[i + 1] is { Type: TokenType.Operator, Value: "=" })
                return new LogicalOperation(GetOperand(operation.GetRange(0, i)),
                    GetOperand(operation.GetRange(i + 2, operation.Count - i - 2)),
                    GetOperator(operation.GetRange(i, 2)), operation[i].File, operation[i].Line);
            if(operation[i] is { Type: TokenType.LogicOperator })
                return new LogicalOperation(GetOperand(operation.GetRange(0, i)),
                    GetOperand(operation.GetRange(i + 1, operation.Count - i - 1)),
                    GetOperator(operation.GetRange(i, 2)), operation[i].File, operation[i].Line);
        }
        Error.SendError("WrongLogicOperation", "Cannot change to Logical Operation", operation[0], true);
        return null;
    }
}