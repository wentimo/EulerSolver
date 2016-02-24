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
        //TODO

        protected override void Solve()
        {
            /*
               2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
               What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
            */

            Initialize();

            var count = 20;

            while (!Div1through20(count)) count += 20;         

            Finalize(count);
        }     

        bool Div1through20 (BigInteger value)
        {
            for (BigInteger i = 19; i >= 2; i--)
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