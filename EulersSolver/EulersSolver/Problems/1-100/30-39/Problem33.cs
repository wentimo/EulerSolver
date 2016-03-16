using System.Collections.Generic;

namespace EulersSolver.Problems
{
    internal class Problem33 : BaseProblem
    {
        protected override string Solve()
        {
            /*
            The fraction 49/98 is a curious fraction, as an inexperienced mathematician in attempting to simplify it may incorrectly believe that 49/98 = 4/8, which is correct, is obtained by cancelling the 9s.

            We shall consider fractions like, 30/50 = 3/5, to be trivial examples.

            There are exactly four non-trivial examples of this type of fraction, less than one in value, and containing two digits in the numerator and denominator.

            If the product of these four fractions is given in its lowest common terms, find the value of the denominator.
            */

            var ListOfMatches = new List<string>();
            for (double i = 10; i <= 99; i++)
            {
                for (double j = i + 1; j <= 99; j++)
                {
                    var numerator = i.ToString();
                    var denominator = j.ToString();
                    double value1 = 0;

                    if (i % 10 != 0 && j % 10 != 0)
                    {
                        if (numerator[0] == denominator[0])
                        {
                            value1 = double.Parse(numerator[1].ToString()) / double.Parse(denominator[1].ToString());
                        }
                        else if (numerator[0] == denominator[1])
                        {
                            value1 = double.Parse(numerator[1].ToString()) / double.Parse(denominator[0].ToString());
                        }
                        else if (numerator[1] == denominator[0])
                        {
                            value1 = double.Parse(numerator[0].ToString()) / double.Parse(denominator[1].ToString());
                        }
                        else if (numerator[1] == denominator[1])
                        {
                            value1 = double.Parse(numerator[0].ToString()) / double.Parse(denominator[0].ToString());
                        }
                    }

                    if (value1 == i / j)
                    {
                        ListOfMatches.Add($"{numerator}/{denominator}");
                    }
                }
            }

            var listOfValues = new List<double>();

            foreach (var fractionString in ListOfMatches)
            {
                var args = fractionString.Split('/');
                var fraction = double.Parse(args[0]) / double.Parse(args[1]);
                listOfValues.Add(fraction);
            }

            double answer = 1;

            foreach (var fraction in listOfValues)
            {
                answer = answer * fraction;
            }

            return (1.0 / answer).ToString();
        }
    }
}