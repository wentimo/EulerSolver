using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtensionMethods;

namespace EulersSolver.Problems
{
    internal class Problem41 : BaseProblem
    {
        protected override int ProblemNumber => 41;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
            We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once. 
            For example, 2143 is a 4-digit pandigital and is also prime.

            What is the largest n-digit pandigital prime that exists?
            */

            Initialize(); 
            var isPrime = MakeSieve(987654321);
            var listPerm = GenerateListOfPermutations();
            listPerm.Sort();
            listPerm.Reverse();

            var itr = 0;
            while (!isPrime[listPerm[itr]])
            {
                itr++;
            }

           // var answer = listPerm.Where(x => isPrime[x]).OrderByDescending(x => x).FirstOrDefault();           

            Finalize(listPerm[itr]);
        }

        public static List<int> GenerateListOfPermutations()
        {
            var numbers = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            // Using a hash set instead of a List because there are going to be duplicates and hash doesn't allow duplicates
            var ListOfPermutations = new List<int>();

            foreach (var v in Permutations(numbers))
            {
                ListOfPermutations.Add(int.Parse(new string(v)));
            }

            return ListOfPermutations;
        }
    }
}
