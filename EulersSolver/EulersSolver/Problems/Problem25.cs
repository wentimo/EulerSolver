using System.Numerics;

namespace EulersSolver.Problems
{
    internal class Problem25 : BaseProblem
    {
        protected override int ProblemNumber => 25;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
               The Fibonacci sequence is defined by the recurrence relation:

                Fn = Fn−1 + Fn−2, where F1 = 1 and F2 = 1.
                Hence the first 12 terms will be:

                F1 = 1
                F2 = 1
                F3 = 2
                F4 = 3
                F5 = 5
                F6 = 8
                F7 = 13
                F8 = 21
                F9 = 34
                F10 = 55
                F11 = 89
                F12 = 144
                The 12th term, F12, is the first term to contain three digits.

                What is the Index of the first term in the Fibonacci sequence to contain 1000 digits?
            */

            Initialize();

            //  Console.WriteLine("What length do you want to test for?");
            // string userInput = Console.ReadLine();
            //  int value;

            //  if (Int32.TryParse(userInput, out value))
            //{
            //    FibonacciHolder values = ReturnIndex(value);
            //    Finalize(values.Index);
            //}

            FibonacciHolder values = ReturnIndex(1000);
            Finalize(values.Index);
        }

        public class FibonacciHolder
        {
            private BigInteger Value { get; }
            public readonly int Index;

            public FibonacciHolder(BigInteger fibvalue, int countIndex)
            {
                Value = fibvalue;
                Index = countIndex;
            }
        }

        private static FibonacciHolder ReturnIndex(int lengthLimit)
        {
            var count = 2;
            BigInteger fib1 = new BigInteger(1), fib2 = new BigInteger(1);

            var nextIteration = true;

            while (nextIteration)
            {
                var holder = fib1;

                fib1 = fib1 + fib2;
                fib2 = holder;
                count++;
                nextIteration = fib1.ToString().Length < lengthLimit;
            }

            return new FibonacciHolder(fib1, count);
        }
    }
}