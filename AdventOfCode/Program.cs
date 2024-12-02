using AdventOfCode.Days;

namespace AdventOfCode;

class Program
{
    static void Main(string[] args)
    {
        var str = File.ReadAllLines(Path.Combine("Puzzles", "Day2.txt"));
        // var day1 = new Day1();
        // var totalDistance = day1.Analysis(str);
        // Console.WriteLine(totalDistance);

        var day2 = new Day2();
        var totalSafeReport = day2.Analysis(str);
        Console.WriteLine(totalSafeReport);
        
        Console.ReadKey();
    }
}