using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public partial class Day3() : BaseDay("Day3.txt")
{
    private readonly Regex _regex = InstructionRegex();

    public override Task<long> ExecutePartOne()
    {
        return Task.FromResult(Input.AsParallel()
            .Select(line =>
            {
                var matches = _regex.Matches(line);
                return CalculateInstructions(matches);
            })
            .Sum());
    }
    
    public override Task<long> ExecutePartTwo()
    {
        throw new NotImplementedException();
    }
    
    private static long CalculateInstructions(MatchCollection matches)
    {
        return matches
            .AsParallel()
            .Select(match =>
            {
                var instruction = match.Value;
                instruction = instruction.Replace("mul(", "").Replace(")", "");
                var digits = instruction.Split(',');
                var multiply = long.Parse(digits[0]) * long.Parse(digits[1]);
                return multiply;
            })
            .Sum();
    }
    
    [GeneratedRegex(@"mul\(\d*,\d*\)")]
    private static partial Regex InstructionRegex();
}