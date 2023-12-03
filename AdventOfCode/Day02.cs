using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    public record Grab(int red, int green, int blue);

    public record Game(int id, IEnumerable<Grab> grabs);

    private Game FetchSingleGame(string line)
    {
        StringSplitOptions options = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
        string[] gameParts = line.Split(':', options);
        int gameId = Convert.ToInt32(gameParts[0].Split(' ', options)[1]);
        string[] grabs = gameParts[1].Split(';', options);

        return new Game(
            gameId,
            grabs
                .Select(grab =>
                {
                    IDictionary<string, int> dictionary =
                        grab
                            .Split(',', options)
                            .Select(color => color.Split(' ', options))
                            .ToDictionary(
                                c => c[1],
                                c => Convert.ToInt32(c[0]));
                    dictionary.TryGetValue("red", out int red);
                    dictionary.TryGetValue("green", out int green);
                    dictionary.TryGetValue("blue", out int blue);
                    return new Grab(red, green, blue);
                })
                .ToArray());
    }

    private IEnumerable<Game> FetchGames()
        => File.ReadAllLines(InputFilePath)
            .Select(FetchSingleGame)
            .ToArray();

    public override ValueTask<string> Solve_1()
    {
        int maxRed = 12;
        int maxGreen = 13;
        int maxBlue = 14;
        var games = FetchGames();
        int sum =
            games
                .Where(g => g.grabs.All(g => g.red <= maxRed && g.green <= maxGreen && g.blue <= maxBlue))
                .Sum(g => g.id);
        return new($"{sum:0}");
    }

    public override ValueTask<string> Solve_2()
    {
        var games = FetchGames();
        int sum =
            games
                .Select(g =>
                {
                    int red = g.grabs.Max(g => g.red);
                    int green = g.grabs.Max(g => g.green);
                    int blue = g.grabs.Max(g => g.blue);
                    return red * green * blue;
                })
                .Sum();
        return new($"{sum:0}");
    }
}
