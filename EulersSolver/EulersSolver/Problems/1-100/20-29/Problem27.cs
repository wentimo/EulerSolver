using EulersSolver.MyMath;
using System;

namespace EulersSolver.Problems
{
    internal class Problem27 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            Euler discovered the remarkable quadratic formula:

            n² + n + 41

            It turns out that the formula will produce 40 primes for the consecutive values n = 0 to 39. However, when n = 40, 402 + 40 + 41 = 40(40 + 1) + 41 is divisible by 41,
             * and certainly when n = 41, 41² + 41 + 41 is clearly divisible by 41.

            The incredible formula  n² − 79n + 1601 was discovered, which produces 80 primes for the consecutive values n = 0 to 79. The product of the coefficients, −79 and 1601, is −126479.

            Considering quadratics of the form:

            n² + an + b, where |a| < 1000 and |b| < 1000

            where |n| is the modulus/absolute value of n
            e.g. |11| = 11 and |−4| = 4
            Find the product of the coefficients, a and b, for the quadratic expression that produces the maximum number of primes for consecutive values of n, starting with n = 0.
            */
            const int max = 1000 * 1000 + 1000 * 1000 + 1000;

            var isPrime = CustomMath.MakeSieve(max);
            var highestNumPrimes = 0;
            var answer = 0;

            for (int a = -1000; a <= 1000; a++)
            {
                for (int b = -1000; b <= 1000; b++)
                {
                    var numPrimes = numberOfConsecutivePrimesFromPolynomial(a, b, isPrime);
                    if (numPrimes > highestNumPrimes)
                    {
                        //var polyNomial = "n^2" + (a > 0 ? " +" : " ") + $"{a, 5}n"
                        //                       + (b > 0 ? " +" : " -") + $"{Math.Abs(b), 5}";
                        highestNumPrimes = numPrimes;
                        // DebugLog(polyNomial + "produces " + highestNumPrimes + " primes");
                        answer = a * b;
                    }
                }
            }

            return answer.ToString();
        }

        // n² + an + b, where |a| < 1000 and |b| < 1000
        private static int numberOfConsecutivePrimesFromPolynomial(int a, int b, bool[] isPrime)
        {
            var numPrimes = 0;
            while (isPrime[Math.Abs(numPrimes * numPrimes + a * numPrimes + b)])
            {
                numPrimes++;
            }
            return numPrimes;
        }
    }
}