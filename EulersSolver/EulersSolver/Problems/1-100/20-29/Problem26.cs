﻿using EulersSolver.Utilities;

namespace EulersSolver.Problems
{
    internal class Problem26 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            A unit fraction contains 1 in the numerator. The decimal representation of the unit fractions with denominators 2 to 10 are given:

            1/2	= 	0.5
            1/3	= 	0.(3)
            1/4	= 	0.25
            1/5	= 	0.2
            1/6	= 	0.1(6)
            1/7	= 	0.(142857)
            1/8	= 	0.125
            1/9	= 	0.(1)
            1/10	= 	0.1
            Where 0.1(6) means 0.166666..., and has a 1-digit recurring cycle. It can be seen that 1/7 has a 6-digit recurring cycle.

            Find the value of d < 1000 for which 1/d contains the longest recurring cycle in its decimal fraction part.
            */

            for (double i = 2; i <= 15; i++)
            {
                //var dec = new decimal(1.0 / i);
                //var sdec = dec.ToString();
                //var obj = dec.ToString();
                //string output = $"1/{i,2} = {obj}";
                //string output = $"1/{i, 2} = {sdec}";
                var sdec = (decimal)(1.0 / i);
                string output = $"1/{i,2} = {sdec,32}";
                DebugLogger.AddLine(output);
            }

            return 1.ToString();
        }
    }
}