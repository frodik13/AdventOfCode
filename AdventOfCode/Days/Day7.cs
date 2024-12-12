using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public class Day7() : BaseDay("Day7.txt")
{
    private readonly Regex _numberRegex = new(@"\d+");
    public override Task<long> ExecutePartOne()
    {
        List<long> correctCalibrationEquations = [];
        foreach (var line in Input)
        {
            var matches = _numberRegex.Matches(line);
            var sum = long.Parse(matches[0].Value);
            List<long> numbers = [];
            for (var i = matches.Count - 1; i >= 1; i--)
            {
                numbers.Add(long.Parse(matches[i].Value));
            }

            var isCorrectCalibrationEquation = false;
            var tempSum = sum;
            Stack<(int index, long sum)> prevMultiplicationNumbers = [];
            
            for (var i = 0; i < numbers.Count; i++)
            {
                if (tempSum % numbers[i] == 0)
                {
                    prevMultiplicationNumbers.Push((i, tempSum));
                    tempSum /= numbers[i];
                    if (i == numbers.Count - 1)
                        tempSum--;
                }
                else
                {
                    tempSum -= numbers[i];
                }
            }

            while (tempSum != 0 && prevMultiplicationNumbers.Count > 0)
            {
                var previous = prevMultiplicationNumbers.Pop();
                tempSum = previous.sum;
                for (var i = previous.index; i < numbers.Count; i++)
                {
                    if (i != previous.index && tempSum % numbers[i] == 0)
                    {
                        prevMultiplicationNumbers.Push((i, tempSum));
                        tempSum /= numbers[i];
                        if (i == numbers.Count - 1)
                            tempSum--;
                    }
                    else
                    {
                        tempSum -= numbers[i];
                    }
                }
            }
            isCorrectCalibrationEquation = tempSum == 0;
            if (isCorrectCalibrationEquation)
                correctCalibrationEquations.Add(sum);
            
        }
        return Task.FromResult(correctCalibrationEquations.Sum());
    }

    public override Task<long> ExecutePartTwo()
    {
        var equations = Input
            .Select(line => line.Split(": "))
            .Select(parts => new Equation(long.Parse(parts[0]), [..parts[1].Split(' ').Select(long.Parse)]))
            .ToArray();

        var validEquations = equations
            .Where(equation => equation.IsValid(false))
            .Select(equation => equation.Target)
            .ToList();

        var total = validEquations.Sum();
        
        var totalWithConcatenation = equations
            .Where(e => !validEquations.Contains(e.Target))
            .Where(e => e.IsValid(true))
            .Select(e => e.Target)
            .Sum();
        
        return Task.FromResult(totalWithConcatenation + total);
    }
}

internal enum Operation
{
    None = -1,
    Add = 0,
    Multiply = 1,
}

internal class Equation(long target, long[] Numbers)
{
    public long Target { get; } = target;
    public bool IsValid(bool useConcatenation) => IsValid(useConcatenation, 0, 0);

    private bool IsValid(bool useConcatenation, long acc, int index)
    {
        if (index == Numbers.Length) return Target == acc;
        if (acc > Target) return false;
        
        return IsValid(useConcatenation, Math.Max(acc, 1) * Numbers[index], index + 1)
            || IsValid(useConcatenation, acc + Numbers[index], index + 1)
            || (useConcatenation && IsValid(true, Concatenate(acc, Numbers[index]), index + 1));
    }

    private long Concatenate(long first, long second)
    {
        return long.Parse($"{first}{second}");
    }
}