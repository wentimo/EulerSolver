using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EulersSolver.Problems
{
    public abstract class BaseProblem
    {
        public static string JoinListOfProblems(string str) => string.Join(str, GetProblemList().ToArray());

        public static string GetAnswer(int id, bool verbose = false)
        {
            var problem = GetProblem(id);
            if (problem == null) return $"Problem {id} has not been solved.";

            if (verbose)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var answer = BaseProblem.GetAnswer(id);
                stopWatch.Stop();
                return $"Problem {id} : {answer}, Duration : {stopWatch.ElapsedTicks} ticks";
            }
            return problem.Solve();
        }

        private static BaseProblem GetProblem(int id)
        {
            var assemblyList = typeof(BaseProblem).Assembly.GetTypes();
            //todo fix this line
            var problem = assemblyList.FirstOrDefault(x => x.FullName.Equals("EulersSolver.Problems.Problem" + id.ToString()));

            if (problem == null) return null;

            return (BaseProblem)Activator.CreateInstance(problem);
        }

        private static List<int> GetProblemList()
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

        /// <summary>
        /// Basically the 'main' function of the problems. Call to generate the answer property of the problem.
        /// </summary>
        protected abstract string Solve();
    }
}