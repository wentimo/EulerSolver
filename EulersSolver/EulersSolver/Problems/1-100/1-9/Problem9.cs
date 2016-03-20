namespace EulersSolver.Problems
{
    internal class Problem9 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,

            a2 + b2 = c2
            For example, 3^2 + 4^2 = 9 + 16 = 25 = 5^2.

            There exists exactly one Pythagorean triplet for which a + b + c = 1000.
            Find the product abc.
            */

            int answer = 0;

            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    for (int k = 0; k < 500; k++)
                    {
                        if (i * i + j * j == k * k && i + j + k == 1000)
                        {
                            answer = i * j * k;
                            DebugLogger.AddLine($"({i})^2 + ({j})^2 = ({k})^2, {i} + {j} + {k} = 1000");
                        }
                    }
                }
            }

            return answer.ToString();
        }
    }
}