using System.Collections.Generic;
using Lya.Objects;

namespace Lya
{
    public static class Utils
    {
        public static List<List<Token>> SplitTokensOnType(List<Token> list, TokenType type)
        {
            var finalList = new List<List<Token>>();
            var currentList = new List<Token>();
            foreach (var val in list)
            {
                if (val.Type == type)
                {
                    finalList.Add(currentList);
                    currentList = new List<Token>();
                }
                else
                    currentList.Add(val);
            }
            finalList.Add(currentList);
            return finalList;
        }

        public static VariableType GetVariableTypeFromKeyword(string keyword)
        {
            return keyword switch
            {
                "int" => VariableType.Integer,
                "bool" => VariableType.Bool,
                "float" => VariableType.Float,
                "string" => VariableType.String,
                _ => VariableType.Null
            };
        }
    }
}