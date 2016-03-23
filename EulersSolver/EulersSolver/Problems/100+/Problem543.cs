using EulersSolver.MyMath;
using System;
using System.Collections.Generic;
using System.Linq;
using EulersSolver.Extensions;
using System.Numerics;

namespace EulersSolver.Problems
{
    internal class Problem543 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            Define function P(n,k) = 1 if n can be written as the sum of k prime numbers (with repetitions allowed), and P(n,k) = 0 otherwise.

            For example, P(10,2) = 1 because 10 can be written as either 3 + 7 or 5 + 5, but P(11,2) = 0 because no two primes can sum to 11.

            Let S(n) be the sum of all P(i,k) over 1 ≤ i,k ≤ n.

            For example, S(10) = 20, S(100) = 2402, and S(1000) = 248838.

            Let F(k) be the kth Fibonacci number (with F(0) = 0 and F(1) = 1).

            Find the sum of all S(F(k)) over 3 ≤ k ≤ 44

            */

            //var primes = CustomMath.GetListOfPrimes(10000);
            //DebugLogger.AddLine(P(10, 2, primes));
            //Console.WriteLine("What fibonacci number? (3-44)");
            //int f = int.Parse(Console.ReadLine());
            //var x = F(20);
            //var x = S(10);
            //DebugLogger.AddLine(x);

            // var x = S_old(10);
            var oldhashSet = new List<Tuple<int, int>>();
            var newhashSet = new List<Tuple<int, int>>();
            var diff = new List<Tuple<int, int>>();

            var x = S_old(100, ref oldhashSet);

            DebugLogger.AddLine(x);
            x = S(100, ref newhashSet);
            DebugLogger.AddLine(x);

            if (oldhashSet.Count > newhashSet.Count)
            {
                diff = oldhashSet.Except(newhashSet).ToList();
            }
            else
            {
                diff = oldhashSet.Except(newhashSet).ToList();
            }

            if (diff.Any())
            {
                var message = oldhashSet.Count > newhashSet.Count ? "Underloaded" : "Overloaded";
                DebugLogger.AddLine($"New Hash Set {message}\n");
                DebugLogger.AddLine("Difference:\n");
                diff.ForEach(DebugLogger.AddLine);
            }

            return x.ToString();
        }

        //private BigInteger F(int k)
        //{
        //    if (k == 0) return 0;
        //    if (k == 1) return 1;

        //    int fibX = 0, fibY = 1, fibZ = 0, count = 2;
        //    BigInteger sum = 0;

        //    while (count <= k)
        //    {
        //        fibZ = fibX + fibY;
        //        fibX = fibY;
        //        fibY = fibZ;

        //        if (count >= 3) sum += S(fibZ);
        //        count++;
        //    }

        //    return sum;
        //}

        private BigInteger S(int n, ref List<Tuple<int, int>> TupleList)
        {
            BigInteger sum = 0;
            int count = 0;
            int innerCount = 0;

            for (int i = n; i > 0; i--)
            {
                innerCount = 0;
                for (int j = i / 2; j > 1; j--)
                {
                    if (j == 2 && isPrime(i, i.Sqrt()))
                    {
                      if (isPrime(i - 2, (i - 2).Sqrt()))
                        {
                            innerCount++;
                            sum++;
                        }
                    }
                    else
                    {
                        innerCount++;
                        sum++;
                    }

                    TupleList.Add(new Tuple<int, int>(i, j));
                    count++;
                    //int incr = P(i, j);
                    ////if (incr > 0)
                    ////{
                    ////    DebugLogger.AddLine($"P({i,-3},{j,-3}) = {incr}, Sum = {sum + incr} (+{incr})");
                    ////}
                    //sum += incr;
                    //count++;
                }

                if (isPrime(i, i.Sqrt())) sum++;
            }
            DebugLogger.AddLine($"new: {count}");
            return sum;
        }

        private BigInteger S_old(int n, ref List<Tuple<int, int>> TupleList)
        {
            //Let S(n)be the sum of all P(i, k) over 1 ≤ i, k ≤ n.
            //For example, S(10) = 20, S(100) = 2402, and S(1000) = 248838.
            BigInteger sum = 0;
            int count = 0;

            for (int i = 1; i <= n; i++)
            {
                bool started = false;

                for (int j = 1; j <= n; j++)
                {
                    int incr = P(i, j);
                    if (incr > 0 && j > 4) started = true;
                    //if (incr > 0)
                    //{
                    //    DebugLogger.AddLine($"P({i,-3},{j,-3}) = {incr}, Sum = {sum + incr} (+{incr})");
                    //}
                    //sum += incr;
                    if (incr > 0)
                    {
                        sum++;
                        TupleList.Add(new Tuple<int, int>(i, j));
                    }
                    count++;
                    if (started && incr == 0) break;
                }
            }
            DebugLogger.AddLine($"old: {count}");
            return sum;
        }

        private int P(int n, int t)
        {
            //  Define function P(n, k) = 1 if n can be written as the sum of k prime numbers(with repetitions allowed), and P(n, k) = 0 otherwise.
            //  For example, P(10, 2) = 1 because 10 can be written as either 3 + 7 or 5 + 5, but P(11, 2) = 0 because no two primes can sum to 11.

            //   var line = $"P({n,3},{t,3}) | n/t = {(double)n / (double)t, 16}, t/n = {(double)t / (double)n, 8}";
            if (t / n > 0.5 || t / n < 0.0) return 0;

            if (n % 2 == 0)
            {
                if (t == 1 && n == 2)
                {
                    //DebugLogger.AddLine(line);
                    return 1;
                }
                if (t == 2 && n > 2)
                {
                    //DebugLogger.AddLine(line);
                    return 1;
                }
                if (t > 2 && ((t * 2) <= n || t == 2))
                {
                    //DebugLogger.AddLine(line);
                    return 1;
                }
                return 0;
            }
            else
            {
                if (t == 1 && isPrime(n, n.Sqrt()))
                {
                    //DebugLogger.AddLine(line);
                    return 1;
                }
                if (t == 2)
                {
                    if (n <= 3) return 0;
                    if (isPrime(n - 2, (n - 2).Sqrt()))
                    {
                        //DebugLogger.AddLine(line);
                        return 1;
                    }
                    return 0;
                }
                if (t >= 3 && (t * 2) <= n)
                {
                    //DebugLogger.AddLine(line);
                    return 1;
                }
                return 0;
            }
        }

        public static bool isPrime(int number, int boundary)
        {
            if (number < 2) return false;
            if (number == 2) return true;

            for (int i = 2; i <= boundary; ++i)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}