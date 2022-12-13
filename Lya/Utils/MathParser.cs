using Lya.AST;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Lya.Utils;

public class MathParser
{
    static readonly Dictionary<string, int> OperatorPrecedence = new Dictionary<string, int>()
    {
        { "/", 3 },
        { "*", 3 },
        { "%", 3 },
        { "+", 2},
        { "-", 2}
    };

    public static MathOperation ParseMathOperation(IReadOnlyList<Token> expression) =>
        ToMathOperation(ShuntingYard(expression));

    static MathOperation.Operators GetOperator(Token token)
    {
        return token.Value switch
        {
            "+" => MathOperation.Operators.Add,
            "-" => MathOperation.Operators.Sub,
            "*" => MathOperation.Operators.Mul,
            "/" => MathOperation.Operators.Div,
            "%" => MathOperation.Operators.Mod,
            _ => throw new Exception()
        };
    }

    static IExpression GetOperand(dynamic op)
    {
        return op switch
        {
            List<Token> => Parser.Parse(op)[0],
            Token => Parser.Parse(new List<Token> { op })[0],
            _ => op
        };
    }

    static MathOperation ToMathOperation(List<dynamic> operation)
    {
        var operandes = new Stack<dynamic>();
        foreach (var ope in operation)
        {
            if (ope is Token { Type: TokenType.Operator } tok)
            {
                var op2 = GetOperand(operandes.Pop());
                var op1 = GetOperand(operandes.Pop());
                operandes.Push(new MathOperation(op1, op2, GetOperator(tok), tok.File, tok.Line));
            }
            else
                operandes.Push(ope);
        }

        return operandes.Pop();
    }

    static List<dynamic> ShuntingYard(IReadOnlyList<Token> expression)
    {
        var operation = new List<dynamic>();
        var stack = new Stack<Token>();

        for (var i = 0; i < expression.Count; i++)
        {
            var token = expression[i];
            switch (token.Type)
            {
                case TokenType.Number:
                    operation.Add(token);
                    break;
                case TokenType.Operator:
                {
                    while (stack.Count > 0 && stack.Peek() is { Type: TokenType.Operator } tok)
                    {
                        var c = OperatorPrecedence[token.Value].CompareTo(OperatorPrecedence[tok.Value]);
                        if (c <= 0)
                            operation.Add(stack.Pop());
                        else
                            break;
                    }

                    stack.Push(token);
                    break;
                }
                case TokenType.Paren:
                {
                    if (token.Value == "(")
                        stack.Push(token);
                    else if (token.Value == ")")
                    {
                        Token top = null;
                        while (stack.Count > 0 && (top = stack.Pop()).Value != "(")
                            operation.Add(top);
                        if (top is null || top.Value != "(")
                            Error.SendError("SyntaxError", "No matching left parenthesis", token, true);
                    }

                    break;
                }
                default:
                {
                    var current = new List<Token>();
                    var backI = i;
                    while (token.Type != TokenType.Paren && token.Type != TokenType.Number &&
                           (token.Type != TokenType.Operator || token.Value == "="))
                    {
                        current.Add(token);
                        if (i < expression.Count - 2)
                            token = expression[++i];
                        else
                            break;
                    }

                    if(i != backI)
                        i--;

                    operation.Add(current);
                    break;
                }
            }
        }
        
        while (stack.Count > 0)
        {
            var top = stack.Pop();
            if(!OperatorPrecedence.ContainsKey(top.Value))
                Error.SendError("SyntaxError", "No matching right parenthesis", top, true);
            operation.Add(top);
        }

        return operation;
    }
}
