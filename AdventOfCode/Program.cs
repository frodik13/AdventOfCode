using AdventOfCode.Days;

namespace AdventOfCode;

class Program
{
    static async Task Main(string[] args)
    {
        
        var day = new Day6();
        
        await PrintPart("Part one", day.ExecutePartOne);
        await PrintPart("Part two", day.ExecutePartTwo);
        
        Console.ReadKey();
    }

    private static async Task PrintPart(string partName, Func<Task<long>> func)
    {
        try
        {
            var resultOne = await func();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{partName}: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(resultOne);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(e);
        }

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
    }
}