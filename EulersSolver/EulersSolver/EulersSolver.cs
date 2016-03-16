using EulersSolver.Problems;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EulersSolver
{
    public class EulersConsole
    {
        private const string PromptString = "> Options: Enter #. (Create). (Q)uit. (L)ist. (C)lear. (D)ebug. (V)erbose.";

        private const string PromptNumber = "What problem number do you want to create?";

        [STAThread]
        public static void Main(string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            var verbose = false;
            var value = 0;
            var userInput = Prompt(PromptString);

            while (CheckInput(userInput))
            {
                switch (userInput.ToUpper())
                {
                    case "CREATE":
                        {
                            userInput = Prompt(PromptNumber);
                            if (int.TryParse(userInput, out value))
                            {
                                var output = CreateProblemClassFile(value);
                                if (output.Length > 0)
                                {
                                    Console.WriteLine(output);
                                    Console.Read();
                                    System.Environment.Exit(1);
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
                            Console.WriteLine(EulersLogger.GetLogText() + Environment.NewLine);
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
                            if (int.TryParse(userInput, out value))
                            {
                                EulersLogger.Initialize(value);
                                Console.WriteLine(BaseProblem.GetAnswer(value, verbose));
                                EulersLogger.FinalizeLog();
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

        private static bool CheckInput(string userInput) => userInput.ToUpper() != "Q";

        private static string Prompt(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        private static string CreateProblemClassFile(int ProblemNumber)
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
    }

    public class EulersLogger
    {
        public static void Initialize(int ProblemNumber)
        {
            _logFile = new Logger($"Problem{ProblemNumber}.Debug.txt");
        }

        public static void FinalizeLog()
        {
            Logger.DumpLog();
        }

        private static Logger _logFile;

        private class Logger
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
            }
        }

        public static void DebugLog<T>(T logmessage)
        {
            Logger.Log(logmessage.ToString());
        }

        public static string GetLogText() => Logger.GetLogText();
    }
}