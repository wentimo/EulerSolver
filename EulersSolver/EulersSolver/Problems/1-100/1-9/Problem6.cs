using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem6 : BaseProblem
    {
        protected override string Solve()
        {
            /*
               The sum of the squares of the first ten natural numbers is,

                1^2 + 2^2 + ... + 10^2 = 385
                The square of the sum of the first ten natural numbers is,

                (1 + 2 + ... + 10)^2 = 552 = 3025
                Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is 3025 − 385 = 2640.

                Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
            */

            var listOfNaturals = Enumerable.Range(1, 100).ToList();

            var squareOfSum = listOfNaturals.Sum() * listOfNaturals.Sum();

            var listOfSquares = listOfNaturals.Select(t => t * t).ToList();

            var sumOfSquares = listOfSquares.Sum();

            var sum = squareOfSum - sumOfSquares;

            return sum.ToString();
        }
    }
}