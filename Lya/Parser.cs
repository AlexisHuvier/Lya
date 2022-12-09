using Lya.AST;
using System.Collections.Generic;

namespace Lya;

public static class Parser
{
    static bool CheckVarExists(Env env, Token varToken)
    {
        if (env.Variables.ContainsKey(varToken.Value))
            return true;
        Error.SendError("VariableUndefined", $"{varToken.Value} is not defined", varToken, true);
        return false;
    }
    
    static bool CheckVarAlreadyExists(Env env, Token varToken)
    {
        if (!env.Variables.ContainsKey(varToken.Value))
            return true;
        Error.SendError("VariableAlreadyDefined", $"{varToken.Value} is already defined", varToken, true);
        return false;
    }
    
    public static dynamic Parse(List<Token> tokens, Env env = null)
    {
        env ??= Env.GetStandartEnv();
        
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
                    expressions.Add(new VarCall() { VarName = expression[0].Value });
                    break;
                }
                case TokenType.Identifier when expression[1].Type == TokenType.Operator && expression[1].Value == "=":
                {
                    if(expression.Count == 2)
                        Error.SendError("SyntaxError", "Missing value for affection", expression[1], true);
                    else
                        expressions.Add(new VarAffectation() { VarName = expression[0].Value, Value = Parse(expression.GetRange(2, expression.Count - 2))});

                    break;
                }
                case TokenType.KeywordType when expression.Count >= 4 && expression[1].Type == TokenType.Identifier && expression[2].Type == TokenType.Operator && expression[2].Value == "=": 
                    expressions.Add(new VarDeclaration() {VarName = expression[1].Value, Type = VariableType.GetVariableTypeFromKeyword(expression[0].Value), Value = Parse(expression.GetRange(3, expression.Count - 3))});
                    break;
                case TokenType.Number or TokenType.String when expression.Count == 1:
                    return new Constant() { Value = expression[0].Value };
                case TokenType.KeywordType:
                    Error.SendError("InvalidDeclaration", "Incomplete declaration", expression[0], true);
                    break;
                default:
                    Error.SendError("UnknownExpression", "Unknown expression", expression[0], true);
                    break;
            }
        }

        return expressions;
    }
}