using AdventOfCode.Days;

namespace AdventOfCode;

class Program
{
    static void Main(string[] args)
    {
        var str = File.ReadAllLines(Path.Combine("Puzzles", "Day1.txt"));
        var day1 = new Day1();
        var totalDistance = day1.Analysis(str);

        Console.WriteLine(totalDistance);
        Console.ReadKey();
    }
}