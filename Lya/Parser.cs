using Lya.AST;
using Lya.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lya;

public static class Parser
{
    public static List<IExpression> Parse(List<Token> tokens)
    {
        var expressionsTokens = LyaUtils.SplitTokensOnType(tokens, TokenType.SemiColon);
        var expressions = new List<IExpression>();
        foreach (var expression in expressionsTokens)
        {
            if (expression.Count == 0)
                continue;

            switch (expression[0].Type)
            {
                case TokenType.Identifier when expression.Count == 1:
                {
                    expressions.Add(new VarCall(expression[0].Value, expression[0].File, expression[0].Line));
                    break;
                }
                case TokenType.Identifier when expression[1].Type == TokenType.Operator && expression[1].Value == "=":
                {
                    if(expression.Count == 2)
                        Error.SendError("SyntaxError", "Missing value for affection", expression[1], true);
                    else
                        expressions.Add(new VarAffectation(expression[0].Value, Parse(expression.GetRange(2, expression.Count - 2)), expression[0].File, expression[0].Line));

                    break;
                }
                case TokenType.Identifier when expression[1].Type == TokenType.Paren && expression[1].Value == "(":
                {
                    if (expression.Count == 2 || expression.Count == 3 && expression[2].Value != ")")
                        Error.SendError("SyntaxError", "Missing closing parenthesis", expression[1], true);
                    else if (expression.Count == 3)
                        expressions.Add(new FunctionCall(expression[0].Value, new List<IExpression>(), expression[0].File, expression[0].Line));
                    else
                        expressions.Add(new FunctionCall(expression[0].Value,Parse(expression.GetRange(2, expression.Count - 3)), expression[0].File, expression[0].Line));
                    break;
                }
                case TokenType.KeywordType when expression.Count >= 4 && expression[1].Type == TokenType.Identifier && expression[2].Type == TokenType.Operator && expression[2].Value == "=": 
                    expressions.Add(new VarDeclaration(expression[1].Value, VariableType.GetVariableTypeFromKeyword(expression[0].Value), Parse(expression.GetRange(3, expression.Count - 3)), expression[0].File, expression[0].Line));
                    break;
                case TokenType.Number or TokenType.String when expression.Count == 1:
                    if(expression[0].Type == TokenType.String)
                        expressions.Add(new Constant(expression[0].Value, expression[0].File, expression[0].Line));
                    else if(expression[0].Value.Contains('.'))
                        expressions.Add(new Constant(Convert.ToSingle(expression[0].Value, CultureInfo.InvariantCulture), expression[0].File, expression[0].Line));
                    else
                        expressions.Add(new Constant(Convert.ToInt32(expression[0].Value), expression[0].File, expression[0].Line));
                    break;
                case TokenType.KeywordType:
                    Error.SendError("InvalidDeclaration", "Incomplete declaration", expression[0], true);
                    break;
                default:
                    Error.SendError("UnknownExpression", $"Unknown expression (Number of Tokens : {expression.Count} {string.Join(", ", expression.Select(x => x.Value))})", expression[0], true);
                    break;
            }
        }

        return expressions;
    }
}