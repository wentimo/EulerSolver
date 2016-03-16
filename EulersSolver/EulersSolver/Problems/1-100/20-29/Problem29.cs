﻿using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace EulersSolver.Problems
{
    internal class Problem29 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            Consider all integer combinations of ab for 2 ≤ a ≤ 5 and 2 ≤ b ≤ 5:

            2^2=4, 23=8, 24=16, 25=32
            32=9, 33=27, 34=81, 35=243
            42=16, 43=64, 44=256, 45=1024
            52=25, 53=125, 54=625, 55=3125
            If they are then placed in numerical order, with any repeats removed, we get the following sequence of 15 distinct terms:

            4, 8, 9, 16, 25, 27, 32, 64, 81, 125, 243, 256, 625, 1024, 3125

            How many distinct terms are in the sequence generated by a^b for 2 ≤ a ≤ 100 and 2 ≤ b ≤ 100?
            */

            var terms = Enumerable.Range(2, 99);
            var listOfBigNumbers = new HashSet<BigInteger>();

            foreach (var value in terms)
            {
                foreach (var power in terms)
                {
                    //DebugLog($"{value,-3}^{power,-3} : {BigInteger.Pow(value, power)}");
                    listOfBigNumbers.Add(BigInteger.Pow(value, power));
                }
            }

            return listOfBigNumbers.Count.ToString();
        }
    }
}