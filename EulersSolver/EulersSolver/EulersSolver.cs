using EulersSolver.Problems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EulersSolver
{
    // TODO Consider creating a help menu
    // TODO Consider moving prompts to resource file

    public class EulersConsole
    {
        private const string PromptString = "> Options: Enter #. (Create). (Q)uit. (L)ist. (C)lear. (D)ebug. (V)erbose.";

        private const string PromptProblemNumber = "What problem number?";

        [STAThread]
        public static void Main(string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            HashSet<int> solved = new HashSet<int>();
            bool verbose = true;
            int problemNumber = -1, lastProblem = -1;
            string userInput = Prompt(PromptString);

            while (CheckInput(userInput))
            {
                var input = userInput.ToUpper().Split(' ');
                switch (input[0])
                {
                    case "CREATE":
                        {
                            if (input.Length > 1) userInput = input[1];
                            else userInput = Prompt(PromptProblemNumber);

                            if (int.TryParse(userInput, out problemNumber))
                            {
                                string fileLocation;
                                if (AttemptCreateProblemClassFile(problemNumber, out fileLocation))
                                {
                                    Console.WriteLine($"Saved Problem{problemNumber}.cs to {fileLocation}.\n End this session and add the class to the problem list before continuing.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"You entered : {userInput} which is not a valid choice.\n");
                            }

                            break;
                        }
                    case "C":
                        {
                            Console.Clear();
                            break;
                        }
                    case "L":
                        {
                            Console.WriteLine(BaseProblem.JoinListOfProblems(",") + Environment.NewLine);
                            break;
                        }
                    case "D":
                        {
                            string bodyText;

                            if (input.Length > 1)
                            {
                                if (int.TryParse(input[1], out problemNumber))
                                {
                                    if (DebugLogger.AttemptGetProblemText(problemNumber, out bodyText))
                                    {
                                        Console.WriteLine($"Contents of Problem{problemNumber}.Debug.txt:\n");
                                        Console.WriteLine(bodyText);
                                    }
                                    else
                                    {
                                        if (solved.Contains(problemNumber))
                                        {
                                            Console.WriteLine($"Problem{problemNumber}.Debug.txt does not exist.");
                                            Console.WriteLine($"Add debug lines to Problem{problemNumber}.cs first and then re-run the solver.\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Run the solver for Problem {problemNumber} first.");
                                            Console.WriteLine($"If no debug lines exist in Problem{problemNumber}.cs no text file will be created.\n");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"You entered : {userInput} which is not a valid choice.\n");
                                }
                            }
                            else
                            {
                                if (lastProblem > -1 && DebugLogger.AttemptGetProblemText(lastProblem, out bodyText))
                                {
                                    Console.WriteLine($"Contents of Problem{lastProblem}.Debug.txt:\n");
                                    Console.WriteLine(bodyText);
                                }
                                else
                                {
                                    Console.WriteLine($"Enter a problem number to see its debug output.");
                                    Console.WriteLine($"You will need to enter that problem number to generate a debug txt file first.\n");
                                }
                            }
                            break;
                        }
                    case "V":
                        {
                            verbose = !verbose;
                            Console.WriteLine("Verbose answer mode " + (verbose ? "enabled." : "disabled.") + Environment.NewLine);
                            break;
                        }
                    default:
                        {
                            if (int.TryParse(userInput, out problemNumber))
                            {
                                Console.WriteLine(DebugLogger.DebugAndSolve(problemNumber, verbose) + Environment.NewLine);
                                lastProblem = problemNumber;
                                solved.Add(lastProblem);
                            }
                            else
                            {
                                Console.WriteLine($"You entered : {userInput} which is not a valid choice.\n");
                            }
                            break;
                        }
                }

                userInput = Prompt(PromptString);
            }
        }

        // Continually prompts the user until they input Q.
        private static bool CheckInput(string userInput) => userInput.ToUpper() != "Q";

        // Prompts the user using the passed string and awaits their response.
        private static string Prompt(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        /// <summary>
        ///  Creates a Problem class file for the problem number passed. Generates a ProblemX.cs class file based on ProblemShell.txt
        ///  where X is the ProblemNumber passed. ProblemShell.cs is in the resource folder. You can change ProblemShell.cs's content to match what 
        ///  you want your base Problem class file to contain.
        /// </summary>
        /// <param name="ProblemNumber"></param>
        /// <example>
        /// CreateProblemClassFile(3) will create Problem3.cs and will attempt to save it to EulersSolver.Problems.Problems by default
        /// </example>
        /// <returns>
        /// Boolean whether or not the user cancelled the action of saving the class file.
        /// </returns>
        private static bool AttemptCreateProblemClassFile(int ProblemNumber, out string fileLocation)
        {
            var _shellFile = @"..\..\..\Resources\ProblemShell.txt";
            var title = $"Problem{ProblemNumber}.cs";

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

                    fileLocation = fd.FileName;
                }
                else
                {
                    fileLocation = "";
                    return false;
                }
                return true;
            }

            // return $"Saved {title} to {savedFileLocation}.\n End this session and add the class to the problem list before continuing.";
        }
    }

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
        public static string DebugAndSolve(int ProblemNumber, bool verbose = false)
        {
            string _filePath = $"Problem{ProblemNumber}.Debug.txt";
            _stringBuilder = new StringBuilder();

            var answer = BaseProblem.GetAnswer(ProblemNumber, verbose);

            string _fileBody = _stringBuilder.ToString();
            if (_fileBody.Length > 0)
            {
                File.WriteAllText(_filePath, _fileBody);
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