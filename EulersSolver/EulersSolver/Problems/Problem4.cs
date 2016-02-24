namespace EulersSolver.Problems
{
    internal class Problem4 : BaseProblem
    {
        protected override int ProblemNumber => 4;
        protected override bool HasBeenSolved => true;

        private static bool IsPalindrome(string str)
        {
            var i = 0;
            var j = str.Length - 1;

            while (i < j)
            {
                if (str[i] != str[j])
                    return false;
                i++;
                j--;
            }
            return true;
        }

        protected override void Solve()
        {
            /*
                A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
                Find the largest palindrome made from the product of two 3-digit numbers.
            */

            Initialize();

            //Brute Force
            const int upperlimit = 999 * 999;
            const int lowerlimit = 100 * 100;
            var largest = 0;

            //for (int i = lowerLimit; i < upperLimit; i++)
            //{
            //    if (IsPalindrome(i.ToString()))
            //    {
            //        for (int j = 100; j < 1000; j++)
            //        {
            //            if (i % j == 0 && (i/j).ToString().Length == 3)
            //            {
            //                //Console.WriteLine(j + " * " + (i/j) + " = " + i);
            //                //largest = i > largest ? i : largest;
            //                largest = i;
            //            }
            //        }
            //    }
            //}

            // Find the highest by starting at the highest possible number, going down and finding the first.
            for (int i = upperlimit; i > lowerlimit; i--)
            {
                if (IsPalindrome(i.ToString()))
                {
                    for (int j = 999; j > 100; j--)
                    {
                        if (i % j == 0 && (i / j).ToString().Length == 3)
                        {
                            largest = i;
                            i = 0;
                            j = 0;
                        }
                    }
                }
            }

            Finalize(largest);
        }
    }
}