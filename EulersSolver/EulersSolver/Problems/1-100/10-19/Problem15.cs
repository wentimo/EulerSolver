using EulersSolver.MyMath;
using System.Numerics;

namespace EulersSolver.Problems
{
    internal class Problem15 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down,
            there are exactly 6 routes to the bottom right corner.

            How many such routes are there through a 20×20 grid?
            */
            // I honestly remember how to solve this type of problem from combinatorics class. Lucky!
            BigInteger answer = CustomMath.bigFactorial(40) / (CustomMath.bigFactorial(20) * CustomMath.bigFactorial(20));
            return answer.ToString();
        }
    }
}