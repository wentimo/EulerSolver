using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace EulersSolver.Problems
{
    internal class Problem5 : BaseProblem
    {
        protected override int ProblemNumber => 5;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
               2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
               What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
            */

            Initialize();

            var z = 29;

            var isPrime = MakeSieve(z);
            var count = Enumerable.Range(1, z).Where(x => isPrime[x]).Select(x => (BigInteger)x).Product();
            var itr = count;

            while (!Div1throughN(itr, z)) itr += count;         

            Finalize(itr);
        }     

        bool Div1throughN (BigInteger value, int n)
        {
            for (BigInteger i = n; i >= 2; i--)
            {
                if (value % i != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}