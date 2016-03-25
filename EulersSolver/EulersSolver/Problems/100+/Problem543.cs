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
            //var x = F(44);
            test();
            //DebugLogger.AddLine(x);


            // var x = S_old(10);

            return "1".ToString();
        }
        
        void test ()
        {
            var oldhashSet = new List<Tuple<int, int>>();
            var newhashSet = new List<Tuple<int, int>>();
            var diff = new List<Tuple<int, int>>();

            var numCheck = 100;
            //var x = S_Ben_2(numCheck, ref oldhashSet);
            //var x = S_Ben(numCheck);
            var x = S_new(numCheck, ref newhashSet);

            //DebugLogger.AddLine($"Old Hash Set : {oldhashSet.Count()}");
            //oldhashSet.ForEach(DebugLogger.AddLine);

            DebugLogger.AddLine($"New Hash Set : {newhashSet.Count()}");
            newhashSet.ForEach(DebugLogger.AddLine);


            //if (oldhashSet.Count > newhashSet.Count)
            //{
            //    diff = oldhashSet.Except(newhashSet).ToList();
            //}
            //else
            //{
            //    diff = newhashSet.Except(oldhashSet).ToList();
            //}

            //if (diff.Any())
            //{
            //    DebugLogger.AddLine($"Old Hash Set : {oldhashSet.Count()}");
            //    DebugLogger.AddLine($"New Hash Set : {newhashSet.Count()}");
            //    DebugLogger.AddLine("Differences:\n");
            //    diff.ForEach(DebugLogger.AddLine);
            //}
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

        //private BigInteger S(int n)

        private BigInteger S_Ben_2(int n, ref List<Tuple<int, int>> TupleList)
        {
            BigInteger sum = 0;

            //for (int i = 3; i <= n; i++)
            //{
            //    int halfI = i / 2;

            //    int ThisTurn = ((halfI) * ((halfI) + 1) / 2);
            //    if (ThisTurn > 0) {
            //        sum += ThisTurn;
            //        DebugLogger.AddLine($"InProgress... i = {i}, halfI = {halfI}, sum = {sum} (+ {ThisTurn})");
            //    }
            //    //TupleList.Add(new Tuple<int, int>(n, i));
            //}

            //TODO * Bad output for compare tool
            //for (int i = 3; i <= n; i++)
            //{
            //    for (int j = 3; j <= n/2; j++)
            //    {
            //        TupleList.Add(new Tuple<int, int>(n, i));
            //    }
            //}

            for (int i = n; i > 1; i--)
            {
                for (int j = i/2; j > 0; j--)
                {

                    if(i==10 && j == 2)
                    {
                        string bp = "";
                    }
                    if (j == 2)
                    {
                        if (i % 2 == 0 || isPrime(i - 2, (i - 2).Sqrt()))
                        {
                            sum++;
                            TupleList.Add(new Tuple<int, int>(i, j));
                        }
                    }
                    else if (j == 1)
                    {
                        if (isPrime(i, i.Sqrt()))
                        {
                            sum++;
                            //DebugLogger.AddLine($"P({i, 3}, {1, 3}) = 1");
                            TupleList.Add(new Tuple<int, int>(i, j));
                        }
                    }
                    else
                    {
                        TupleList.Add(new Tuple<int, int>(i, j));
                        sum++;
                    }
                }

            }
            DebugLogger.AddLine($"Sum = {sum}");
            
            return sum;
        }

        private BigInteger S_Ben(int n)//, ref List<Tuple<int, int>> TupleList)
        {
            BigInteger sum = 0;

            //GIVEN S(N)
            //If S(N),  N - 2 = x
            //(x * (x + 1))/2 = B
            //(B * (B + 1))/2 = sum

            //int x, B;
            //x = n - 2;
            //B = (x * (x + 1)) / 2;
            //sum = (B * (B + 1)) / 2;



            for(int i = 3; i <= n; i++)
            {
                sum = sum + ((i / 2) - 2);
                
            }
            for(int i = n; i > 0; i--)
            {
                for(int j = 1; j < 2; j++)
                {
                    if (j == 2)
                    {
                        if (i % 2 == 0 || isPrime(i - 2, (i - 2).Sqrt())) { sum++; }
                    }else
                    {
                        if (isPrime(i, i.Sqrt()))
                        {
                            sum++;
                            //DebugLogger.AddLine($"P({i, 3}, {1, 3}) = 1");
                            //TupleList.Add(new Tuple<int, int>(i, 1));
                        }
                    }
                }

            }
            DebugLogger.AddLine($"Sum = {sum}");




            //int i = n;
            //int x = 1;
            //for (int j = i / 2; j > 2; j--)
            //{
            //    DebugLogger.AddLine($"x = {x} : i = {i} : j = {j}");
            //    sum = sum + (((j * (j + 1)) / 2)-3);
            //    x = x + 2;
            //    DebugLogger.AddLine($"sum = {sum}");
            //    //TupleList.Add(new Tuple<int, int>(i, j));
            //}


            //(MUST ALSO SOLVE FOR ALL (N,y) PAIRS WHERE y IN {1, 2})


            //for (int i = n; i > 0; i--)
            //{
            //    for (int j = i / 2; j > 1; j--)
            //    {
            //        if (j == 2)
            //        {
            //            if (i % 2 == 0 || isPrime(i - 2, (i - 2).Sqrt()))
            //            {
            //                sum++;
            //                DebugLogger.AddLine($"P({i,3}, {j,3}) = 1");
            //               // TupleList.Add(new Tuple<int, int>(i, j));
            //            }
            //        }
            //        else
            //        {
            //            sum++;
            //            DebugLogger.AddLine($"P({i,3}, {j,3}) = 1");
            //            //TupleList.Add(new Tuple<int, int>(i, j));
            //        }
            //    }

            //    if (isPrime(i, i.Sqrt()))
            //    {
            //        sum++;
            //        DebugLogger.AddLine($"P({i,3}, {1,3}) = 1");
            //       // TupleList.Add(new Tuple<int, int>(i, 1));
            //    }
            //}


            return sum;
        }

        private BigInteger S_new(int n, ref List<Tuple<int, int>> TupleList)
        {
            BigInteger sum = 0;

            for (int i = n; i > 0; i--)
            {
                for (int j = i / 2; j > 1; j--)
                {
                    //if (j == 2)
                    //{
                    //    if (i % 2 == 0 || isPrime(i - 2, (i - 2).Sqrt()))
                    //    {
                    //        sum++;
                    //        //DebugLogger.AddLine($"P({i,3}, {j,3}) = 1");
                    //        TupleList.Add(new Tuple<int, int>(i, j));
                    //    }
                    //}
                    //else
                    //{
                    //    sum++;
                    //    //DebugLogger.AddLine($"P({i,3}, {j,3}) = 1");
                    //    TupleList.Add(new Tuple<int, int>(i, j));
                    //}
                }

                var sumz = (3 * (3 + i / 2)) / 2;

                //if (i % 2 == 0 || isPrime(i - 2, (i - 2).Sqrt()))
                //{
                //    sum++;
                //    //DebugLogger.AddLine($"P({i,3}, {j,3}) = 1");
                //    TupleList.Add(new Tuple<int, int>(i, j));
                //}

                if (isPrime(i, i.Sqrt()))
                {
                    sum++;
                    //DebugLogger.AddLine($"P({i, 3}, {1, 3}) = 1");
                    TupleList.Add(new Tuple<int, int>(i, 1));
                }
            }
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
                        if (i % 2 == 0 || isPrime(i - 2, (i - 2).Sqrt()))
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

                if (isPrime(i, i.Sqrt()))
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