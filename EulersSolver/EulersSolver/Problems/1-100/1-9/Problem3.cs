﻿using EulersSolver.MyMath;
using EulersSolver.Utilities;
using System;
using System.Collections.Generic;

namespace EulersSolver.Problems
{
    internal class Problem3 : BaseProblem
    {
        protected override string Solve()
        {
            /*
               The prime factors of 13195 are 5, 7, 13 and 29.
               What is the largest prime factor of the number 600851475143 ?
            */
            var answer = 0;
            const long value = 600851475143;
            var root = Math.Sqrt((double)value);
            var upperBound = Math.Ceiling(Math.Sqrt(value));
            var checkValuePrime = CustomMath.MakeSieve((int)root);

            var divisors = new HashSet<int> { 0x1 };

            for (var i = 2; i < root; i++)
            {
                if (value % i != 0) continue;
                divisors.Add(i);
                divisors.Add((int)(value / i));
                DebugLogger.AddLine(string.Format("{0,10} * {1,10} = {2,10} | {0,10} is{3}a prime", i, value / i, value, checkValuePrime[i] ? " " : " not "));

                if (checkValuePrime[i] && i > answer)
                {
                    answer = i;
                }
            }

            return answer.ToString();
        }
    }
}