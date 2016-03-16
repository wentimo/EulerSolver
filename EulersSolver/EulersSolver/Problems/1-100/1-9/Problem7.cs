using EulersSolver.MyMath;

namespace EulersSolver.Problems
{
    internal class Problem7 : BaseProblem
    {
        protected override string Solve()
        {
            /*
               By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.

               What is the 10 001st prime number?
            */

            var isPrime = CustomMath.MakeSieve(1000000);

            int count = 0, iterator = 1;

            while (count < 10001 && iterator < 1000000)
            {
                iterator++;
                if (isPrime[iterator])
                {
                    count++;
                }
            }

            return iterator.ToString();
        }
    }
}