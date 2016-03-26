using EulersSolver.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EulersSolver.MyMath;

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

            int x = 100;

            var primes = CustomMath.GetListOfPrimes(x);
            DebugLogger.AddLine(primes.Count);

            var dprimeCount = x/Math.Log(x);
            DebugLogger.AddLine(dprimeCount);

            var y = S_new(100);
            return y.ToString();
        }
        
        private BigInteger F(int k)
        {
            if (k == 0) return 0;
            if (k < 3) return 1;

            int fibX = 0, fibY = 1, fibZ = 0, count = 2;
            BigInteger sum = 0;
            var x = new List<Tuple<int, int>>();

            while (count <= k)
            {
                fibZ = fibX + fibY;
                fibX = fibY;
                fibY = fibZ;

                if (count > 2) sum += S(fibZ, ref x);
                count++;
            }

            return sum;
        }

        private BigInteger S_new(int n)
        {
            BigInteger sum = 0;

            // I think I can change this into an equation somehow.
            for (int i = n; i > 5; i--)
            {
                sum += i / 2 - 2;
            }            
            DebugLogger.AddLine($"j >= 3 logic : 0 (+{sum})");
            var diff = sum;

            sum += n / 2;
            DebugLogger.AddLine($"j == 2 && i % 2 == 0 logic : {sum} (+{sum - diff})");
            diff = sum;

            var primes = CustomMath.GetListOfPrimes(n);
            int primeCount = primes.Count;

            //var dprimeCount = n/Math.Log(n);
            //var primeCount = (int)dprimeCount;

            sum += primeCount - 2;
            DebugLogger.AddLine($"j == 2 && i % 2 == 1 logic : {sum} (+{sum - diff})");
            diff = sum;

            sum += primeCount;
            DebugLogger.AddLine($"j == 1 logic : {sum} (+{sum - diff})");
            return sum;
        }

        private BigInteger S(int n, ref List<Tuple<int, int>> TupleList)
        {
            BigInteger sum = 0;

            for (int i = n; i > 0; i--)
            {
                for (int j = i / 2; j > 1; j--)
                {
                    if (j == 2)
                    {
                        if (i % 2 == 0 || isPrime(i - 2))
                        {
                            sum++;
                            //DebugLogger.AddLine($"P({i,3}, {j,3}) = 1");
                            TupleList.Add(new Tuple<int, int>(i, j));
                        }
                    }
                    else
                    {
                        sum++;
                        //DebugLogger.AddLine($"P({i,3}, {j,3}) = 1");
                        TupleList.Add(new Tuple<int, int>(i, j));
                    }
                }

                if (isPrime(i))
                {
                    sum++;
                    //DebugLogger.AddLine($"P({i, 3}, {1, 3}) = 1");
                    TupleList.Add(new Tuple<int, int>(i, 1));
                }
            }
            return sum;
        }

        private BigInteger S_old(int n, ref List<Tuple<int, int>> TupleList)
        {
            //Let S(n)be the sum of all P(i, k) over 1 ≤ i, k ≤ n.
            //For example, S(10) = 20, S(100) = 2402, and S(1000) = 248838.
            BigInteger sum = 0;

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
                    if (started && incr == 0) break;
                }
            }
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
                if (t == 1 && isPrime(n))
                {
                    //DebugLogger.AddLine(line);
                    return 1;
                }
                if (t == 2)
                {
                    if (n <= 3) return 0;
                    if (isPrime(n - 2))
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

        public static bool isPrime(int number)
        {
            var boundary = number.Sqrt();
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