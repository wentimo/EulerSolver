using System.IO;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem67 : BaseProblem
    {
        protected override int ProblemNumber => 67;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
               By starting at the top of the triangle below and moving to adjacent numbers on the row below, the maximum total from top to bottom is 23.

                3
                7 4
                2 4 6
                8 5 9 3

                That is, 3 + 7 + 4 + 9 = 23.

                Find the maximum total from top to bottom of the triangle below:

                75
                95 64
                17 47 82
                18 35 87 10
                20 04 82 47 65
                19 01 23 75 03 34
                88 02 77 73 07 63 67
                99 65 04 28 06 16 70 92
                41 41 26 56 83 40 80 70 33
                41 48 72 33 47 32 37 16 94 29
                53 71 44 65 25 43 91 52 97 51 14
                70 11 33 28 77 73 17 78 39 68 17 57
                91 71 52 38 17 14 91 43 58 50 27 29 48
                63 66 04 68 89 53 67 30 73 16 69 87 40 31
                04 62 98 27 23 09 70 98 73 93 38 53 60 04 23
            */

            /*
               State 1:
               3
               7 4
               2 4 6
               8 5 9 3

                Assume n = number of rows (4). Maximum path algorithim; starting at n-1 check the greatest value we can get from the children of those nodes and add that to the value.

               Row 3 has a 2 4 and 6. Considering 2's children we have 8 and 5, since 8 > 5, 2's highest path is 2 + 8 = 10 so we replace 2 with 10.
                                      Considering 4's children we have 5 and 9, since 9 > 5, 4's highes tpath is 4 + 9 = 13 so we replace 4 with 13.
                                      Considering 6's children we have 9 and 3, since 9 > 3, 4's highest path is 6 + 3 = 9 so we replace 6 with 9.

               State 2:
              3
              7 4
              10 13 9

              Follow the same steps as above to get to state 3:
              3
              20 17

              Follow the same steps as above to get to state 4:
              23.

              The highest path sum for state 1: is 23.

            */
            Initialize();
            const string problemFilePath = @"..\..\..\Resources\p067_triangle.txt";
            using (var file = new StreamReader(problemFilePath))
            {
                var lineCount = File.ReadLines(problemFilePath).Count();
                // Jagged array to hold the triangle
                var triangle = new int[lineCount][];

                // Used for creating the arrays within the jagged array the correct Index/size
                var outercount = 0;

                // Fill in the array
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    triangle[outercount] = new int[outercount + 1];
                    var innercount = 0;
                    var values = line.Split(' ');
                    foreach (var value in values)
                    {
                        var test = 0;
                        if (int.TryParse(value, out test))
                        {
                            triangle[outercount][innercount] = test;
                        }
                        innercount++;
                    }
                    outercount++;
                }
                file.Close();

                while (lineCount > 1)
                {
                    var updateArray = triangle[lineCount - 2];
                    var childArray = triangle[lineCount - 1];

                    var itr = 0;
                    foreach (var value in updateArray)
                    {
                        var increaseBy = childArray[itr] > childArray[itr + 1] ? childArray[itr] : childArray[itr + 1];
                        updateArray[itr] += increaseBy;
                        itr++;
                    }
                    lineCount--;
                }

                var answer = triangle[0][0];
                Finalize(answer);
            }
        }
    }
}