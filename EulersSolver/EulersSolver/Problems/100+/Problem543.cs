using System.Collections.Generic;
using System.IO;
using System.Linq;
using EulersSolver.MyMath;
using EulersSolver.Extensions;

namespace EulersSolver.Problems
{
    internal class Problem543 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            Define function P(n,k) = 1 if n can be written as the sum of k prime numbers (with repetitions allowed), and P(n,k) = 0 otherwise.

            For example, P(10,2) = 1 because 10 can be written as either 3 + 7 or 5 + 5, but P(11,2) = 0 because no two primes can sum to 11.

            Let S(n) be the sum of all P(i,k) over 1 ≤ i,k ≤ n.

            For example, S(10) = 20, S(100) = 2402, and S(1000) = 248838.

            Let F(k) be the kth Fibonacci number (with F(0) = 0 and F(1) = 1).

            Find the sum of all S(F(k)) over 3 ≤ k ≤ 44

            */
            var primes = CustomMath.GetListOfPrimes(10000);
            //DebugLogger.AddLine(P(10, 2, primes));
            //var x = S(10, primes); //20
            //DebugLogger.AddLine(x);
            var x = S(100, primes);
            //DebugLogger.AddLine(x);
            //x = S(1000, primes);
            //DebugLogger.AddLine(x);
            return x.ToString();
        }

        int F(int k)
        {
            if (k == 0) return 0;
            if (k == 1) return 1;

            int fibX = 0;
            int fibY = 1;
            int fibZ = 0, holder;

            int count = 1;
            while (count < k)
            {
                holder = fibZ;
                fibZ = fibX + fibY;
                fibX = fibY;
                fibY = fibZ;
                count++;
            }
            return fibZ;
        }

        int S(int n, List<int> Primes)
        {
            //Let S(n)be the sum of all P(i, k) over 1 ≤ i, k ≤ n.
            //For example, S(10) = 20, S(100) = 2402, and S(1000) = 248838.
            int sum = 0;
            Primes = Primes.Where(primeValue => primeValue <= n).ToList();

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    var incr = P(i, j, Primes);
                    if (incr > 0) DebugLogger.AddLine($"P({i,-3},{j,-3}) = {incr}, Sum = {sum + incr} (+{incr})");
                    sum += incr;
                }
            }
            return sum;
        }

        // First thing I did was implement the P method:
        int P(int n, int t, IEnumerable<int> Primes)
        {
            //  Define function P(n, k) = 1 if n can be written as the sum of k prime numbers(with repetitions allowed), and P(n, k) = 0 otherwise.
            //  For example, P(10, 2) = 1 because 10 can be written as either 3 + 7 or 5 + 5, but P(11, 2) = 0 because no two primes can sum to 11.

            // We only want to look at prime numbers lower than the our number n
            // So for example with P(10,2) we'd only want to consider 2,3,5,7 since the next prime is 11 and there's no prime number
            // that 11 can be added with to get 10.

            if (n < (t * 2)) return 0;

            if (t > (n * 2)) return 0;
            //if (t > n) return 0;

            //var result = Primes.Where(primeValue => primeValue <= n).Combinations(t).Select(g => g.Aggregate((left, right) => left + right));
            // dostuff(n, t, Primes.Where(primeValue => primeValue <= n).ToList());

            // return result.Any(x => x == n) ? 1 : 0;

            return DetermineValueIsAttainable(n, t, Primes.ToList()) ? 1 : 0;
        }

        // Attainable reprsents the idea that the value can be created by choosing X items from the list of primes.
        public bool DetermineValueIsAttainable(int value, int numberOfElements, List<int> Primes)
        {
           // var combinedInts = new List<int>();

            var numInts = Primes.Count;
            var loopCounters = new int[numberOfElements]; // make one loop counter for each "nested loop"
            var lastCounter = numberOfElements - 1; // iterate the right-most counter by default

            // maintain current sum in a variable for efficiency, since most of the time
            // it is changing only by the value of one loop counter change.
            var tempSum = Primes[0] * numberOfElements;

            // we are finished when the left/outer-most counter has looped past number of ints
            while (loopCounters[0] < numInts)
            {
                // you can use this to verify the output is iterating correctly:
                // DebugLogger.AddLine(string.Join(",", loopCounters.Select(x => Primes[x])) + ": " + loopCounters.Select(x => Primes[x]).Sum() + "; " + tempSum);

                if (value == tempSum) return true;
               // combinedInts.Add(tempSum);

                tempSum -= Primes[loopCounters[lastCounter]];
                loopCounters[lastCounter]++;
                if (loopCounters[lastCounter] < numInts) tempSum += Primes[loopCounters[lastCounter]];

                // if last element reached in inner-most counter, increment previous counter(s).
                while (lastCounter > 0 && loopCounters[lastCounter] == numInts)
                {
                    lastCounter--;
                    tempSum -= Primes[loopCounters[lastCounter]];
                    loopCounters[lastCounter]++;
                    if (loopCounters[lastCounter] < numInts) tempSum += Primes[loopCounters[lastCounter]];
                }

                // if a previous counter was advanced, reset all future counters to same
                // starting number to start iteration forward again.
                while (lastCounter < numberOfElements - 1)
                {
                    lastCounter++;
                    if (loopCounters[lastCounter] < numInts) tempSum -= Primes[loopCounters[lastCounter]];
                    loopCounters[lastCounter] = loopCounters[lastCounter - 1];
                    if (loopCounters[lastCounter] < numInts) tempSum += Primes[loopCounters[lastCounter]];
                }            
            }

            return false;
        }
    }
}
