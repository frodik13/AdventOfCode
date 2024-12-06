using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day6() : BaseDay("Day6.txt")
{
    public override Task<long> ExecutePartOne()
    {
        var result = 0L;
        var currentPosition = FindStartPosition();
        while (true)
        {
            var nextPosition = CalculateNextPosition(currentPosition);
            if (nextPosition.x == 0 || nextPosition.y == 0 || nextPosition.x == Input.Length - 1
                || nextPosition.y == Input[0].Length - 1)
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

            if (Input[currentPosition.x][currentPosition.y] != 'X')
            {
                result++;
            }
            Input[currentPosition.x] = Input[currentPosition.x].ReplaceChar(currentPosition.y, 'X');
            currentPosition.x = nextPosition.x;
            currentPosition.y = nextPosition.y;
        }
        
        
        return Task.FromResult(result);
    }

    private char ReplaceDirection(char direction)
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

    private (int x, int y) CalculateNextPosition((int x, int y, char direction) currentPosition)
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

    public override Task<long> ExecutePartTwo()
    {
        throw new NotImplementedException();
    }
}