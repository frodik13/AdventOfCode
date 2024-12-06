namespace AdventOfCode.Days;

public class Day5 : BaseDay
{
    private readonly List<string> _pageNumberingRules = [];
    private readonly List<string> _updatePagesNumbers = [];
    public Day5() : base("Day5.txt")
    {
        var list = _pageNumberingRules;
        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                list = _updatePagesNumbers;
                continue;
            }
            
            list.Add(line);
        }
    }
    
    public override Task<long> ExecutePartOne()
    {
        var result = (from updatePageNumbers in _updatePagesNumbers
            select updatePageNumbers.Split(',', StringSplitOptions.RemoveEmptyEntries)
            into updatePageNumberArray
            let isRight = IsRight(updatePageNumberArray)
            where isRight
            select updatePageNumberArray[updatePageNumberArray.Length / 2]
            into average
            select Convert.ToInt64(average)).Sum();

        return Task.FromResult(result);
    }

    private bool IsRight(string[] updatePageNumberArray)
    {
        var index = 0;
        while (index < updatePageNumberArray.Length - 1)
        {
            var currentPageNumber = updatePageNumberArray[index];
            var rules = _pageNumberingRules.Where(x => x.StartsWith(currentPageNumber)).ToList();
            for (var i = index + 1; i < updatePageNumberArray.Length - 1; i++)
            {
                var check = $"{currentPageNumber}|{updatePageNumberArray[i]}";
                if (rules.Any(x => x.Equals(check))) continue;
                return false;
            }
                
            index++;
        }

        return true;
    }

    public override Task<long> ExecutePartTwo()
    {
        throw new NotImplementedException();
    }
}