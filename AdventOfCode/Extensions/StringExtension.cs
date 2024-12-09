namespace AdventOfCode.Extensions;

public static class StringExtension
{
    public static string ReplaceChar(this string input, int index, char replacement)
    {
        if (input[index] == replacement) return input;

        var result = input.Remove(index, 1);
        result = result.Insert(index, replacement.ToString());
        return result;
    }
}