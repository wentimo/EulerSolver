using System.Collections.Generic;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem35 : BaseProblem
    {
        protected override int ProblemNumber => 35;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
                The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.
                There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.
                How many circular primes are there below one million?
            */

            Initialize();

            var isPrime = MakeSieve(1000000);
            //List<int> CircularPrimes = new List<int>();
            //for (int i = 0; i < 1000000; i++)
            //{
            //    //bool allPrime = true;
            //    //foreach (int value in Rotations(i))
            //    //{
            //    //    if (!isPrime[value])
            //    //    {
            //    //        allPrime = false;
            //    //        break;
            //    //    }
            //    //}
            //    //if (allPrime) CircularPrimes.Add(i);
            //}

            //for (int i = 0; i < 1000000; i++)
            //{
            //    if (Rotations(i).All(x => isPrime[x])) CircularPrimes.Add(i);
            //}

            // bool setter;
            //  Enumerable.Range(1, 1000000).ToList().ForEach(x => setter = Rotations(x).All(y => isPrime[y]) ? CircularPrimes.Add(x) : false);

            //CircularPrimes.AddRange(Enumerable.Range(1, 1000000).Where(x => Rotations(x).All(y => isPrime[y])));
            //Finalize(CircularPrimes.Count);

            var count = Enumerable.Range(1, 1000000).Count(x => Rotations(x).All(y => isPrime[y]));
            Finalize(count);
        }

        /// <summary>
        /// Lists all Rotations of the number passed according to the problem definition. If "197" is passed it will return "197", "971", "719".
        /// Accomplished by taking the 1rst character in the string and putting it at the end of the string a number of times equal
        /// to the strings length. "1" only returns "1" but "21" returns "21" and "12" for example. This method does create duplicate
        /// values in the list but that is not a problem for our algorithim.
        /// </summary>
        /// <param name="numVar">This method returns all rotations of this parameter</param>
        /// <returns></returns>
        private static IEnumerable<int> Rotations(int numVar)
        {
            var number = numVar.ToString();
            var rotations = new int[number.Length];
            for (var i = 0; i < number.Length; i++)
            {
                rotations[i] = int.Parse(number);
                number = number.Substring(1) + number[0];
            }
            return rotations;
        }
    }
}