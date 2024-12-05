using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day2() : BaseDay("Day2.txt")
{
    public override Task<long> ExecutePartOne() => Task.FromResult((long)Input
        .Select(report => report.Split(" ", StringSplitOptions.TrimEntries).Select(int.Parse).ToList())
        .Count(IsSafeReport));

    public override Task<long> ExecutePartTwo() => Task.FromResult((long)Input
        .Select(report => report.Split(" ", StringSplitOptions.TrimEntries).Select(int.Parse).ToList())
        .Count(IsSafeReportWithDeleteOneLevel));

    private  bool IsSafeReport(List<int> levels)
    {
        var index = 0;
        var prevAscending = levels[0] < levels[1];
        while (index < levels.Count - 1)
        {
            var isAscending = levels[index] < levels[index + 1];
            var difference = Math.Abs(levels[index] - levels[index + 1]);
            
            if (prevAscending != isAscending || difference > 3 || difference < 1)
                return false;
            index++;
            prevAscending = isAscending;
        }
        
        return true;
    }
    
    private bool IsSafeReportWithDeleteOneLevel(List<int> levels)
    {
        if (IsValidSequence(levels))
        {
            return true;
        }

        for (var i = 0; i < levels.Count; i++)
        {
            if (IsValidSequence(levels.RemoveIndex(i)))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsValidSequence(List<int> levels)
    {
        var decrease = false;
        var increase = false;

        for (var i = 1; i < levels.Count; i++)
        {
            var difference = Math.Abs(levels[i] - levels[i - 1]);
            if (difference is > 3 or < 1)
                return false;

            if (levels[i] > levels[i - 1])
            {
                increase = true;
                if (decrease) return false;
            }
            else if (levels[i] < levels[i - 1])
            {
                decrease = true;
                if (increase) return false;
            }
        }

        return true;
    }
}