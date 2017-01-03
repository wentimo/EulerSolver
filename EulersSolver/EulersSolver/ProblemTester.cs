namespace EulersSolverTester
{
    using EulersSolver.Problems;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProblemTester
    {
        [TestMethod]
        public void TestSubmissions()
        {
            // Is the answer to Problem 1 equal to 233168? True.
            //Assert.AreEqual("233168", BaseProblem.GetAnswer(1));

            // Is the answer to Problem 1 not equal to 1? True.
            //Assert.AreNotEqual("1", BaseProblem.GetAnswer(1));

            Assert.AreEqual("669171001", BaseProblem.GetAnswer(28));
        }
    }
}