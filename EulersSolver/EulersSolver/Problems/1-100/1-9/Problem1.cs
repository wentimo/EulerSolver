using EulersSolver.Utilities;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem1 : BaseProblem
    {
        protected override string Solve()
        {
            /*
             If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
             Find the sum of all the multiples of 3 or 5 below 1000.
            */
            var range = Enumerable.Range(1, 999);
            var filteredRange = range.Where(t => t % 3 == 0 || t % 5 == 0);

            // This line is enabled by the DebugLogger static class. If Solve() is called from the inside
            // DebugLogger.LogAnswer() this line will be added to a Problem1.Debug.txt file. Otherwise it is sent
            // to Console.WriteLine();
            DebugLogger.AddLine("Example Debug Line for Problem 1");

            /*Examples you can uncomment for output.*/

            //// Ex 1. Displays every number in 1-999 in order that are divisible by 3 or 5
            //foreach (int number in filteredRange)
            //{
            //    DebugLogger.AddLine(number);
            //}
            // DebugLogger.AddLine($"Numbers divisible by 3 or 5 : {filteredRange.Count()}");

            //// Ex 2. Displays the count of numbers in 1-999 that are divisible by 3 and 5
            //new int[] {3,5}.ForEach
            //(n =>
            //    DebugLogger.AddLine($"Numbers divisible by {n} : {range.Count(x => x % n == 0)}")
            //);

            // Ex 3. Use this to show the concept of overlapping if you try to add the list of numbers divisible by 3 together
            // with the list of numbers divisible by 5.
            //var nums = new int[3] { 3, 5, 15 };
            //string examples;
            //foreach (int value in nums)
            //{
            //    examples = string.Join(",", range.Where(x => x % value == 0).Take(15));
            //    DebugLogger.AddLine($"Numbers divisible by {value,-6} : {range.Count(x => x % value == 0),-4}\n\t ({examples})\n");
            //}

            //var combinedRange = range.Where(x => x % 3 == 0).ToList();
            //combinedRange.AddRange(range.Where(x => x % 5 == 0));
            //combinedRange.Sort();

            //examples = string.Join(",", combinedRange.Take(20));
            //DebugLogger.AddLine($"Numbers divisible by 3 added with Numbers divisible by 5 : {combinedRange.Count(),-4}\n\t ({examples})\n");

            //examples = string.Join(",", filteredRange.Take(20));
            //DebugLogger.AddLine($"Numbers divisible by 3 or 5 : {filteredRange.Count(), -4}\n\t ({examples})\n");

            return filteredRange.Sum().ToString();
        }
    }
}