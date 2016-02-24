using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulersSolver.Problems
{
    internal class Problem68 : BaseProblem
    {
        protected override int ProblemNumber => 68;
        protected override bool HasBeenSolved => true;

        protected override void Solve()
        {
            /*
            
            */

            Initialize();
            var answer = GonSolver(3);
            Finalize(answer);
        }

        public class Gon
        {
            public List<Triplet> Triplets;

            public Gon(List<Triplet> triplets)
            {
                this.Triplets = triplets;
            }

            public bool SumsEquals()
            {
                if (!Triplets.Any()) return false;

                bool equal = true;
                int sum = Triplets.First().ToSum();

                foreach(Triplet trip in Triplets.Skip(1))
                {
                    equal &= (sum == trip.ToSum());
                }

                return equal;
            }

            public string ToDisplayString()
            {
                var sb = new StringBuilder();

                foreach (var str in Triplets)
                {
                    sb.Append(str.Lead);
                    sb.Append(str.Middle);
                    sb.Append(str.End);
                    //if (str != Triplets.Last())
                    //{
                    //    sb.Append('|');
                    //}
                }

                return sb.ToString();
            }
        }

        public class Triplet
        {
            public int Lead { get; set; }
            public int Middle { get; set; }
            public int End { get; set; }

            public Triplet(int pnt1, int pnt2, int pnt3)
            {
                this.Lead = pnt1;
                this.Middle = pnt2;
                this.End = pnt3;
            }

            public int ToSum()
            {
                return Lead + Middle + End;
            }
        }

        string GonSolver(int number)
        {
            if (number < 3) return "No answer";
            int high = number * 2;
            var list = Enumerable.Range(1, high).ToArray();

            List<string> ListOfGons = new List<string>();
            var perms = new List<int[]>();

            foreach (var perm in Permutations(list))
            {
                perms.Add(perm);
            }

            foreach (var x in perms)
            {
                if (perms.ToString().StartsWith("432"))
                {
                    string bp = "";
                }
                List<Triplet> triplets = new List<Triplet>();
                int lead = x[0];
                int itr = 1;
                int holder = x[itr];

                while (triplets.Count < number)
                {
                    int p1, p2, p3;

                    if (itr == 1)
                    {
                        p1 = lead;
                        p2 = x[itr];
                        p3 = x[itr + 1];
                        itr++;
                    }
                    else
                    {
                        p1 = x[itr + 1];
                        p2 = x[itr];
                        if (itr + 2 == high)
                        {
                            p3 = holder;
                        }
                        else
                        {
                            p3 = x[itr + 2];
                        }
                        itr += 2;
                    }

                    Triplet trip = new Triplet(p1, p2, p3);
                    if (p1 == 4 && p2 == 3 && p3 == 2)
                    {
                        string bp = "";
                    }

                    triplets.Add(trip);
                }
                // gon 3
                //var tri1 = new Triplet(x[0], x[1], x[2]);
                //var tri2 = new Triplet(x[3], x[2], x[4]);
                //var tri3 = new Triplet(x[5], x[4], x[1]);

                // gon 4
                //var tri1 = new Triplet(x[0], x[1], x[2]);
                //var tri2 = new Triplet(x[3], x[2], x[4]);
                //var tri3 = new Triplet(x[5], x[4], x[6]);
                //var tri4 = new Triplet(x[7], x[6], x[1]);

                //gon 5
                //var tri1 = new Triplet(x[0], x[1], x[2]);
                //var tri2 = new Triplet(x[3], x[2], x[4]);
                //var tri3 = new Triplet(x[5], x[4], x[6]);
                //var tri4 = new Triplet(x[7], x[6], x[8]);
                //var tri5 = new Triplet(x[9], x[8], x[1]);
                Gon gon = new Gon(triplets);
                var displaystring = gon.ToDisplayString();
                if (displaystring == "432621513")
                {
                    string bp = "";
                }

                if (gon.SumsEquals()) ListOfGons.Add(gon.ToDisplayString());
 
            }
            ListOfGons.Sort();
            ListOfGons.ForEach(DebugLog);
            return ListOfGons.Last();
        }
    }
}

