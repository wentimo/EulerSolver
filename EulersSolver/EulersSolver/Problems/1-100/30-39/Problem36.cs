using System;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem36 : BaseProblem
    {
        protected override string Solve()
        {
            /*
                The decimal number, 585 = 10010010012 (binary), is palindromic in both bases.

                Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.

                (Please note that the palindromic number, in either base, may not include leading zeros.)
            */

            //List<int> PalindromesInBase2and10 = new List<int>();
            //PalindromesInBase2and10.AddRange(Enumerable.Range(1, 1000000).Where(x => PalindromeInBase2And10(x)));
            //Finalize(PalindromesInBase2and10.Sum());

            //Finalize(Enumerable.Range(1, 1000000).Where(PalindromeInBase2And10).Sum());

            return Enumerable.Range(1, 1000000).Where(x => PalindromeBaseChecker(x, 2, 10)).Sum().ToString();
        }

        private static bool PalindromeBaseChecker(int value, params int[] baseList)
        {
            foreach (int intbase in baseList)
            {
                var numberInBase = Convert.ToString(value, intbase);
                if (numberInBase != Reverse(numberInBase)) return false;
            }

            return true;
        }

        private static bool PalindromeInBase2And10(int value)
        {
            var number = value.ToString();
            var numberInBinary = Convert.ToString(value, 2);
            return number == Reverse(number) &&
                   numberInBinary == Reverse(numberInBinary);
        }

        private static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}