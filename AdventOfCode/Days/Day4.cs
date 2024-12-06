namespace AdventOfCode.Days;

public class Day4() : BaseDay("Day4.txt")
{
    public override Task<long> ExecutePartOne()
    {
        var result = 0L;
        
        for (var i = 0; i < Input.Length; i++)
        for (var j = 0; j < Input[0].Length; j++)
            if (Input[i][j] == 'X')
                result += FindCountWords(i, j);

        return Task.FromResult(result);
    }
    
    public override Task<long> ExecutePartTwo()
    {
        var result = 0L;
        
        for (var i = 0; i < Input.Length; i++)
        for (var j = 0; j < Input[0].Length; j++)
            if (Input[i][j] == 'A')
                result += FindX(i, j);

        return Task.FromResult(result);
    }

    private long FindX(int i, int j)
    {
        var isX = false;
        var isY = false;
        if (IsCorrectChar(i - 1, j - 1, 'M'))
        {
            if (IsCorrectChar(i + 1, j + 1, 'S'))
            {
                isX = true;
            }
        }
        else if (IsCorrectChar(i - 1, j - 1, 'S'))
        {
            if (IsCorrectChar(i + 1, j + 1, 'M'))
            {
                isX = true;
            }
        }

        if (IsCorrectChar(i + 1, j - 1, 'M'))
        {
            if (IsCorrectChar(i - 1, j + 1, 'S'))
            {
                isY = true;
            }
        }
        else if (IsCorrectChar(i + 1, j - 1, 'S'))
        {
            if (IsCorrectChar(i - 1, j + 1, 'M'))
            {
                isY = true;
            }
        }
        

        return isX && isY ? 1 : 0;
    }
    
    private long FindCountWords(int i, int j)
    {
        return Enum.GetValues<Direction>().LongCount(direction => IsFindWord(i, j, direction));
    }

    private bool IsFindWord(int i, int j, Direction direction)
    {
        var nextPoint = NextPoint(direction, i, j);
        if (!IsCorrectChar(nextPoint.i, nextPoint.j, 'M')) return false;
        
        nextPoint = NextPoint(direction, nextPoint.i, nextPoint.j);
        if (!IsCorrectChar(nextPoint.i, nextPoint.j, 'A')) return false;
        
        nextPoint = NextPoint(direction, nextPoint.i, nextPoint.j);
        return IsCorrectChar(nextPoint.i, nextPoint.j, 'S');
    }
    
    private bool IsCorrectChar(int i, int j, char c)
    {
        return i < Input.Length && j < Input[0].Length && i >= 0 && j >= 0 && Input[i][j] == c;
    }
    
    private static (int i, int j) NextPoint(Direction direction, int i, int j)
    {
        return direction switch
        {
            Direction.Down => (i + 1, j),
            Direction.Up => (i - 1, j),
            Direction.Left => (i, j + 1),
            Direction.Right => (i, j - 1),
            Direction.DownRight => (i + 1, j + 1),
            Direction.DownLeft => (i + 1, j - 1),
            Direction.UpLeft => (i - 1, j - 1),
            Direction.UpRight => (i - 1, j + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
    
    private enum Direction
    {
        Down,
        Up,
        Left,
        Right,
        DownRight,
        DownLeft,
        UpLeft,
        UpRight,
    }
}