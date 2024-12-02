namespace AdventOfCode.Days;

public class Day1
{
    private readonly List<int> _inputOne = [];
    private readonly List<int> _inputTwo = [];
    
    public int Distance(string[] input)
    {
        ParseInput(input);
        Sort();
        return CalculateDistance();
    }

    public int Analysis(string[] input)
    {
        ParseInput(input);
        Sort();
        return Analysis();
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

    private int CalculateDistance()
    {
        return _inputOne.Select((t, i) => Math.Abs(t - _inputTwo[i])).Sum();
    }
    
    private int Analysis()
    {
        return (from one in _inputOne let count = _inputTwo.Count(x => x == one) select count * one).Sum();
    }
}