using EulersSolver.Problems;
using System;

namespace EulersSolver
{
    internal static class EulersSolving
    {
        private const string PromptString = "> Options: Enter #. (Create). (Q)uit. (L)ist. (C)lear. (D)ebug. (V)erbose.";
        private const string PromptNumber = "What problem number do you want to create?";

        private static string Prompt(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        private static bool CheckInput(string userInput) => userInput.ToUpper() != "Q";

        [STAThread]
        public static void Main(string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            var verbose = true;
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
                                var output = BaseProblem.CreateProblemClassFile(value);
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
                            Console.WriteLine(BaseProblem.GetLogText() + Environment.NewLine);
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
                                if (BaseProblem.GetSolved(value))
                                {
                                    Console.WriteLine(BaseProblem.GetAnswer(value, verbose) + Environment.NewLine);
                                }
                                else
                                {
                                    Console.WriteLine($"Problem {value} has not been solved yet.\n");
                                }
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
    }
}