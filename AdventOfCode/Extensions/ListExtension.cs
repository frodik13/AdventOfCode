namespace AdventOfCode.Extensions;

public static class ListExtension
{
    public static List<T> RemoveIndex<T>(this List<T> list, int index)
    {
        ArgumentNullException.ThrowIfNull(list);

        var result = new List<T>(list);
        result.RemoveAt(index);

        return result;
    }
}