using EulersSolver.Utilities;
using System;
using System.Diagnostics;

namespace EulersSolver.Problems
{
    internal class Problem551 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            Let a0, a1, a2, ... be an integer sequence defined by:

            a0 = 1;
            for n ≥ 1, an is the sum of the digits of all preceding terms.
            The sequence starts with 1, 1, 2, 4, 8, 16, 23, 28, 38, 49, ...
            You are given a_10^6 = 31054319.

            Find a_10^15.
            */

            var sw = new Stopwatch();
            sw.Start();
            var seconds = 5;
            long value = 1, count = 0;
            //while (sw.ElapsedMilliseconds < seconds * 1000)
            DebugLogger.AddLine("n, A(n), difference");
            DebugLogger.AddLine("0, 1,=B5-B4");
            while (count <= 1000)
            {
                count++;
                value += sumOfDigits(value);
                DebugLogger.AddLine($"{count}, {value},=B{count+5}-B{count+4}");
            }
            sw.Stop();

            return $"Numbers calculated in {seconds} seconds : {count}";
        }

        private long sumOfDigits(long n)
        {
            long sum = 0;
            while (n != 0)
            {
                sum += n % 10;
                n /= 10;
            }
            return sum;
        }
    }
}