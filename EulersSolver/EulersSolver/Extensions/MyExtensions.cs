using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace EulersSolver.Extensions
{
    public static class MyExtensions
    {
        /// <summary>
        /// Rob created this method on stackoverflow question: 
        /// stackoverflow.com/questions/36122487/pick-a-varying-number-of-item-combinations-from-a-list/36122841#36122841
        /// Thanks for the help sir!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int? k = null)
        {
            if (!k.HasValue)
                k = elements.Count();

            return k == 0 ? new[] { new T[0] } :
               elements.SelectMany((e, i) => elements.Skip(i).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }

        /// <summary>
        ///  Gets the square root of an integer
        /// </summary>
        /// <param name="integer"></param>
        ///  <example> Sqrt(8) = 3, Sqrt(9) = 3, Sqrt(10) = 4 </example>
        public static int Sqrt(this int integer)
        {
            return (int)Math.Ceiling(Math.Sqrt((double)integer));
        }

        /// <summary>
        /// Returns the product of the IEnumerable<BigInteger> passed
        /// </summary>
        /// <param name="enumeration"></param>
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

        /// <summary>
        /// Allows us to use ForEach over an enumeration
        /// </summary>
        /// <typeparam name="T">type of enumeration</typeparam>
        /// <param name="enumeration">action to perform on each instance</param>
        /// <param name="action">action to perform</param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var number in enumeration)
            {
                action?.Invoke(number);
            }
        }
    }
}