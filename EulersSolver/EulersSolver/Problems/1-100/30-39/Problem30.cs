using EulersSolver.Extensions;
using EulersSolver.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem30 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:

            1634 = 14 + 64 + 34 + 44
            8208 = 84 + 24 + 04 + 84
            9474 = 94 + 44 + 74 + 44
            As 1 = 14 is not a sum it is not included.

            The sum of these numbers is 1634 + 8208 + 9474 = 19316.

            Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.
            */
            var factor = 5;
            var digitsRaised = new int[10];
            Enumerable.Range(0, 10).ForEach(n => digitsRaised[n] = (int)Math.Pow(n, factor));

            var listOfNumbers = new List<int>();

            for (int i = 10; i <= 1000000; i++)
            {
                var sum = 0;
                var value = i;
                while (value != 0)
                {
                    int remainder;
                    value = Math.DivRem(value, 10, out remainder);
                    sum += digitsRaised[remainder];
                }

                if (i == sum) listOfNumbers.Add(i);
            }

            listOfNumbers.ForEach(DebugLogger.AddLine);

            return listOfNumbers.Sum().ToString();
        }

        public static int[] digitArr(int n)
        {
            if (n == 0) return new int[1] { 0 };

            var digits = new List<int>();

            for (; n != 0; n /= 10)
                digits.Add(n % 10);

            var arr = digits.ToArray();
            Array.Reverse(arr);
            return arr;
        }
    }
}