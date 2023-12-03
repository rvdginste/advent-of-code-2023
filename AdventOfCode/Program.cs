using System;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode;

public static class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            await Solver.SolveLast(opt => opt.ClearConsole = false);
        }
        else if (args.Length == 1 && args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase))
        {
            await Solver.SolveAll(opt =>
            {
                opt.ShowConstructorElapsedTime = true;
                opt.ShowTotalElapsedTimePerDay = true;
            });
        }
        else
        {
            var indexes = args.Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);
            await Solver.Solve(indexes.Where(i => i < uint.MaxValue));
        }
    }
}
