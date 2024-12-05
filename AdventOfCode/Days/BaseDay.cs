namespace AdventOfCode.Days;

public abstract class BaseDay : IDay
{
    protected string[] Input;
    
    protected BaseDay(string path)
    {
        Input = File.ReadAllLines(Path.Combine("Puzzles", path));
    }
    
    public abstract Task<long> ExecutePartOne();
    public abstract Task<long> ExecutePartTwo();
}