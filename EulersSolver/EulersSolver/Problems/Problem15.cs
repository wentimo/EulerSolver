using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace EulersSolver.Problems
{
    internal class Problem15 : BaseProblem
    {
        protected override int ProblemNumber => 15;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
            Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down,
            there are exactly 6 routes to the bottom right corner.

            How many such routes are there through a 20×20 grid?
            */

            Initialize();
            BigInteger answer = bigFactorial(40) / (bigFactorial(20) * bigFactorial(20));
            Finalize(answer);
        }
    }
}
