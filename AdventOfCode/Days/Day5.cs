namespace AdventOfCode.Days;

public class Day5 : BaseDay
{
    private readonly List<string> _pageNumberingRules = [];
    private readonly List<string> _updatePagesNumbers = [];
    private readonly List<string> _incorrectPagesNumbers = [];
    
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
        long result = 0;
        foreach (var updatePageNumbers in _updatePagesNumbers)
        {
            var updatePageNumberArray = updatePageNumbers.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (IsRight(updatePageNumberArray))
            {
                var average = updatePageNumberArray[updatePageNumberArray.Length / 2];
                result += Convert.ToInt64(average);
            }
            else
            {
                _incorrectPagesNumbers.Add(updatePageNumbers);
            }
        }

        return Task.FromResult(result);
    }
    
    public override Task<long> ExecutePartTwo()
    {
        var result = 0L;

        foreach (var incorrectPageNumbers in _incorrectPagesNumbers)
        {
            var incorrectPageNumberArray = incorrectPageNumbers.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var correctUpdateNumbers = OrganizePageNumbers(incorrectPageNumberArray);
            
            var average = correctUpdateNumbers[correctUpdateNumbers.Count / 2];
            result += Convert.ToInt64(average);
        }

        return Task.FromResult(result);
    }

    private List<string> OrganizePageNumbers(string[] updatePageNumberArray)
    {
        var result = new List<string>();
        var pages = updatePageNumberArray.ToList();
        
        var index = 0;
        while (index < pages.Count)
        {
            var currentPage = pages[index];
            var rules = _pageNumberingRules.Where(x => x.StartsWith(currentPage)).ToHashSet();
            var checks = pages.Where((t, i) => i != index).Select(t => $"{currentPage}|{t}").ToList();
            if (rules.IsSupersetOf(checks))
            {
                result.Add(currentPage);
                pages.RemoveAt(index);
                index = -1;
            }
            
            index++;
        }

        return result;
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
}