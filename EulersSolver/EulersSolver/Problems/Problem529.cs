using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using ExtensionMethods;
using System;

namespace EulersSolver.Problems
{
    internal class Problem529 : BaseProblem
    {
        protected override int ProblemNumber => 529;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
            A 10-substring of a number is a substring of its digits that sum to 10. 
            For example, the 10-substrings of the number 3523014 are:

            '352'3014
            3'523'014
            3'5230'14
            35'23014'

            A number is called 10-substring-friendly if every one of its digits belongs to a 10-substring. 
            For example, 3523014 is 10-substring-friendly, but 28546 is not.

            Let T(n) be the number of 10-substring-friendly numbers from 1 to 10^n (inclusive).
            For example T(2) = 9 and T(5) = 3492.

            Find T(1018) mod 1 000 000 007.
            */

            Initialize();
            //Enumerable.Range(1, 10).ForEach(x => DebugLog($"T({x}) = {T(x)}"));

            //var x = isNumber10Friendly(1819);
            //DebugLog($"T(2) = {T(2)}");
            //DebugLog($"T(3) = {T(3)}");
            DebugLog($"T(4) = {T(4)}");
            DebugLog($"T(5) = {T(5)}");
            DebugLog($"T(6) = {T(6)}");
           // DebugLog($"T(7) = {T(7)}");
            //DebugLog($"T(6) = {T(6)}");
            //var answer = T(5);
            //var x = isNumber10Friendly(3523014);
            Finalize(1);

        }

        static int T(int pow)
        {
            if (pow < 2) return 0;

            var count = 0;
            for (int i = 19; i <= BigInteger.Pow(10, pow); i++)
            {
                if (isNumber10Friendly(i)) count++;
            }

            return count;
        }

        static bool isNumber10Friendly(int num)
        {
            var number = digitArr(num);

            var increment = 0;
            for (int i = 0; i < number.Length; i += increment > 0 ? increment : 1)
            {
                if (!is10Friendly(number, i, 0, 0, ref increment))
                {
                    // DebugLog($" {num} 10Friendly? False");
                   // DebugLog($"not 10 friendly on digit {number[i]} of 3523014");
                    return false;
                }
            }

            DebugLog($"{num}");
            return true;
        }

        static bool is10Friendly (int[] number, int startIndex, int left, int right, ref int increment)
        {
            // SLR stands for START LEFT RIGHT
            // SLR (1, 0, 0) Means START = 1, LEFT = 0, RIGHT = 0, start is 0-indexed
            // var output = "";
            // number.Skip(startIndex - left).Take(right + left + 1).ForEach(x => output += x);

            //DebugLog($"Called Is10Friendly({startIndex},{left},{right}) : {output}");

            //var list = new List<int>();

            //3523014
            // SLR (5,3,0) 35"2301"4
            // SLR (5,3,1) 35"23014"

            // list.AddRange(number.Skip(startIndex - left).Take(left + right + 1));

            // var sum = list.Sum();

            increment = right;

            var sum = number.Skip(startIndex - left).Take(left + right + 1).Sum();

            if (sum < 10)
            {
                if (startIndex - left > 0 || startIndex + right == number.Length)
                {
                    var FriendlyTotheLeft = is10Friendly(number, startIndex, left + 1, right, ref increment);

                    if (!FriendlyTotheLeft && (startIndex + right + 1 ) < number.Length)
                    {
                        return is10Friendly(number, startIndex, left, right + 1, ref increment);
                    }

                    return FriendlyTotheLeft;
                }
                else if (startIndex + right < number.Length && (left + right + 1) < number.Length)
                {
                    var FriendlyTotheRight = is10Friendly(number, startIndex, left, right + 1, ref increment);

                    if (!FriendlyTotheRight && startIndex - left > 0)
                    {
                        return is10Friendly(number, startIndex, left + 1, right, ref increment);
                    }

                    return is10Friendly(number, startIndex, left, right + 1, ref increment);
                }
                else
                {
                    return false;
                }          
            }
            else return sum == 10 ? true : false;
        }

        public static int[] digitArr(int n)
        {
            if (n == 0) return new int[1] { 0 };

            var digits = new List<int>();

            for (; n != 0; n /= 10)
                digits.Add(n % 10);

            var arr = digits.ToArray();
            Array.Reverse(arr);
            return arr;
        }
    }
}
