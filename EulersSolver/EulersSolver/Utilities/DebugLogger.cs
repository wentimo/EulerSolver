using EulersSolver.Problems;
using System;
using System.IO;
using System.Text;

namespace EulersSolver.Utilities
{
    /// <summary>
    ///  Static class that enables all of the Problem files to contain a DebugLogger.AddLine method to easily allow debugging
    ///  inside of the problems without worrying about creating instantiating files, threading, etc.
    /// </summary>
    /// <example>
    ///  This class enables the following line in Problem1.cs:
    ///  DebugLogger.AddLine("Debug Line for Problem 1");
    ///  When Problem1.cs is run it will create a Problem1.Debug.txt file that contains the text passed.
    /// </example>
    public static class DebugLogger
    {
        private static StringBuilder _stringBuilder;
        private static string extension = "txt";

        public static void SetExtension(string ext)
        {
            extension = ext;
        }

        /// <summary>
        /// If used within DebugLog.DebugAndSolve it saves the line to a text file. Otherwise sends it to the console
        /// default it to the console instead.
        /// </summary>
        /// <param name="line">message line to display</param>
        public static void AddLine<T>(T line)
        {
            if (_stringBuilder == default(StringBuilder))
                Console.WriteLine(line);
            else
                _stringBuilder.Append(line + Environment.NewLine);
        }

        /// <summary>
        /// An overload of BaseProblem.GetAnswer. Allows calls of DebugLogger.AddLine to send lines to debug txt files.
        /// </summary>
        /// <param name="ProblemNumber">Number to create</param>
        /// <param name="verbose">Display verbose answer or not</param>
        /// <returns></returns>
        public static string DebugAndSolve(int ProblemNumber, ref string filename, bool verbose = false)
        {
            filename = $"Problem{ProblemNumber}.Debug.{extension}";
            _stringBuilder = new StringBuilder();

            var answer = BaseProblem.GetAnswer(ProblemNumber, verbose);

            string _fileBody = _stringBuilder.ToString();
            if (verbose)
            {
                _fileBody = answer + Environment.NewLine + Environment.NewLine + _fileBody;
            }

            if (_fileBody.Length > 0)
            {
                File.WriteAllText(filename, _fileBody);
            }

            return answer;
        }

        /// <summary>
        /// Attempts to get the debug log text of a specific problem number.
        /// </summary>
        /// <param name="ProblemNumber">Problem numiober to check for</param>
        /// <param name="text">will be filled with the text of the problem if found</param>
        /// <returns>true if file exists, false otherwise</returns>
        public static bool AttemptGetProblemText(int ProblemNumber, out string text)
        {
            var _fileName = $"Problem{ProblemNumber}.Debug.txt";
            text = "";
            if (!File.Exists(_fileName)) _fileName = $"Problem{ProblemNumber}.Debug.csv";

            if (File.Exists(_fileName))
            {
                using (var file = new StreamReader(_fileName))
                {
                    text = file.ReadToEnd();
                }
                return true;
            }
            return false;
        }
    }
}