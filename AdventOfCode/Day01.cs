using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode;

public class Day01 : BaseDay
{
    public override ValueTask<string> Solve_1()
    {
        long total =
            File
                .ReadAllLines(InputFilePath)
                .Select(l => l.Where(char.IsDigit).ToArray())
                .Where(l => l.Length != 0)
                .Select(l => Convert.ToInt64(new string(new[] { l[0], l[^1] })))
                .Sum();

        return new ValueTask<string>($"{total:0}");
    }

    private long RetrieveNumber(string line)
    {
        ReadOnlySpan<char> one = "one";
        ReadOnlySpan<char> two = "two";
        ReadOnlySpan<char> three = "three";
        ReadOnlySpan<char> four = "four";
        ReadOnlySpan<char> five = "five";
        ReadOnlySpan<char> six = "six";
        ReadOnlySpan<char> seven = "seven";
        ReadOnlySpan<char> eight = "eight";
        ReadOnlySpan<char> nine = "nine";
        ReadOnlySpan<char> characters = line;
        Stack<long> digitsStack = new Stack<long>(characters.Length);
        int i = 0;
        while (i < characters.Length)
        {
            if (char.IsDigit(characters[i]))
            {
                long n =
                    characters[i] switch
                    {
                        '0' => 0,
                        '1' => 1,
                        '2' => 2,
                        '3' => 3,
                        '4' => 4,
                        '5' => 5,
                        '6' => 6,
                        '7' => 7,
                        '8' => 8,
                        '9' => 9,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                digitsStack.Push(n);
                i++;
            }
            else
            {
                bool HandleSpan(ref ReadOnlySpan<char> span, long number, ref ReadOnlySpan<char> current)
                {
                    if (current.StartsWith(span))
                    {
                        digitsStack.Push(number);
                        i++;
                        return true;
                    }

                    return false;
                }

                ReadOnlySpan<char> current = characters[i..];

                if (HandleSpan(ref one, 1, ref current)) continue;
                if (HandleSpan(ref two, 2, ref current)) continue;
                if (HandleSpan(ref three, 3, ref current)) continue;
                if (HandleSpan(ref four, 4, ref current)) continue;
                if (HandleSpan(ref five, 5, ref current)) continue;
                if (HandleSpan(ref six, 6, ref current)) continue;
                if (HandleSpan(ref seven, 7, ref current)) continue;
                if (HandleSpan(ref eight, 8, ref current)) continue;
                if (HandleSpan(ref nine, 9, ref current)) continue;

                i++;
            }
        }

        return digitsStack.Last() * 10 + digitsStack.First();
    }

    public override ValueTask<string> Solve_2()
    {
        long total =
            File
                .ReadAllLines(InputFilePath)
                .Where(l => l.Length != 0)
                .Select(RetrieveNumber)
                .Sum();

        return new ValueTask<string>($"{total:0}");
    }
}
