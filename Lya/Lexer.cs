﻿using System.Collections.Generic;
using System.Linq;
using Lya.Objects;
using Lya.Objects.TokenObjects;

namespace Lya;

public static class Lexer
{
    enum LexerState
    {
        Basic,
        String,
        Identifier,
        Number
    }
    
    public static List<Token> Tokenize(string program, string name = "_")
    {
        var tokens = new List<Token>();
        var state = LexerState.Basic;
        var currentToken = new Token { File = name };
        var line = 0;

        foreach (var currentLine in program.Replace("\r", "").Split("\n"))
        {
            line++;
            var column = 0;
            foreach (var currentCharacter in currentLine)
            {
                column++;
                
                switch (state)
                {
                    case LexerState.String when currentCharacter == '"':
                        tokens.Add(currentToken);
                        currentToken = new Token
                        {
                            Column = column,
                            Line = line,
                            File = name
                        };
                        state = LexerState.Basic;
                        continue;
                    case LexerState.Number when char.IsNumber(currentCharacter):
                    case LexerState.String:
                    case LexerState.Identifier when char.IsLetter(currentCharacter):
                        currentToken.Value += currentCharacter;
                        break;
                    case LexerState.Number when currentCharacter == '.' && !currentToken.Value.Contains('.'):
                        currentToken.Value += '.';
                        break;
                    case LexerState.Number:
                    case LexerState.Identifier:
                        tokens.Add(currentToken);
                        currentToken = new Token
                        {
                            Column = column,
                            Line = line,
                            File = name
                        };
                        state = LexerState.Basic;
                        break;
                    case LexerState.Basic:
                        tokens.Add(currentToken);
                        currentToken = new Token
                        {
                            Column = column,
                            Line = line,
                            File = name
                        };
                        break;
                }

                if (state == LexerState.Basic)
                {
                    switch (currentCharacter)
                    {
                        case ' ':
                            currentToken.Value += " ";
                            currentToken.Type = TokenType.Whitespace;
                            break;
                        case '(': case ')':
                            currentToken.Value += currentCharacter;
                            currentToken.Type = TokenType.Paren;
                            break;
                        case '#':
                            currentToken.Value += currentCharacter;
                            currentToken.Type = TokenType.Comment;
                            break;
                        case '{': case '}':
                            currentToken.Value += currentCharacter;
                            currentToken.Type = TokenType.Bracket;
                            break;
                        case '[': case ']':
                            currentToken.Value += currentCharacter;
                            currentToken.Type = TokenType.Hook;
                            break;
                        case ';':
                            currentToken.Value = ";";
                            currentToken.Type = TokenType.SemiColon;
                            break;
                        case ',':
                            currentToken.Value = ",";
                            currentToken.Type = TokenType.Comma;
                            break;
                        case '>': case '<':
                            currentToken.Value += currentCharacter;
                            currentToken.Type = TokenType.LogicOperator;
                            break;
                        case '+': case '-': case '*': case '/': case '=': case '%':
                            currentToken.Value += currentCharacter;
                            currentToken.Type = TokenType.Operator;
                            break;
                        case '1': case '2': case '3': case '4': case '5': case '6': case '7':
                        case '8': case '9': case '0':
                            currentToken.Value += currentCharacter;
                            currentToken.Type = TokenType.Number;
                            state = LexerState.Number;
                            break;
                        case '"':
                            currentToken.Type = TokenType.String;
                            state = LexerState.String;
                            break;
                        default:
                            currentToken.Value += currentCharacter;
                            state = LexerState.Identifier;
                            currentToken.Type = TokenType.Identifier;
                            break;
                    }
                }
            }
        }
        tokens.Add(currentToken);

        tokens.RemoveAll(token => token.Type is TokenType.Unknown or TokenType.Whitespace);
        foreach (var token in tokens.Where(token => token.Type == TokenType.Identifier && Env.Keywords.Contains(token.Value)))
            token.Type = TokenType.Keyword;
        foreach (var token in tokens.Where(token => token.Type == TokenType.Identifier && Env.KeywordTypes.Contains(token.Value)))
            token.Type = TokenType.KeywordType;
        
        return tokens;
    }
}