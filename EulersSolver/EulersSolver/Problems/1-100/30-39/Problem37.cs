using EulersSolver.MyMath;
using System.Collections.Generic;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem37 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            The number 3797 has an interesting property. Being prime itself, it is possible to continuously remove digits from left to right,
            and remain prime at each stage: 3797, 797, 97, and 7. Similarly we can work from right to left: 3797, 379, 37, and 3.
            Find the sum of the only eleven primes that are both truncatable from left to right and right to left.

            NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.
            */
            bool[] isPrime;
            var factor = 1000000;
            var primes = new List<int>();
            var truncPrimes = new List<int>();

            while (truncPrimes.Count < 11)
            {
                isPrime = CustomMath.MakeSieve(factor);
                primes.Clear();
                primes.AddRange(Enumerable.Range(1, factor).Where(x => isPrime[x]));
                factor *= 10;
                truncPrimes = CountTruncatablePrimes(primes, isPrime);
            }

            return truncPrimes.Sum().ToString();
        }

        private static List<int> CountTruncatablePrimes(IEnumerable<int> primes, IReadOnlyList<bool> isPrime)
        {
            // 3797 : it is possible to continuously remove digits from left to right,
            // and remain prime at each stage: 3797, 797, 97, and 7. Similarly we can work from right to left: 3797, 379, 37, and 3.
            // NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.

            //primes.Where(x => x > 7).ForEach(x => { if (TruncatedVariations(x).All(y => isPrime[y])) TruncatablePrimes.Add(x); });

            return primes.Where(x => x > 7).Where(prime => TruncatedVariations(prime).All(x => isPrime[x])).ToList();
        }

        private static IEnumerable<int> TruncatedVariations(int prime)
        {
            // "1" only variation is "1"                        - Length: 1 -  Count: 1
            // "13" variations: "13", "1", "3"                  - Length: 2 -  Count: 3
            // "129" variations: "129", "12", "1", "29", "9"    - Length: 3 -  Count: 5 // not all are prime but this is for count
            // "3797" variations: see example                   - Length: 4 -  Count: 7
            // This equation looks like 2x-1 variations per length of the prime number

            var number = prime.ToString();
            var truncVars = new List<int>(prime);

            for (var i = 1; i < number.Length; i++)
            {
                truncVars.Add(int.Parse(number.Substring(i)));
                truncVars.Add(int.Parse(number.Substring(0, number.Length - i)));
            }

            return truncVars;
        }
    }
}