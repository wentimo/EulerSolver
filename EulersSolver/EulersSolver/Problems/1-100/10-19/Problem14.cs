using EulersSolver.Utilities;
using System.Numerics;

namespace EulersSolver.Problems
{
    internal class Problem14 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            The following iterative sequence is defined for the set of positive integers:

            n → n/2 (n is even)
            n → 3n + 1 (n is odd)

            Using the rule above and starting with 13, we generate the following sequence:

            13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1
            It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms.
            Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.

            Which starting number, under one million, produces the longest chain?

            NOTE: Once the chain starts the terms are allowed to go above one million.
            */
            var length = 0;
            var number = 0;
            for (int i = 2; i <= 1000000; i++)
            {
                var len = LengthCollatzSquence(i);
                if (len > length)
                {
                    number = i;
                    length = len;
                }
                DebugLogger.AddLine($"{i} : {len}");
            }

            return number.ToString();
        }

        private int LengthCollatzSquence(int start)
        {
            var value = new BigInteger(start);

            int count = 0;

            while (value != 1)
            {
                if (value % 2 == 0)
                {
                    value /= 2;
                }
                else
                {
                    value = 3 * value + 1;
                }
                count++;
            }

            return count;
        }
    }
}