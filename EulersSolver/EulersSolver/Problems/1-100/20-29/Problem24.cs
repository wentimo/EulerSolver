using EulersSolver.MyMath;
using EulersSolver.Utilities;
using System.Collections.Generic;

namespace EulersSolver.Problems
{
    internal class Problem24 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            A permutation is an ordered arrangement of objects. For example, 3124 is one

            possible permutation of the digits 1, 2, 3 and 4. If all of the permutations

            are listed numerically or alphabetically, we call it lexicographic order. The

            lexicographic permutations of 0, 1 and 2 are:

            012   021   102   120   201   210

            What is the millionth lexicographic

            permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?
            */

            var lexico = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<string> perms = new List<string>();

            foreach (var x in CustomMath.Permutations(lexico))
            {
                perms.Add(string.Join("", x));
            }

            perms.Sort();

            DebugLogger.AddLine(perms[999999]);

            //int answer = 1;
            return perms[999999].ToString();
        }
    }
}