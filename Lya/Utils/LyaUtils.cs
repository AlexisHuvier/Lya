using System.Collections.Generic;

namespace Lya;

public static class LyaUtils
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
}