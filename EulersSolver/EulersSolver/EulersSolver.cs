using EulersSolver.Problems;
using EulersSolver.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

// TODO Consider creating a help menu
// TODO Consider moving prompts to resource file
namespace EulersSolver
{
    public class EulersConsole
    {
        private const string PromptString = "> Options: # (Create). (Q)uit. (L)ist. (C)lear. (D)ebug. (V)erbose.";

        private const string PromptProblemNumber = "What problem number?";

        private static string lastSolved = null;

        [STAThread]
        public static void Main(string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            ProcessInput();
        }

        private static void ProcessInput()
        {
            bool verbose = true;
            int problemNumber = -1, lastProblem = -1;
            string userInput = Prompt(PromptString);

            while (CheckInput(userInput))
            {
                var input = userInput.ToUpper().Split(' ');
                switch (input[0])
                {
                    case "CREATE":
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

                    case "O":
                        Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                        break;

                    case "C":
                        Console.Clear();
                        break;

                    case "L":
                        Console.WriteLine(BaseProblem.JoinListOfProblems(",") + Environment.NewLine);
                        break;

                    case "D":
                        if (!string.IsNullOrWhiteSpace(lastSolved))
                        {
                            Console.WriteLine(File.ReadAllText(lastSolved));
                        }
                        break;

                    case "V":
                        verbose = !verbose;
                        Console.WriteLine("Verbose answer mode " + (verbose ? "enabled." : "disabled.") + Environment.NewLine);
                        break;

                    default:
                        if (int.TryParse(userInput, out problemNumber))
                        {
                            DebugLogger.SetExtension("csv");
                            Console.WriteLine(DebugLogger.DebugAndSolve(problemNumber, ref lastSolved, verbose) + Environment.NewLine);
                            lastProblem = problemNumber;
                        }
                        else
                        {
                            Console.WriteLine($"You entered : {userInput} which is not a valid choice.\n");
                        }
                        break;
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
        }
    }
}