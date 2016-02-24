using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ExtensionMethods;
using System.Text;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static int Sqrt(this int integer)
        {
            return (int)Math.Ceiling(Math.Sqrt((double)integer));
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var number in enumeration)
            {
                action?.Invoke(number);
            }
        }

        public static string RemoveSpaces(this string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }

        public static void AttemptRemoveNumbers(this List<int> numList, params int[] values)
        {
            foreach (var t in values.Where(numList.Contains))
                numList.Remove(t);
        }
    }
}

namespace EulersSolver.Problems
{
    public abstract class BaseProblem
    {
        #region Properties

        protected abstract int ProblemNumber { get; }      // An int representation of what problem number we're in. Used for output
        protected abstract bool HasBeenSolved { get; }         // Used to keep track of if the problem is completed or not
        private string Answer { get; set; }      // Used to hold the answer. String format because it may be int, double, long, etc.
        private long DurationInMilliseconds { get; set; } // Used to record how long the problem took for display
        private Stopwatch _stopWatch;                    // Used for clocking how long problems take
        private Logger _logFile;                         // Used for outputting to the debug log

        #endregion Properties

        #region Class Definitions

        // Used to centralize logging across the problems
        class Logger
        {
            private static bool _fileCreated = false;
            private static string _filePath;
            private static StringBuilder _stringBuilder;

            // Creates or overwrite the existing log
            public Logger(string filePathString)
            {
                _filePath = filePathString;
                _stringBuilder = new StringBuilder();

                try
                {
                    File.Delete(_filePath);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static string GetLogText()
            {
                if (!_fileCreated) return "No Debug Log was generated for the last problem.\n";
                string fileContents;
                using (var file = new StreamReader(_filePath))
                {
                    fileContents = file.ReadToEnd();
                }
                return fileContents;
            }

            // Creates a new entry in the log
            public static void Log(string message)
            {
#if DEBUG
                if (!_fileCreated)
                {
                    using (File.Create(_filePath))
                    {
                        _fileCreated = true;
                    }
                }

                _stringBuilder.Append(message + Environment.NewLine);
            }

            public static void DumpLog()
            {
                if (_fileCreated)
                {
                    using (var w = File.AppendText(_filePath))
                    {
                        w.Write(_stringBuilder.ToString());
                        w.Flush();
                    }
                }               
#endif
            }
        }

        #endregion Class Definitions

        #region Math Functions

        public int IntPow(int x, uint pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        public int iFactorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * iFactorial(i - 1);
        }

        public BigInteger bigFactorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * bigFactorial(i - 1);
        }

        //https://stackoverflow.com/questions/239865/best-way-to-find-all-factors-of-a-given-number-in-c-sharp
        public List<int> Factor(int number)
        {
            List<int> factors = new List<int>();
            int max = (int)Math.Sqrt(number);  //round down
            for (int factor = 1; factor <= max; ++factor)
            { //test from 1 to the square root, or the int below it, inclusive.
                if (number % factor == 0)
                {
                    factors.Add(factor);
                    if (factor != number / factor)
                    { // Don't add the square root twice!  Thanks Jon
                        factors.Add(number / factor);
                    }
                }
            }
            return factors;
        }

        // Found this awesome code at http://csharphelper.com/blog/2014/08/use-the-sieve-of-eratosthenes-to-find-prime-numbers-in-c/
        // This creates a List of Booleans where you can check if a value x is prime by simply doing if(is_prime[x]);
        // todo I could probably figure a way to utilize a method to just call IsPrime(x) to iterate over a list/array
        protected static bool[] MakeSieve(int max)
        {
            // Make an array indicating whether numbers are prime.
            var isPrime = new bool[max + 1];
            for (var i = 2; i <= max; i++) isPrime[i] = true;

            // Cross out multiples.
            for (var i = 2; i <= max.Sqrt(); i++)
            {
                // See if i is prime.
                if (!isPrime[i]) continue;
                // Knock out multiples of i.
                for (var j = i * 2; j <= max; j += i)
                    isPrime[j] = false;
            }
            return isPrime;
        }

        // Got this from http://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
        // It will return all permutations of an array of any type based to it
        protected static IEnumerable<T[]> Permutations<T>(T[] values, int fromInd = 0)
        {
            if (fromInd + 1 == values.Length)
                yield return values;
            else
            {
                foreach (var v in Permutations(values, fromInd + 1))
                    yield return v;

                for (var i = fromInd + 1; i < values.Length; i++)
                {
                    SwapValues(values, fromInd, i);
                    foreach (var v in Permutations(values, fromInd + 1))
                        yield return v;
                    SwapValues(values, fromInd, i);
                }
            }
        }

        // used in Permutations
        private static void SwapValues<T>(IList<T> values, int pos1, int pos2)
        {
            if (pos1 == pos2) return;
            var tmp = values[pos1];
            values[pos1] = values[pos2];
            values[pos2] = tmp;
        }

        protected static List<int> GetListOfPrimes(int max)
        {
            var isPrime = MakeSieve(max);
            return new List<int>(Enumerable.Range(1, max).Where(x => isPrime[x]));
        }

        #endregion Math Functions

        #region Methods

        public void DebugLogElapsedTime(string label)
        {
            _stopWatch.Stop();
            DebugLog($"{label} : Elapsed time - {_stopWatch.ElapsedMilliseconds} milliseconds");
            _stopWatch.Start();
        }

        public static string JoinListOfProblems(string str) => string.Join(str, GetProblemList().ToArray());

        public static string GetLogText() => Logger.GetLogText();

        public static string GetAnswer(int id, bool verbose = false)
        {
            var problem = GetProblem(id);
            if (problem == null || !problem.HasBeenSolved) return $"Problem {id} has not been solved.";
            problem.Solve();
            return verbose ? $"Problem {id} : {problem.Answer}, Duration: {problem.DurationInMilliseconds} milliseconds" :
                             problem.Answer;
        }

        public static bool AttemptGetAnswerById(int id, out string value, bool verbose = false)
        {
            var problem = GetProblem(id);
            if (problem != null && problem.HasBeenSolved)
            {
                problem.Solve();
                value = verbose ? $"Problem {id} : {problem.Answer}, Duration: {problem.DurationInMilliseconds} milliseconds" :
                                    problem.Answer;
                return true;
            }
            value = $"Problem {id} has not been solved.";
            return false;
        }

        public static bool GetSolved(int id)
        {
            var problem = GetProblem(id);
            return problem != null && problem.HasBeenSolved;
        }

        public static string CreateProblemClassFile(int ProblemNumber)
        {      
            var _shellFile = @"..\..\..\Resources\ProblemShell.txt";
            var title = $"Problem{ProblemNumber}.cs";
            var savedFileLocation = "";

            string fileContents;
            using (var file = new StreamReader(_shellFile))
            {
                fileContents = file.ReadToEnd();
            }

            fileContents = fileContents.Replace("$num$", ProblemNumber.ToString());

            using (var fd = new SaveFileDialog())
            {
                fd.FileName = title;
                fd.InitialDirectory = Directory.GetCurrentDirectory() + "\\..\\..\\Problems";
                fd.Filter = "Class File | *.cs";
                fd.RestoreDirectory = true;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    using (var writer = new StreamWriter(fd.OpenFile()))
                    {
                        writer.WriteLine(fileContents);
                    }

                    savedFileLocation = fd.FileName;
                }
                else
                {
                    return "";
                }
            }

            return $"Saved {title} to {savedFileLocation}.\n End this session and add the class to the problem list before continuing.";
        }

        private static BaseProblem GetProblem(int id)
        {
            var assemblyList = typeof(BaseProblem).Assembly.GetTypes();
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
                if (int.TryParse(problem.Name.Substring(7), out value) && GetSolved(value))
                {
                    problemNumbers.Add(value);
                }
            }

            problemNumbers.Sort();
            return problemNumbers;
        }

        /// <summary>
        /// Used to send a string to the debug log.
        /// </summary>
        /// <param name="logmessage">The string to be sent to the debug log file.</param>
        protected static void DebugLog<T>(T logmessage)
        {
            Logger.Log(logmessage.ToString());
        }

        /// <summary>
        /// Initializes the problem which creates the debug text file and begins the _stopWatch that determines the amount of time the solution took.
        /// </summary>
        protected void Initialize()
        {
            _logFile = new Logger($"Problem{ProblemNumber}.Debug.txt");
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        /// <summary>
        /// Ends the stopwatch and displays the answer as well as the amount of time it took to complete in milliseconds.
        /// </summary>
        /// <param name="answer">The value to display as the answer to the Euler's problem</param>
        protected void Finalize(object answer)
        {
            _stopWatch.Stop();
            this.Answer = answer.ToString();
            this.DurationInMilliseconds = _stopWatch.ElapsedMilliseconds;
            Logger.DumpLog();
        }

        /// <summary>
        /// Basically the 'main' function of the problems. Call to generate the answer property of the problem.
        /// </summary>
        protected abstract void Solve();

        #endregion Methods
    }
}