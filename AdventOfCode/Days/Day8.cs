namespace AdventOfCode.Days;

public class Day8 : BaseDay
{
    private readonly Dictionary<char, List<(int x, int y)>> _map = new();
    private readonly HashSet<(int x, int y)> _antiNodes = [];
    public Day8() : base("Day8.txt")
    {
        for (var i = 0; i < Input.Length; i++)
        {
            for (var j = 0; j < Input[0].Length; j++)
            {
                if (Input[i][j] == '.') continue;
                
                if (_map.TryGetValue(Input[i][j], out var value))
                {
                    value.Add((i, j));
                }
                else
                {
                    _map.Add(Input[i][j], [(i, j)]);
                }
            }
        }
    }

    public override Task<long> ExecutePartOne()
    {
        foreach (var coordinates in _map.Select(map => map.Value))
        {
            for (var i = 0; i < coordinates.Count - 1; i++)
            {
                for (var j = i + 1; j < coordinates.Count; j++)
                {
                    var (x, y) = coordinates[i];
                    var (x1, y1) = coordinates[j];
                    var differenceX = Math.Abs(x - x1);
                    var differenceY = Math.Abs(y - y1);
                    if (x == x1)
                    {
                        if (y - differenceY >= 0)
                            _antiNodes.Add((x, y - differenceY));
                        if (y1 + differenceY < Input[0].Length)
                            _antiNodes.Add((x, y1 + differenceY));
                    }
                    else if (y == y1)
                    {
                        if (x - differenceX >= 0)
                            _antiNodes.Add((x - differenceX, y));
                        if (x1 + differenceX < Input.Length)
                            _antiNodes.Add((x1 + differenceX, y));
                    }
                    else if (x < x1 && y < y1)
                    {
                        if (x - differenceX >= 0 && y - differenceY >= 0)
                            _antiNodes.Add((x - differenceX, y - differenceY));
                        if (x1 + differenceX < Input.Length && y1 + differenceY < Input[0].Length)
                            _antiNodes.Add((x1 + differenceX, y1 + differenceY));
                    }
                    else
                    {
                        if (x - differenceX >= 0 && y + differenceY < Input[0].Length)
                            _antiNodes.Add((x - differenceX, y + differenceY));
                        if (x1 + differenceX < Input.Length && y1 - differenceY >= 0)
                            _antiNodes.Add((x1 + differenceX, y1 - differenceY));
                    }
                }
            }
        }

        return Task.FromResult((long)_antiNodes.Count);
    }

    public override Task<long> ExecutePartTwo()
    {
        var rowLen = Input.Length;
        var colLen = Input[0].Length;
        var uniqueAntiNodes = new HashSet<Coordinate>();
        
        var antennas = Input.SelectMany((x, i) => x.Select((y, j) => (Freq: y, Coord: new Coordinate(i, j))))
            .Where(x => x.Freq != '.')
            .ToList();
        
        var antennaToCoordinates = antennas.GroupBy(x => x.Freq)
            .ToDictionary(x => x.Key, x => x.Select(y => y.Coord).ToList());
        
        foreach (var antenna in antennas)
        {
            var sameFrequencyAntennas = antennaToCoordinates[antenna.Freq]
                .Where(x => x.Row != antenna.Coord.Row || x.Col != antenna.Coord.Col)
                .ToList();

            foreach (var coord in sameFrequencyAntennas)
            {
                var delta = antenna.Coord - coord;
                var potentialAntinodeA = antenna.Coord + delta;
                var potentialAntinodeB = antenna.Coord - delta;

                while (IsInGrid(potentialAntinodeA, rowLen, colLen))
                {
                    uniqueAntiNodes.Add(potentialAntinodeA);
                    potentialAntinodeA = potentialAntinodeA + delta;
                }
                while (IsInGrid(potentialAntinodeB, rowLen, colLen))
                {
                    uniqueAntiNodes.Add(potentialAntinodeB);
                    potentialAntinodeB = potentialAntinodeB - delta;
                }
            }
        }
        
        return Task.FromResult((long)uniqueAntiNodes.Count);
    }

    public static bool IsInGrid(Coordinate coord, int rowLen, int colLen) =>
        0 <= coord.Row && coord.Row < rowLen && 0 <= coord.Col && coord.Col < colLen;
    
    public sealed record Coordinate(int Row, int Col)
    {
        public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.Row + b.Row, a.Col + b.Col);

        public static Coordinate operator -(Coordinate a, Coordinate b) => new(a.Row - b.Row, a.Col - b.Col);
    }
}