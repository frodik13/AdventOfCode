namespace AdventOfCode.Days;

public class Day1() : BaseDay("Day1.txt")
{
    private readonly List<int> _inputOne = [];
    private readonly List<int> _inputTwo = [];

    public override Task<long> ExecutePartOne()
    {
        ParseInput(Input);
        Sort();
        return Task.FromResult(CalculateDistance());
    }

    public override  Task<long> ExecutePartTwo()
    {
        ParseInput(Input);
        Sort();
        return Task.FromResult(Analysis());
    }
    
    private void ParseInput(string[] input)
    {
        foreach (var str in input)
        {
            var s = str.Split(' ');
            var one = int.Parse(s[0]);
            var two = int.Parse(s[3]);
            
            _inputOne.Add(one);
            _inputTwo.Add(two);
        }
    }

    private void Sort()
    {
        _inputOne.Sort();
        _inputTwo.Sort();
    }

    private long CalculateDistance()
    {
        return _inputOne.Select((t, i) => Math.Abs(t - _inputTwo[i])).Sum();
    }
    
    private long Analysis()
    {
        return (from one in _inputOne let count = _inputTwo.Count(x => x == one) select count * one).Sum();
    }
}