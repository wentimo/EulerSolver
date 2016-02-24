using System;
using System.Globalization;

namespace EulersSolver.Problems
{
    internal class Problem26 : BaseProblem
    {
        protected override int ProblemNumber => 26;

        protected override bool HasBeenSolved => false;
        // TODO

        protected override void Solve()
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

            Initialize();

            int highest = 0;
            for (decimal i = 2; i < 11; i++)
            {
                decimal value = 1 / i;
                string valueString;
                int lengthOfCycle = CalculateCycleLength(value, out valueString);
                highest = highest > lengthOfCycle ? highest : lengthOfCycle;
            }

            Finalize(highest);
        }

        public static int CalculateCycleLength(decimal value, out string valueString)
        {
            var valueToCheck = value.ToString(CultureInfo.CurrentCulture);
            valueToCheck = valueToCheck.Substring(2, valueToCheck.Length - 2);
            var testLength = 1;
            var length = valueToCheck.Length;

            if (length == 28)
            {
                while (testLength < length)
                {
                    var iterationOne = valueToCheck.Substring(0, testLength) ==
                                       valueToCheck.Substring(testLength, testLength);
                    var iterationTwo = true;

                    if (testLength * 3 < 28)
                    {
                        iterationTwo = valueToCheck.Substring(testLength * 2, testLength) ==
                                       valueToCheck.Substring(testLength * 3, testLength);
                    }
                    if (iterationOne && iterationTwo)
                    {
                        break;
                    }

                    testLength++;
                }

                valueString = String.Format("0.({0})", valueToCheck.Substring(0, testLength));
            }
            else
            {
                valueString = String.Format("0.{0}", valueToCheck);
                testLength = length;
            }

            return testLength;
        }
    }
}