using EulersSolver.MyMath;
using EulersSolver.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem32 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once; for example, the 5-digit number, 15234, is 1 through 5 pandigital.

            The product 7254 is unusual, as the identity, 39 × 186 = 7254, containing multiplicand, multiplier, and product is 1 through 9 pandigital.

            Find the sum of all products whose multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital.

            HINT: Some products can be obtained in more than one way so be sure to only include it once in your sum.
            */
            var numbers = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            // Using a hash set instead of a List because there are going to be duplicates and hash doesn't allow duplicates
            var hash = new HashSet<int>();

            foreach (var v in CustomMath.Permutations(numbers))
            {
                string logMessage;
                int product;
                if (!ConfirmUnusuality(v, out logMessage, out product)) continue;
                DebugLogger.AddLine(logMessage);
                hash.Add(product);
            }

            var answer = hash.Sum();
            return answer.ToString();
        }

        private static bool ConfirmUnusuality(char[] permutation, out string logMessage, out int product)
        {
            // 1x4=4 || 4x1=4 || 2x3=4 || 3x2=4  are the only logical possibilities (so if you have 9 characters abcdefghi, 1x4=4 is saying a x bcde = fghi)

            // 1x4=4 check

            var holder = permutation[0].ToString();
            var v1 = Convert.ToInt32(holder);

            holder = $"{permutation[1]}{permutation[2]}{permutation[3]}{permutation[4]}";
            var v2 = Convert.ToInt32(holder);

            holder = $"{permutation[5]}{permutation[6]}{permutation[7]}{permutation[8]}";
            var v3 = Convert.ToInt32(holder);
            product = v3;

            if (v1 * v2 == v3)
            {
                logMessage = $"{v1}*{v2}={v3}";
                return true;
            }

            // 4x1=4 check
            holder = $"{permutation[0]}{permutation[1]}{permutation[2]}{permutation[3]}";
            v1 = Convert.ToInt32(holder);

            holder = permutation[4].ToString();
            v2 = Convert.ToInt32(holder);

            if (v1 * v2 == v3)
            {
                logMessage = $"{v1}*{v2}={v3}";
                return true;
            }

            // 2x3=4 check
            holder = $"{permutation[0]}{permutation[1]}";
            v1 = Convert.ToInt32(holder);

            holder = $"{permutation[2]}{permutation[3]}{permutation[4]}";
            v2 = Convert.ToInt32(holder);

            if (v1 * v2 == v3)
            {
                logMessage = $"{v1}*{v2}={v3}";
                return true;
            }

            // 3x2=4 check
            holder = $"{permutation[0]}{permutation[1]}{permutation[2]}";
            v1 = Convert.ToInt32(holder);

            holder = $"{permutation[3]}{permutation[4]}";
            v2 = Convert.ToInt32(holder);

            if (v1 * v2 == v3)
            {
                logMessage = $"{v1}*{v2}={v3}";
                return true;
            }
            else
            {
                logMessage = "";
                return false;
            }
        }
    }
}