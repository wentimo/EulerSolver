using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace EulersSolver.Problems
{
    internal class Problem16 : BaseProblem
    {
        protected override int ProblemNumber => 16;
        protected override bool HasBeenSolved => true;

        int SumOfDigits(BigInteger value) => value.ToString().ToCharArray().Select(x => x - '0').Sum();

        protected override void Solve()
        {
            /*
            2^15 = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.

            What is the sum of the digits of the number 2^1000?
            */

            Initialize();
            int answer = SumOfDigits(BigInteger.Pow(2, 1000));
            Finalize(answer);
        }

    }
}
