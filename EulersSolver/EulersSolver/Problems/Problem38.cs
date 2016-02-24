using System;
using System.Collections.Generic;
using System.Text;

namespace EulersSolver.Problems
{
    internal class Problem38 : BaseProblem
    {
        protected override int ProblemNumber => 38;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
            Take the number 192 and multiply it by each of 1, 2, and 3:

            192 × 1 = 192
            192 × 2 = 384
            192 × 3 = 576
            By concatenating each product we get the 1 to 9 pandigital, 192384576. We will call 192384576 the concatenated product of 192 and (1,2,3)

            The same can be achieved by starting with 9 and multiplying by 1, 2, 3, 4, and 5, giving the pandigital, 918273645, which is the concatenated product of 9 and (1,2,3,4,5).

            What is the largest 1 to 9 pandigital 9-digit number that can be formed as the concatenated product of an integer with (1,2, ... , n) where n > 1?
            */

            Initialize();

            //192 × 1 = 192
            //192 × 2 = 384
            //192 × 3 = 576

            var listOfNumbers = new List<int>();
            var listPerm = GenerateListOfPermutations();

            // 1 .. 200
            for (int i = 1; i <= 50000; i++)
            {
                for (int pandigitalItr = 1; pandigitalItr <= 9; pandigitalItr++)
                {
                    var number = "";
                    var builder = new StringBuilder();
                    builder.Append(number);
                    for (int j = 1; j <= pandigitalItr; j++)
                    {
                        builder.Append(i * j);
                    }
                    number = builder.ToString();

                    if (number.Length == 9 && listPerm.Contains(number))
                    {
                        //DebugLog($"{i, 3} x ({pandigitalItr}) = {number}");
                        listOfNumbers.Add(int.Parse(number));
                    }
                }
            }
            listOfNumbers.Sort();
            listOfNumbers.Reverse();
            DebugLog(listOfNumbers[0]);
            //listOfNumbers.ForEach(x => DebugLog(x));

            int answer = 1;
            Finalize(answer);
        }

        public static List<string> GenerateListOfPermutations()
        {
            var numbers = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            // Using a hash set instead of a List because there are going to be duplicates and hash doesn't allow duplicates
            var ListOfPermutations = new List<string>();

            foreach (var v in Permutations(numbers))
            {
                ListOfPermutations.Add(new string(v));
            }

            return ListOfPermutations;
        }
    }
}
