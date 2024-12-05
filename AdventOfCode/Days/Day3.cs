using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public partial class Day3() : BaseDay("Day3.txt")
{
    private readonly Regex _regex = InstructionRegex();
    private readonly Regex _instructionPartTwoRegex = InstructionPartTwoRegex();

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
        var result = 0L;
        var skipValues = false;
        foreach (var line in Input)
        {
            var matches = _instructionPartTwoRegex.Matches(line);
            foreach (var match in matches.Cast<Match>())
            {
                switch (match.Value)
                {
                    case "don't()":
                        skipValues = true;
                        continue;
                    case "do()":
                        skipValues = false;
                        continue;
                }
                
                if (skipValues) continue;
                
                result += ParseInstruction(match.Value);
            }
        }
        
        return Task.FromResult(result);
    }

    private static long CalculateInstructions(MatchCollection matches)
    {
        return matches
            .AsParallel()
            .Select(match => ParseInstruction(match.Value))
            .Sum();
    }

    private static long ParseInstruction(string instruction)
    {
        instruction = instruction.Replace("mul(", "").Replace(")", "");
        var digits = instruction.Split(',');
        var multiply = long.Parse(digits[0]) * long.Parse(digits[1]);
        return multiply;
    }
    
    [GeneratedRegex(@"mul\(\d+,\d+\)")]
    private static partial Regex InstructionRegex();
    
    [GeneratedRegex(@"mul\(\d+,\d+\)|don't\(\)|do\(\)")]
    private static partial Regex InstructionPartTwoRegex();
}