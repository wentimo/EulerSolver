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
            Assert.AreEqual("233168", BaseProblem.GetAnswer(1));
            Assert.AreNotEqual("1", BaseProblem.GetAnswer(1));
        }
    }
}