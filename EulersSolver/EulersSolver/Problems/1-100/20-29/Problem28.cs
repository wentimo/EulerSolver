using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem28 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            Starting with the number 1 and moving to the right in a clockwise direction a 5 by 5 spiral is formed as follows:

            21 22 23 24 25
            20  7  8  9 10
            19  6  1  2 11
            18  5  4  3 12
            17 16 15 14 13

            It can be verified that the sum of the numbers on the diagonals is 101.

            What is the sum of the numbers on the diagonals in a 1001 by 1001 spiral formed in the same way?
            */

            //var spiralCount = 5; // Test Scenario
            var spiralCount = 1001;
            int count = 1, point = 1, increment = 2;
            DebugLogger.AddLine($"Start at {1}:");
            
            // This was deceptively confusing to figure out.
            for (int i = 1; i < (spiralCount + 1) / 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    point += increment;
                    count += point;
                    DebugLogger.AddLine($"\t{count}(+{point})");
                }
                increment += 2;
            }
            return count.ToString();
        }
    }
}
