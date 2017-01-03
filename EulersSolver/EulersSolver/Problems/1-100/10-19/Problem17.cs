using EulersSolver.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem17 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            If the numbers 1 to 5 are written out in words: one, two, three, four, five, then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.

            If all the numbers from 1 to 1000 (one thousand) inclusive were written out in words, how many letters would be used?

            NOTE: Do not count spaces or hyphens. For example, 342 (three hundred and forty-two) contains 23 letters and 115 (one hundred and fifteen)
            contains 20 letters. The use of "and" when writing out numbers is in compliance with British usage.
            */
            var int_Words = new List<string>();
            for (int i = 1; i <= 1000; i++)
            {
                int_Words.Add(intToWord(i));
            }

            var sstring = string.Join(",", int_Words);

            DebugLogger.AddLine(sstring);
            return sstring.Count(char.IsLetterOrDigit).ToString();
        }

        private string intToWord(int value)
        {
            int expPow = 0;

            var clone = value;

            while (clone > 0)
            {
                expPow++;
                clone /= 10;
            }

            if (expPow == 4)
            {
                var thousand = value / 1000;

                var remainder = value % 1000;

                if (remainder > 0)
                {
                    return $"{intToWord(thousand)} thousand {intToWord(remainder)}";
                }
                else
                {
                    return $"{intToWord(thousand)} thousand";
                }
            }
            else if (expPow == 3)
            {
                var hundred = value / 100;

                var remainder = value % 100;

                if (remainder > 0)
                {
                    return $"{intToWord(hundred)} hundred and {intToWord(remainder)}";
                }
                else
                {
                    return $"{intToWord(hundred)} hundred";
                }
            }
            else if (expPow == 2)
            {
                if (value >= 10 && value <= 19)
                {
                    switch (value)
                    {
                        case 10: return "ten";
                        case 11: return "eleven";
                        case 12: return "twelve";
                        case 13: return "thirteen";
                        case 14: return "fourteen";
                        case 15: return "fifteen";
                        case 16: return "sixteen";
                        case 17: return "seventeen";
                        case 18: return "eighteen";
                        case 19: return "nineteen";
                    }
                }
                else
                {
                    var remainder = value % 10;

                    if (remainder > 0)
                    {
                        return $"{intToWord(value - remainder)}-{intToWord(remainder)}";
                    }
                    else
                    {
                        switch (value / 10)
                        {
                            case 2: return "twenty";
                            case 3: return "thirty";
                            case 4: return "forty";
                            case 5: return "fifty";
                            case 6: return "sixty";
                            case 7: return "seventy";
                            case 8: return "eighty";
                            case 9: return "ninety";
                        }
                    }
                }
            }
            else
            {
                switch (value)
                {
                    case 1: return "one";
                    case 2: return "two";
                    case 3: return "three";
                    case 4: return "four";
                    case 5: return "five";
                    case 6: return "six";
                    case 7: return "seven";
                    case 8: return "eight";
                    case 9: return "nine";
                }
            }

            return "zero";
        }
    }
}