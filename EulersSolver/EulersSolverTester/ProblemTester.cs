using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulersSolverTester
{
    using EulersSolver.Problems;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProblemTester
    {

        //[TestInitialize]
        //public void Initialize()
        //{
            
        //}

        [TestMethod]
        public void TestSubmissions()
        {
            Assert.AreEqual("233168", BaseProblem.GetAnswer(1));
            Assert.AreNotEqual(  "1", BaseProblem.GetAnswer(1));
        }

    }
}
