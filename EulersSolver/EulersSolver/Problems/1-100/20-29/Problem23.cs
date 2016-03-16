using System;
using System.Collections.Generic;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem23 : BaseProblem
    {
        protected override string Solve()

        {
            /*
                A perfect number is a number for which the sum of its proper divisors is exactly equal to the number. For example, the sum of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28,
                which means that 28 is a perfect number.

                A number n is called deficient if the sum of its proper divisors is less than n and it is called abundant if this sum exceeds n.

                As 12 is the smallest abundant number, 1 + 2 + 3 + 4 + 6 = 16, the smallest number that can be written as the sum of two abundant numbers is 24.
                By mathematical analysis, it can be shown that all integers greater than 28123 can be written as the sum of two abundant numbers. However, this upper limit cannot be reduced any further
                by analysis even though it is known that the greatest number that cannot be expressed as the sum of two abundant numbers is less than this limit.

                Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers.
            */

            var total = Enumerable.Range(1, 28123).Sum();

            var abundantNumbers = GetAbundantNumbers();

            var combinations = GetCombinations(abundantNumbers);
            //var combinations = abundantNumbers.SelectMany(x => abundantNumbers, (x, y) => x + y).Where(x => x < 28124).Distinct().ToList();

            //var combinations = (from x in abundantNumbers
            //                    from y in abundantNumbers
            //                    where x + y < 28124
            //                    select x + y).Distinct().ToList();

            int answer = total - combinations.Sum();

            return answer.ToString();
        }

        private static HashSet<int> GetCombinations(List<int> abundantNumbers)
        {
            var combinations = new HashSet<int>();

            for (int i = 0; i < abundantNumbers.Count; i++)
            {
                for (int j = i; i < abundantNumbers.Count && abundantNumbers[i] + abundantNumbers[j] < 28124; j++)
                {
                    combinations.Add(abundantNumbers[i] + abundantNumbers[j]);
                }
            }

            return combinations;
        }

        private static List<int> GetAbundantNumbers()
        {
            int lowBound = 12, highBound = 28124;
            var range = Enumerable.Range(lowBound, highBound);
            return range.Where(x => getFactors(x).Sum() > x).ToList();
        }

        private static List<int> GetAbundantNumbersNew()
        {
            int lowBound = 12, highBound = 28124;
            var range = Enumerable.Range(lowBound, highBound);
            return range.Where(x => getFactorsNew(x).Sum() > x).ToList();
        }

        public static List<int> getFactors(int x)
        {
            var factors = new List<int>(1);

            var limit = (x + 1) / 2;

            foreach (int factor in Enumerable.Range(1, limit).Where(factor => x % factor == 0)) factors.Add(factor);

            return factors.ToList();
        }

        public static List<int> getFactorsNew(int n)
        {
            var factors = new List<int>();
            factors.Add(1);

            var rootn = (int)Math.Sqrt(n);

            for (int i = 2; i <= rootn; i++)
            {
                if (n % i == 0)
                {
                    factors.Add((int)i);

                    if (n != (n / i))
                    {
                        factors.Add((int)(n / i));
                    }
                }
            }

            return factors;
        }
    }
}