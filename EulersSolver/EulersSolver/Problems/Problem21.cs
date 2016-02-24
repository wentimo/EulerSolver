using System;
using System.Collections.Generic;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem21 : BaseProblem
    {
        protected override int ProblemNumber => 21;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
               Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).
                If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.

                For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.

                Evaluate the sum of all the amicable numbers under 10000.
            */

            Initialize();

            var divisorsList = DetermineProperDivisors();
            var amicablePairs = new List<AmicablePairHolder>();

            for (var i = 1; i <= divisorsList.Count; i++)
            {
                for (var j = i + 1; j <= divisorsList.Count; j++)
                {
                    if (divisorsList[i] != j || divisorsList[j] != i) continue;
                    amicablePairs.Add(new AmicablePairHolder(i, j));
                    amicablePairs.Add(new AmicablePairHolder(j, i));
                }
            }

            // Get rid of duplicates
            amicablePairs = amicablePairs.Distinct().ToList();
            foreach (var amicableInstance in amicablePairs)
            {
                DebugLog($"D({amicableInstance.Index,4}) = {amicableInstance.Value,4}");
            }
            var answer = amicablePairs.Select(t => t.Value).Sum();
            Finalize(answer);
        }

        private class AmicablePairHolder
        {
            public readonly int Value;
            public readonly int Index;

            public AmicablePairHolder(int number, int counter)
            {
                Value = number;
                Index = counter;
            }
        }

        private static List<int> DetermineProperDivisors()
        {
            // Threw in 0 to offset being 0 indexed so we can use returnList[n] the same as d(n) in the problem description
            var returnList = new List<int>() { 0, 1, 1, 1 };

            // Starting loop at 4 just because the sqrt of 3 or below isn't greater than 1. Makes the iterations within the loop simpler.

            for (var i = 4; i <= 10000; i++)
            {
                var sum = 0;
                var root = Math.Ceiling(Math.Sqrt((double)i));
                for (var j = 1; j <= root; j++)
                {
                    //if (i == 220 || i == 284)
                    //{
                    //    string breakpoint = "";
                    //}

                    if (i % j != 0) continue;
                    if (j != 1)     // don't want to add the number itself
                    {
                        sum += i / j;
                    }

                    if (j != i / j) // don't wanna add squares twice (4 is 2 * 2, so we only want to add 2 a single time, not twice)
                    {
                        sum += j;
                    }
                }
                returnList.Add(sum);
            }
            //Console.WriteLine(String.Format("D(220) = {0}\nD(284) = {1}", returnList[220], returnList[284]));
            return returnList;
        }
    }
}