using EulersSolver.MyMath;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem10 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.

            Find the sum of all the primes below two million.
            */
            const int max = 200000000 - 1;
            return CustomMath.GetListOfPrimes(max).Sum().ToString();
        }
    }
}