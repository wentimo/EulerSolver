using EulersSolver.Extensions;
using System;
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
            var isPrime = MakeSieve(max);
            return new List<int>(Enumerable.Range(1, max).Where(x => isPrime[x]));
        }
    }
}