using System.Text;
using AdventOfCode.Extensions;

namespace AdventOfCode.Days;

public class Day9() : BaseDay("Day9.txt")
{
    public override Task<long> ExecutePartOne()
    {
        var str = Input[0];
        
        var blocks = ReadInput(str);
        
        MoveFiles(blocks);

        return Task.FromResult(CheckSum(blocks));
    }

    private static List<int> ReadInput(string str)
    {
        List<int> blocks = [];
        var id = -1;
        for (var i = 0; i < str.Length; i++)
        {
            var number = int.Parse(str[i].ToString());
            if (i % 2 == 0)
            {
                id++;
                blocks.AddElementCertainNumberOfElements(id, number);
            }
            else
                blocks.AddElementCertainNumberOfElements(-1, number);
        }

        return blocks;
    }

    private static long CheckSum(List<int> blocks)
    {
        long checkSum = 0;
        for (var i = 0; i < blocks.Count; i++)
        {
            if (blocks[i] != -1)
                checkSum += blocks[i] * i;
        }

        return checkSum;
    }

    private void MoveFiles(List<int> blocks)
    {
        var leftIndex = -1;
        var rightIndex = blocks.Count;

        while (true)
        {
            while (true)
            {
                leftIndex++;
                if (leftIndex >= rightIndex) return;
                if (leftIndex == blocks.Count) return;
                if (blocks[leftIndex] == -1) break;
            }

            while (true)
            {
                rightIndex--;
                if (rightIndex < 0) return;
                if (rightIndex <= leftIndex) return;
                if (blocks[rightIndex] != -1) break;
            }
            
            blocks[leftIndex] = blocks[rightIndex];
            blocks[rightIndex] = -1;
        }
    }

    public override Task<long> ExecutePartTwo()
    {
        throw new NotImplementedException();
    }
}