using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem10 : BaseProblem
    {
        protected override int ProblemNumber => 10;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
            The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.

            Find the sum of all the primes below two million.
            */

            Initialize();
            const int max = 200000000 - 1;
            var Primes = GetListOfPrimes(max);
            Finalize(Primes.Sum());
        }
    }
}