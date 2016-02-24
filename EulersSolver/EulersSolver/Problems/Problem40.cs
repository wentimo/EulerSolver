using System.Text;

namespace EulersSolver.Problems
{
    internal class Problem40 : BaseProblem
    {
        protected override int ProblemNumber => 40;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
            An irrational decimal fraction is created by concatenating the positive integers:

            0.123456789101112131415161718192021...

            It can be seen that the 12th digit of the fractional part is 1.

            If dn represents the nth digit of the fractional part, find the value of the following expression.

            d1 × d10 × d100 × d1000 × d10000 × d100000 × d1000000
            */
            Initialize();

            var value = 1;
            var digit = 1;

            var numberBuilder = new StringBuilder(".");

            while (numberBuilder.Length < 1000001)
            {
                numberBuilder.Append(value++);
            }

            var number = numberBuilder.ToString();

            value = 1;

            digit = number[1] - '0';
            value *= digit;

            digit = number[10] - '0';
            value *= digit;

            digit = number[100] - '0';
            value *= digit;

            digit = number[1000] - '0';
            value *= digit;

            digit = number[10000] - '0';
            value *= digit;

            digit = number[100000] - '0';
            value *= digit;

            digit = number[1000000] - '0';
            value *= digit;

            Finalize(value);
        }
    }
}