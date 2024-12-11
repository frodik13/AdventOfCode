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
        throw new NotImplementedException();
    }
}