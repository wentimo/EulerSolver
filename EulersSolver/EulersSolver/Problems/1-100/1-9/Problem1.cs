using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem1 : BaseProblem
    {
        protected override string Solve()
        {
            /*
             If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
             Find the sum of all the multiples of 3 or 5 below 1000.
            */
            return Enumerable.Range(1, 999).Where(t => t % 3 == 0 || t % 5 == 0).Sum().ToString();
        }
    }
}