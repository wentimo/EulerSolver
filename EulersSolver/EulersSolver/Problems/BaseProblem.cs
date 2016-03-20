using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EulersSolver.Problems
{
    public abstract class BaseProblem
    {
        protected abstract string Solve();

        /// <summary>
        /// Joins the list of problem numbers together with the chosen join string.
        /// </summary>
        /// <param name="str">seperator to join the problems together with</param>
        /// <returns>string created</returns>
        public static string JoinListOfProblems(string str) => string.Join(str, GetProblemList().ToArray());

        /// <summary>
        /// Returns the answer as a string, or a message saying the answer could not be found.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="verbose"></param>
        /// <returns></returns>
        public static string GetAnswer(int id, bool verbose = false)
        {
            var problem = GetProblem(id);
            if (problem == null) return $"Problem {id} has not been solved.";

            if (verbose)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                string answer = problem.Solve();
                stopWatch.Stop();
                return $"Problem {id} : {answer}, Duration : {stopWatch.ElapsedTicks} ticks";
            }
            return problem.Solve();
        }

        /// <summary>
        /// Returns the chosen Problem object based on the parameter passed
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BaseProblem object to call .Solve() or null</returns>
        private static BaseProblem GetProblem(int id)
        {
            var assemblyList = typeof(BaseProblem).Assembly.GetTypes();
            var problem = assemblyList.FirstOrDefault(x => x.FullName.Equals($"EulersSolver.Problems.Problem{id}"));

            if (problem == null) return null;

            return (BaseProblem)Activator.CreateInstance(problem);
        }

        /// <summary>
        ///  Get a list<int> of all of the existings problem files in the solution.
        /// </summary>
        /// <returns></returns>
        public static List<int> GetProblemList()
        {
            var problemNumbers = new List<int>();
            var problemTypes = typeof(BaseProblem).Assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(BaseProblem)));

            foreach (var problem in problemTypes)
            {
                var value = 0;
                if (int.TryParse(problem.Name.Substring(7), out value))
                {
                    problemNumbers.Add(value);
                }
            }

            problemNumbers.Sort();
            return problemNumbers;
        }
    }
}