namespace AdventOfCode.Extensions;

public static class IEnumerableExtension
{
    public static IEnumerable<(T value, int index)> WithIndex<T>(this IEnumerable<T> source) =>
        source.Select((value, index) => (value, index));
}