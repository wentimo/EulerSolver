using EulersSolver.MyMath;
using System.Collections.Generic;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem50 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            The prime 41, can be written as the sum of six consecutive primes:

            41 = 2 + 3 + 5 + 7 + 11 + 13
            This is the longest sum of consecutive primes that adds to a prime below one-hundred.

            The longest sum of consecutive primes below one-thousand that adds to a prime, contains 21 terms, and is equal to 953.

            Which prime, below one-million, can be written as the sum of the most consecutive primes?
            */

            var isPrime = CustomMath.MakeSieve(1100000);
            var primes = new List<int>(Enumerable.Range(1, 1100000).Where(x => isPrime[x]));

            int skip = 0, take = 1, sum = 0, highestCount = 0, value = 0;
            var next = true;
            while (next)
            {
                while (sum < 1000000)
                {
                    sum = primes.Skip(skip).Take(take).Sum();
                    if (take > highestCount && sum < 1000000 && isPrime[sum])
                    {
                        highestCount = take;
                        value = sum;
                    }
                    take++;
                }
                skip++;
                next = primes.Skip(skip).First() < 1000000 && take > highestCount;
                take = 1;
                sum = 0;
            }

            return value.ToString();
        }
    }
}