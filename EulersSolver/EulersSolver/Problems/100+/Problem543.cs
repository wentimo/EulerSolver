using EulersSolver.Extensions;
using StackOverflow_Math;
using EulersSolver.MyMath;
using System;
using System.Collections.Generic;
using System.Linq;
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

            //int x = 700000000;
            //var primes = OutsideMath.SieveOfEratosthenes(x);
            //int y = Enumerable.Range(1, x).Where(z => primes[z]).Count();

            //int lowEnd = 3;
            //int highEnd = 44;

            //var fib = CustomMath.GeneratetFibonacciNumbers(highEnd);
            //fib = fib.Skip(lowEnd).Take(fib.Count() - lowEnd).ToList();

            //foreach (var prime in fib)
            //{
            //    DebugLogger.AddLine($"F({lowEnd++}) = {prime}");
            //}

            //var y = S_new(39088169);

            //Enumerable.Range(2, 19).ForEach(x => DebugLogger.AddLine($"S({x}) = {S(x)}"));
            var prevValue = S(1);
            foreach (var value in Enumerable.Range(2, 38))
            {
                
                string message = $"S({value}) = {S(value)}";
              //  DebugLogger.AddLine(message);
            }
            //var y = S(100);
            // return y.ToString();
            return 1.ToString();
        }
     
        private BigInteger S(int n)
        {
            BigInteger sum = 0;

            // I think I can change this into an equation somehow.
            for (int i = n; i > 5; i--)
            {
                sum += i / 2 - 2;
            }            
            DebugLogger.AddLine($"S({n, 2}) = 0 (+{sum})");
            var diff = sum;

            sum += n / 2;
            //DebugLogger.AddLine($"j == 2 && i % 2 == 0 logic : {sum} (+{sum - diff})");
            diff = sum;

            var primes = CustomMath.GetListOfPrimes(n);
           // var primes = OutsideMath.SieveOfEratosthenes(n);
            int primeCount = primes.Count;
            
            sum += primeCount - 2;
            //DebugLogger.AddLine($"j == 2 && i % 2 == 1 logic : {sum} (+{sum - diff})");
            diff = sum;

            sum += primeCount;
            //DebugLogger.AddLine($"j == 1 logic : {sum} (+{sum - diff})");
            return sum;
        }
    }
}