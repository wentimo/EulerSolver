using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace EulersSolver.Extensions
{
    public static class MyExtensions
    {
        public static int Sqrt(this int integer)
        {
            return (int)Math.Ceiling(Math.Sqrt((double)integer));
        }

        public static BigInteger Factorial(this BigInteger integer)
        {
            if (integer <= 1)
                return 1;
            return integer * Factorial(integer - 1);
        }

        public static BigInteger Product(this IEnumerable<BigInteger> enumeration)
        {
            if (enumeration.Any(x => x == 0)) return 0;

            BigInteger product = 1;
            foreach (var number in enumeration)
            {
                product = product * number;
            }
            return product;
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var number in enumeration)
            {
                action?.Invoke(number);
            }
        }

        public static string RemoveSpaces(this string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }
    }
}