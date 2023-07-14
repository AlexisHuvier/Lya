using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Lya.AST;
using Lya.Objects;
using Lya.Objects.TokenObjects;
using Lya.Parsers;

namespace Lya;

public static class Parser
{
    public static List<Expression> Parse(List<Token> tokens)
    {
        var expressionsTokens = tokens.SplitTokensOnType(TokenType.SemiColon);
        var expressions = new List<Expression>();
        var expressionCount = 0;
        while(expressionCount < expressionsTokens.Count)
        {
            var expression = expressionsTokens[expressionCount];
            if (expression.Count == 0)
            {
                expressionCount++;
                continue;
            }

            switch (expression[0].Type)
            {
                // COMMENT
                case TokenType.Comment:
                    break;
                
                // VARIABLE CALL
                case TokenType.Identifier when expression.Count == 1:
                {
                    expressions.Add(new VarCall(expression[0].Value, expression[0].File, expression[0].Line));
                    break;
                }
                
                // VARIABLE AFFECTATION
                case TokenType.Identifier when expression[1].Type == TokenType.Operator && expression[1].Value == "=" && (expression.Count == 2 || expression[2].Type != TokenType.Operator || expression[2].Value != "="):
                {
                    if(expression.Count == 2)
                        Error.SendError("MissingValue", "Missing value for affection", expression[1], true);
                    else
                        expressions.Add(new VarAffectation(expression[0].Value, Parse(expression.GetRange(2, expression.Count - 2)), expression[0].File, expression[0].Line));

                    break;
                }
                
                // FUNCTION CALL
                case TokenType.Identifier when expression[1].Type == TokenType.Paren && expression[1].Value == "(":
                {
                    if (expression.Count == 2 || expression.Count == 3 && expression[2].Value != ")")
                        Error.SendError("MissingParenthesis", "Missing closing parenthesis", expression[1], true);
                    else if (expression.Count == 3)
                        expressions.Add(new FunctionCall(expression[0].Value, new List<Expression>(), expression[0].File, expression[0].Line));
                    else
                        expressions.Add(new FunctionCall(expression[0].Value,
                            expression.GetRange(2, expression.Count - 3).SplitTokensOnType(TokenType.Comma).Select(x => Parse(x)[0]).ToList(), 
                            expression[0].File, expression[0].Line));
                    break;
                }
                
                // VARIABLE DECLARATION
                case TokenType.KeywordType when expression.Count >= 4 && expression[1].Type == TokenType.Identifier && expression[2].Type == TokenType.Operator && expression[2].Value == "=": 
                    expressions.Add(new VarDeclaration(expression[1].Value, VariableType.GetVariableTypeFromKeyword(expression[0].Value), Parse(expression.GetRange(3, expression.Count - 3)), expression[0].File, expression[0].Line));
                    break;
                
                // CONSTANT
                case TokenType.Number or TokenType.String when expression.Count == 1:
                    if(expression[0].Type == TokenType.String)
                        expressions.Add(new Constant(expression[0].Value, expression[0].File, expression[0].Line));
                    else if(expression[0].Value.Contains('.'))
                        expressions.Add(new Constant(Convert.ToSingle(expression[0].Value, CultureInfo.InvariantCulture), expression[0].File, expression[0].Line));
                    else
                        expressions.Add(new Constant(Convert.ToInt32(expression[0].Value), expression[0].File, expression[0].Line));
                    break;
                
                // CONDITION IF
                case TokenType.Keyword when expression.Count >= 1 && expression[0].Value == "if" && expression[1].Type == TokenType.Paren && expression[1].Value == "(":
                    var index = -1;
                    for (var i = 2; i < expression.Count; i++)
                    {
                        if (expression[i].Type == TokenType.Paren && expression[i].Value == ")")
                        {
                            index = i;
                            break;
                        }
                    }
                    if(index == -1)
                        Error.SendError("MissingParenthesis", "Missing closing parenthesis", expression[1], true);
                    var condition = Parse(expression.GetRange(2, index - 2))[0];
                    if(expression[index + 1].Type != TokenType.Bracket || expression[index + 1].Value != "{")
                        Error.SendError("MissingBracket", "Missing opening bracket", expression[index+1], true);
                    var expressionInIf = new List<Expression> { Parse(expression.GetRange(index + 2, expression.Count - index - 2))[0] };
                    expressionCount++;
                    var currentExpression = expressionsTokens[expressionCount];
                    while (currentExpression[0].Type != TokenType.Bracket || currentExpression[0].Value != "}")
                    {
                        expressionInIf.Add(Parse(currentExpression)[0]);
                        expressionCount++;
                        currentExpression = expressionsTokens[expressionCount];
                    }

                    expressions.Add(new IfExpression(condition, expressionInIf, expression[0].File, expression[0].Line));
                    break;
                
                // ERROR IMCOMPLETE DECLARATION
                case TokenType.Keyword when expression.Count >= 1 && expression[0].Value == "while" && expression[1].Type == TokenType.Paren && expression[1].Value == "(":
                    var indexWhile = -1;
                    for (var i = 2; i < expression.Count; i++)
                    {
                        if (expression[i].Type == TokenType.Paren && expression[i].Value == ")")
                        {
                            indexWhile = i;
                            break;
                        }
                    }
                    if(indexWhile == -1)
                        Error.SendError("MissingParenthesis", "Missing closing parenthesis", expression[1], true);
                    var conditionWhile = Parse(expression.GetRange(2, indexWhile - 2))[0];
                    if(expression[indexWhile + 1].Type != TokenType.Bracket || expression[indexWhile + 1].Value != "{")
                        Error.SendError("MissingBracket", "Missing opening bracket", expression[indexWhile+1], true);
                    var expressionInWhile = new List<Expression> { Parse(expression.GetRange(indexWhile + 2, expression.Count - indexWhile - 2))[0] };
                    expressionCount++;
                    var currentExpressionWhile = expressionsTokens[expressionCount];
                    while (currentExpressionWhile[0].Type != TokenType.Bracket || currentExpressionWhile[0].Value != "}")
                    {
                        expressionInWhile.Add(Parse(currentExpressionWhile)[0]);
                        expressionCount++;
                        currentExpressionWhile = expressionsTokens[expressionCount];
                    }

                    expressions.Add(new WhileExpression(conditionWhile, expressionInWhile, expression[0].File, expression[0].Line));
                    break;
                case TokenType.KeywordType:
                    Error.SendError("InvalidDeclaration", "Incomplete declaration", expression[0], true);
                    break;
                
                // ERROR IMCOMPLETE EXPRESSION
                case TokenType.Keyword:
                    Error.SendError("InvalidExpression", "Incomplete expression", expression[0], true);
                    break;
                
                // MATH / LOGICAL OPERATION OR UNKNOWN EXPRESSION
                default:
                    if (expression.Count > 2)
                    {
                        var found = false;
                        for (var i = 1; i < expression.Count - 1; i++)
                        {
                            if (expression[i].Type == TokenType.LogicOperator ||
                                (expression[i].Type == TokenType.Operator && expression[i].Value == "=" &&
                                 expression[i + 1].Type == TokenType.Operator && expression[i + 1].Value == "="))
                            {
                                found = true;
                                expressions.Add(LogicParser.ParseLogicOperation(expression));
                                break;
                            }

                            if (expression[i].Type == TokenType.Operator && expression[i].Value != "=")
                            {
                                found = true;
                                expressions.Add(MathParser.ParseMathOperation(expression));
                                break;
                            }
                        }
                        
                        if(found)
                            break;
                    }
                    Error.SendError("Unknown", $"Unknown expression (Tokens : {string.Join(", ", expression)})", expression[0], true);
                    break;
            }

            expressionCount++;
        }
        return expressions;
    }
}