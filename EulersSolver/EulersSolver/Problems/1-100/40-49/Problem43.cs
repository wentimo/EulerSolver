using EulersSolver.MyMath;
using System.Collections.Generic;

namespace EulersSolver.Problems
{
    internal class Problem43 : BaseProblem
    {
        /*
        The number, 1406357289, is a 0 to 9 pandigital number because it is made up of each of the digits 0 to 9 in some order, but it also has a rather interesting sub-string divisibility property.

        Let d1 be the 1st digit, d2 be the 2nd digit, and so on. In this way, we note the following:

        d2d3d4=406 is divisible by 2
        d3d4d5=063 is divisible by 3
        d4d5d6=635 is divisible by 5
        d5d6d7=357 is divisible by 7
        d6d7d8=572 is divisible by 11
        d7d8d9=728 is divisible by 13
        d8d9d10=289 is divisible by 17
        Find the sum of all 0 to 9 pandigital numbers with this property.
        */

        protected override string Solve()
        {
            /*
            var number = "1406357289";
            if (CheckDivisibility(number))
            {
                return "True";
            }
            */

            var lexicon = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var perms = new List<string>();

            foreach (var digitalArray in CustomMath.Permutations(lexicon))
            {
                if (digitalArray[0] != 0) // Starting with 0 is not valid for a pandigital number
                {
                    perms.Add(string.Join("", digitalArray));
                }
            }

            var sum = 0.0;
            foreach (var pandigital in perms)
            {
                var p = double.Parse(string.Join(string.Empty, pandigital));
                if (CheckDivisibility(p))
                {
                    sum += p;
                }
            }

            return sum.ToString();
        }

        protected bool CheckDivisibility(double number)
        {
            var checks = new Dictionary<int, int>
            {
                { 2, 2 },
                { 3, 3 },
                { 4, 5 },
                { 5, 7 },
                { 6, 11 },
                { 7, 13 },
                { 8, 17 },
            };

            var divisible = true;
            foreach (var kvp in checks)
            {
                var startingIndex = kvp.Key;
                var divisibleBy = kvp.Value;

                divisible &= ValueIsDivisibleByIndex(number.ToString(), startingIndex, divisibleBy);
            }

            return divisible;
        }

        protected bool ValueIsDivisibleByIndex(string number, int startingDigit, int divisibleBy, int length = 3)
        {
            var range = number.Substring(startingDigit - 1, length).ToString();
            var num = int.Parse(range);

            return num % divisibleBy == 0;
        }
    }
}
