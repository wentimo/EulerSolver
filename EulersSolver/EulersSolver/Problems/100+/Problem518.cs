using EulersSolver.MyMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace EulersSolver.Problems
{
    internal class Problem518 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            Let S(n) = a+b+c over all triples (a,b,c) such that:

            a, b, and c are prime numbers.
            a < b < c < n.
            a+1, b+1, and c+1 form a geometric sequence.
            For example, S(100) = 1035 with the following triples:

            (2, 5, 11), (2, 11, 47), (5, 11, 23), (5, 17, 53), (7, 11, 17), (7, 23, 71), (11, 23, 47), (17, 23, 31), (17, 41, 97), (31, 47, 71), (71, 83, 97)

            Find S(10^8).
            */

            BigInteger answer = S(1000);

            //DebugLog_TimerReset(answer);
            //answer = S2(1000);
            //DebugLog_TimerReset(answer, false);
            return answer.ToString();
        }

        //BigInteger S2(int num)
        //{
        //    var primes = GetListOfPrimes(num).Select(prime => (double)prime).ToList();
        //    var ratioedTriplets =
        //            from pt1 in primes
        //            from pt2 in primes
        //            from pt3 in primes
        //            where (pt1 + 1) / (pt2 + 1) == (pt2 + 1) / (pt3 + 1)
        //            where pt1 < pt2 && pt2 < pt3
        //            select Tuple.Create(pt1, pt2, pt3);

        //    return (int)ratioedTriplets.Sum(x => x.Item1 + x.Item2 + x.Item3);
        //}

        private BigInteger S(int num)
        {
            var primes = CustomMath.GetListOfPrimes(num).Select(prime => (double)prime).ToList();

            var ratioedTriplets = new List<Tuple<double, double, double>>();

            var numbers = primes.Select(x => x + 1).ToList();

            for (int index1 = 0; index1 < numbers.Count() - 2; index1++)
            {
                for (int index2 = index1 + 1; index2 < numbers.Count() - 1; index2++)
                {
                    var formerRatio = numbers[index1] / numbers[index2];

                    for (int index3 = index2 + 1; index3 < numbers.Count(); index3++)
                    {
                        var latterRatio = numbers[index2] / numbers[index3];

                        if (formerRatio == latterRatio)
                        {
                            ratioedTriplets.Add(new Tuple<double, double, double>(primes[index1], primes[index2], primes[index3]));
                        }
                        else if (latterRatio < formerRatio)
                        {
                            index3 = numbers.Count;
                        }
                    }
                }
            }
            return (int)ratioedTriplets.Sum(x => x.Item1 + x.Item2 + x.Item3);
        }
    }
}