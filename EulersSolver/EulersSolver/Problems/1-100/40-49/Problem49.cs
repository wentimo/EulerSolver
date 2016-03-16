using EulersSolver.MyMath;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem49 : BaseProblem
    {
        protected override string Solve()
        {
            /*
             The arithmetic sequence, 1487, 4817, 8147, in which each of the terms increases by 3330, is unusual in two ways:
             (i) each of the three terms are prime, and,
             (ii) each of the 4-digit numbers are permutations of one another.

            There are no arithmetic sequences made up of three 1-, 2-, or 3-digit primes, exhibiting this property, but there is one other 4-digit increasing sequence.

            What 12-digit number do you form by concatenating the three terms in this sequence?
            */
            var primes = CustomMath.GetListOfPrimes(10000);
            primes.RemoveAll(x => x < 1000);

            var counts = primes.Where(x => primes.Any(y => x + 3330 == y) &&
                                           primes.Any(y => x + 6660 == y)).ToList();

            var numsToCheck = new List<int>(counts);
            numsToCheck.AddRange(counts.Select(x => x + 3330));
            numsToCheck.AddRange(counts.Select(x => x + 6660));

            numsToCheck.Sort();

            var obj = numsToCheck.Select(x => new { Value = x, Sorted = IntSortValue(x) }).OrderBy(x => x.Sorted).ToList();

            foreach (var sortValue in obj.Select(x => x.Sorted).Distinct())
            {
                var count = obj.Count(x => x.Sorted == sortValue);
                if (count <= 1) continue;
                {
                    var matchValues = obj.Where(x => x.Sorted == sortValue).Select(x => x.Value).ToList();
                    var stringValues = matchValues.Select(x => x.ToString()).ToList();
                    string output = $"{sortValue,4} - {count} = [{String.Join(",", stringValues)}]\n";
                    EulersLogger.DebugLog(output);
                }
            }

            return 1.ToString();
        }

        private static string IntSortValue(int value)
        {
            var x = value.ToString();
            var sorter = x.ToCharArray();
            Array.Sort(sorter);
            return new string(sorter);
        }
    }
}