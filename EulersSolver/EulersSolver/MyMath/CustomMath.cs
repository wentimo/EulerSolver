using EulersSolver.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace EulersSolver.MyMath
{
    public class CustomMath
    {
        public static BigInteger bigFactorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * bigFactorial(i - 1);
        }

        public static List<int> GeneratetFibonacciNumbers(int count)
        {
            if (count == 0) return null;
            if (count == 1) return new List<int> { 0 };
            if (count == 2) return new List<int> { 0, 1 };

            int fibX = 1, fibY = 1, fibZ = 0;
            var returnList = new List<int> { 0, 1, 1 };

            while (returnList.Count <= count)
            {
                fibZ = fibX + fibY;
                fibX = fibY;
                fibY = fibZ;
                returnList.Add(fibZ);
            }

            return returnList;
        }

        //https://stackoverflow.com/questions/239865/best-way-to-find-all-factors-of-a-given-number-in-c-sharp
        public static List<int> Factor(int number)
        {
            List<int> factors = new List<int>();
            int max = (int)Math.Sqrt(number);  //round down
            for (int factor = 1; factor <= max; ++factor)
            { //test from 1 to the square root, or the int below it, inclusive.
                if (number % factor == 0)
                {
                    factors.Add(factor);
                    if (factor != number / factor)
                    { // Don't add the square root twice!  Thanks Jon
                        factors.Add(number / factor);
                    }
                }
            }
            return factors;
        }

        // Found this awesome code at http://csharphelper.com/blog/2014/08/use-the-sieve-of-eratosthenes-to-find-prime-numbers-in-c/
        // This creates a List of Booleans where you can check if a value x is prime by simply doing if(is_prime[x]);
        // todo I could probably figure a way to utilize a method to just call IsPrime(x) to iterate over a list/array
        public static bool[] MakeSieve(int max)
        {
            // Make an array indicating whether numbers are prime.
            var isPrime = new bool[max + 1];
            for (var i = 2; i <= max; i++) isPrime[i] = true;

            // Cross out multiples.
            for (var i = 2; i <= max.Sqrt(); i++)
            {
                // See if i is prime.
                if (!isPrime[i]) continue;
                // Knock out multiples of i.
                for (var j = i * 2; j <= max; j += i)
                    isPrime[j] = false;
            }
            return isPrime;
        }

        // Got this from http://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
        // It will return all permutations of an array of any type based to it
        public static IEnumerable<T[]> Permutations<T>(T[] values, int fromInd = 0)
        {
            if (fromInd + 1 == values.Length)
                yield return values;
            else
            {
                foreach (var v in Permutations(values, fromInd + 1))
                    yield return v;

                for (var i = fromInd + 1; i < values.Length; i++)
                {
                    SwapValues(values, fromInd, i);
                    foreach (var v in Permutations(values, fromInd + 1))
                        yield return v;
                    SwapValues(values, fromInd, i);
                }
            }
        }

        // used in Permutations
        private static void SwapValues<T>(IList<T> values, int pos1, int pos2)
        {
            if (pos1 == pos2) return;
            var tmp = values[pos1];
            values[pos1] = values[pos2];
            values[pos2] = tmp;
        }

        /// <summary>
        ///  Returns a list of prime numbers between 1 and max.
        /// </summary>
        /// <param name="max"></param>
        public static List<int> GetListOfPrimes(int max)
        {
            var returnList = new List<int>();
            var isPrime = MakeSieve(max);
            for (int i = 0; i <= max; i++) if (isPrime[i]) returnList.Add(i);
            return returnList;

            //var isPrime = MakeSieve(max);
            //return new List<int>(Enumerable.Range(1, max).Where(x => isPrime[x]));
        }
    }
}

namespace StackOverflow_Math
{
    // All credit goes to David Johnstone for these implementations listed here:
    // https://stackoverflow.com/questions/1042902/most-elegant-way-to-generate-prime-numbers
    public class OutsideMath
    {
        public static List<int> GeneratePrimesNaive(int n)
        {
            List<int> primes = new List<int>();
            primes.Add(2);
            int nextPrime = 3;
            while (primes.Count < n)
            {
                int sqrt = (int)Math.Sqrt(nextPrime);
                bool isPrime = true;
                for (int i = 0; (int)primes[i] <= sqrt; i++)
                {
                    if (nextPrime % primes[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(nextPrime);
                }
                nextPrime += 2;
            }
            return primes;
        }

        public static int ApproximateNthPrime(int nn)
        {
            double n = (double)nn;
            double p;
            if (nn >= 7022)
            {
                p = n * Math.Log(n) + n * (Math.Log(Math.Log(n)) - 0.9385);
            }
            else if (nn >= 6)
            {
                p = n * Math.Log(n) + n * Math.Log(Math.Log(n));
            }
            else if (nn > 0)
            {
                p = new int[] { 2, 3, 5, 7, 11 }[nn - 1];
            }
            else
            {
                p = 0;
            }
            return (int)p;
        }

        // Find all primes up to and including the limit
        public static BitArray SieveOfEratosthenes(int limit)
        {
            BitArray bits = new BitArray(limit + 1, true);
            bits[0] = false;
            bits[1] = false;
            for (int i = 0; i * i <= limit; i++)
            {
                if (bits[i])
                {
                    for (int j = i * i; j <= limit; j += i)
                    {
                        bits[j] = false;
                    }
                }
            }
            return bits;
        }

        public static List<int> GeneratePrimesSieveOfEratosthenes(int n)
        {
            int limit = ApproximateNthPrime(n);
            BitArray bits = SieveOfEratosthenes(limit);
            List<int> primes = new List<int>();
            for (int i = 0, found = 0; i < limit && found < n; i++)
            {
                if (bits[i])
                {
                    primes.Add(i);
                    found++;
                }
            }
            return primes;
        }

        public static BitArray SieveOfSundaram(int limit)
        {
            limit /= 2;
            BitArray bits = new BitArray(limit + 1, true);
            for (int i = 1; 3 * i + 1 < limit; i++)
            {
                for (int j = 1; i + j + 2 * i * j <= limit; j++)
                {
                    bits[i + j + 2 * i * j] = false;
                }
            }
            return bits;
        }

        public static List<int> GeneratePrimesSieveOfSundaram(int n)
        {
            int limit = ApproximateNthPrime(n);
            BitArray bits = SieveOfSundaram(limit);
            List<int> primes = new List<int>();
            primes.Add(2);
            for (int i = 1, found = 1; 2 * i + 1 <= limit && found < n; i++)
            {
                if (bits[i])
                {
                    primes.Add(2 * i + 1);
                    found++;
                }
            }
            return primes;
        }
    }

}
