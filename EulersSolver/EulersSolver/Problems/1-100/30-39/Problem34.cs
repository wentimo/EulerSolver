using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace EulersSolver.Problems
{
    internal class Problem34 : BaseProblem
    {
        protected override string Solve()
        {
            /*
                145 is a curious number, as 1! + 4! + 5! = 1 + 24 + 120 = 145.

                Find the sum of all numbers which are equal to the sum of the factorial of their digits.

                Note: as 1! = 1 and 2! = 2 are not sums they are not included.
            */

            var curiousNumbers = new List<BigInteger>();

            var sw = new Stopwatch();
            sw.Start();

            BigInteger i = 11;
            while (curiousNumbers.Count < 2)
            {
                var sumOfFactorials = 0;
                var ListofChars = i.ToString().ToCharArray();
                foreach (char digit in ListofChars)
                {
                    switch (digit)
                    {
                        case '0': sumOfFactorials += 1; break;
                        case '1': sumOfFactorials += 1; break;
                        case '2': sumOfFactorials += 2; break;
                        case '3': sumOfFactorials += 6; break;
                        case '4': sumOfFactorials += 24; break;
                        case '5': sumOfFactorials += 120; break;
                        case '6': sumOfFactorials += 720; break;
                        case '7': sumOfFactorials += 5040; break;
                        case '8': sumOfFactorials += 40320; break;
                        case '9': sumOfFactorials += 362880; break;
                    }
                }

                if (i == sumOfFactorials) curiousNumbers.Add(i);
                i++;
            }

            DebugLogger.AddLine(i);

            var sum = new BigInteger();
            curiousNumbers.ForEach(x => sum += x);
            curiousNumbers.ForEach(DebugLogger.AddLine);

            return sum.ToString();
        }
    }
}