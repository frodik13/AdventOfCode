using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day6() : BaseDay("Day6.txt")
{
    public override Task<long> ExecutePartOne()
    {
        var result = 0L;
        var currentPosition = FindStartPosition();
        var temp = Input.ToArray();
        while (true)
        {
            var nextPosition = CalculateNextPosition(currentPosition);
            if (nextPosition.x == 0 || nextPosition.y == 0 || nextPosition.x == temp.Length - 1
                || nextPosition.y == temp[0].Length - 1)
            {
                if (CheckPositionInObstacle(nextPosition))
                {
                    currentPosition.direction = ReplaceDirection(currentPosition.direction);
                    continue;
                }

                result += 2;
                break;
            }

            if (CheckPositionInObstacle(nextPosition))
            {
                currentPosition.direction = ReplaceDirection(currentPosition.direction);
                continue;
            }

            if (temp[currentPosition.x][currentPosition.y] != 'X')
            {
                result++;
            }
            temp[currentPosition.x] = temp[currentPosition.x].ReplaceChar(currentPosition.y, 'X');
            currentPosition.x = nextPosition.x;
            currentPosition.y = nextPosition.y;
        }
        
        
        return Task.FromResult(result);
    }
    
    public override Task<long> ExecutePartTwo()
    {
        var result = 0L;
        
        var gaurdCoord = Input.SelectMany((x, i) => x.Select((y, j) =>
        {
            if (y == '^')
            {
                return new Coordinate(i, j);
            }
            return null;
        })).Single(x => x is not null);
        var rowLen = Input.Length;
        var colLen = Input[0].Length;
        
        var direction = Direction.Up;
        var seen = new HashSet<Coordinate>();
        var tempGaurdCoord = gaurdCoord!;
        
        var tempInput = Input.Select(x => x.Select(y => y).ToArray()).ToArray();
        
        for (int i = 0; i < rowLen; i++)
        {
            for (int j = 0; j < colLen; j++)
            {
                var temp = tempInput[i][j];
                if (temp == '#')
                {
                    continue;
                }
                tempInput[i][j] = '#';
                tempGaurdCoord = gaurdCoord!;
                direction = Direction.Up;
                if (IsCycle(tempGaurdCoord, direction, rowLen, colLen, tempInput))
                {
                    result++;
                }
                tempInput[i][j] = temp;
            }
        }

        return Task.FromResult(result);
    }

    private static bool IsCycle(Coordinate gaurdCoord, Direction direction, int rowLen, int colLen, char[][] input)
    {
        var seenObstacle = new HashSet<(Coordinate, Direction)>();
        while (IsInGrid(gaurdCoord, rowLen, colLen))
        {
            if (input[gaurdCoord.Row][gaurdCoord.Col] == '#')
            {
                if (!seenObstacle.Add((gaurdCoord, direction)))
                {
                    return true;
                }
                gaurdCoord = GetPrevCoordinate(gaurdCoord!, direction);
                direction = GetNextDirection(direction);
            } 
            gaurdCoord = GetNextCoordinate(gaurdCoord!, direction);
        }
        return false;
    }

    private static Coordinate GetNextCoordinate(Coordinate coord, Direction direction)
    {
        return direction switch
        {
            Direction.Up => coord with { Row = coord.Row - 1 },
            Direction.Down => coord with { Row = coord.Row  + 1 },
            Direction.Left => coord with { Col = coord.Col - 1 },
            Direction.Right => coord with { Col = coord.Col + 1 },
            _ => throw new NotImplementedException(),
        };
    }

    private static Direction GetNextDirection(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new NotImplementedException(),
        };
    }

    private static Coordinate GetPrevCoordinate(Coordinate coord, Direction direction)
    {
        return direction switch
        {
            Direction.Up => coord with { Row = coord.Row + 1 },
            Direction.Down => coord with { Row = coord.Row - 1 },
            Direction.Left => coord with { Col = coord.Col + 1 },
            Direction.Right => coord with { Col = coord.Col - 1 },
            _ => throw new NotImplementedException(),
        };
    }

    private static bool IsInGrid(Coordinate coord, int rowLen, int colLen)
    {
        return 0 <= coord.Row && coord.Row < rowLen && 0 <= coord.Col && coord.Col < colLen;
    }
    
    private static char ReplaceDirection(char direction)
    {
        return direction switch {
            '^' => '>',
            '>' => 'v',
            'v' => '<',
            '<' => '^',
            _ => direction
        };
    }

    private bool CheckPositionInObstacle((int x, int y) nextPosition)
    {
        var (x, y) = nextPosition;
        return Input[x][y] == '#';
    }

    private static (int x, int y) CalculateNextPosition((int x, int y, char direction) currentPosition)
    {
        var (i, j, direction) = currentPosition;
        (int, int) nextPosition;
        nextPosition = direction switch {
            '^' => (i - 1, j),
            '>' => (i, j + 1),
            '<' => (i, j - 1),
            'v' => (i + 1, j),
            _ => (i, j)
        };

        return nextPosition;
    }

    private (int x, int y, char direction) FindStartPosition()
    {
        for (var i = 0; i < Input.Length; i++)
        {
            for (var j = 0; j < Input[0].Length; j++)
            {
                if (Input[i][j] != '^' && Input[i][j] != '<' && Input[i][j] != '>' && Input[i][j] != 'v') 
                    continue;
                return (i, j, Input[i][j]);
            }
        }

        return (0, 0, ' ');
    }
}

public record Coordinate(int Row, int Col);
public enum Direction
{
    Up,
    Right,
    Down,
    Left
}