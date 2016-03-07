using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem22 : BaseProblem
    {
        protected override int ProblemNumber => 22;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
            Using names.txt (right click and 'Save Link/Target As...'), a 46K text file containing over five-thousand first names, begin by sorting it into alphabetical order.
            Then working out the alphabetical value for each name, multiply this value by its alphabetical position in the list to obtain a name score.

            For example, when the list is sorted into alphabetical order, COLIN, which is worth 3 + 15 + 12 + 9 + 14 = 53, is the 938th name in the list.
            So, COLIN would obtain a score of 938 � 53 = 49714.

            What is the total of all the name scores in the file?
            */

            Initialize();

            const string problemFilePath = @"..\..\..\Resources\p022_names.txt";

            var fileText = "";
            using (var file = new StreamReader(problemFilePath))
            {
                fileText = file.ReadToEnd();
            }

            fileText = fileText.Replace("\"", "");

            var split = fileText.Split(',');

            Array.Sort(split);

            int index = 1, count = 0;
            foreach (var name in split)
            {
                count += NameScore(name, index++);
            }

            Finalize(count);
        }

        int NameScore (string name, int index)
        {
            int score = 0;
            foreach (char ch in name)
            {
                score += (ch - '@');
            }
            return score * index;
        }
    }
}
