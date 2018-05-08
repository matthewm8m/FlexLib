using System;
using FlexLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexLibTests
{
    [TestClass]
    public class PercolationGridTest
    {
        private PercolationGrid GridA, GridB, GridC, GridRandom;

        [TestInitialize]
        public void TestInitialize()
        {
            // Create and setup Grid A (3x3 No Percolation)
            GridA = new PercolationGrid(3, 3);
            GridA[0, 0] = true;
            GridA[0, 1] = true;
            GridA[0, 2] = true;
            GridA[1, 0] = true;
            GridA[1, 1] = true;
            GridA[1, 2] = true;
            GridA[2, 1] = true;
            GridA[2, 1] = false;

            // Create and setup Grid B (5x3 Percolation)
            GridB = new PercolationGrid(5, 3);
            GridB[0, 0] = true;
            GridB[1, 0] = true;
            GridB[2, 0] = true;
            GridB[2, 1] = true;
            GridB[2, 2] = true;
            GridB[3, 2] = true;
            GridB[4, 2] = true;

            // Create and setup Grid C (3x5 Percolation)
            GridC = new PercolationGrid(3, 5);
            GridC[0, 0] = true;
            GridC[1, 0] = true;
            GridC[1, 1] = true;
            GridC[1, 2] = true;
            GridC[2, 2] = true;

            // Create Random Grid (5x5)
            GridRandom = new PercolationGrid(5, 5);
        }

        [TestMethod]
        public void TestDimensions()
        {
            // Test Grid A
            Assert.AreEqual(3, GridA.Rows);
            Assert.AreEqual(3, GridA.Columns);

            // Test Grid B
            Assert.AreEqual(5, GridB.Rows);
            Assert.AreEqual(3, GridB.Columns);

            // Test Grid C
            Assert.AreEqual(3, GridC.Rows);
            Assert.AreEqual(5, GridC.Columns);

            // Test Random Grid
            Assert.AreEqual(5, GridRandom.Rows);
            Assert.AreEqual(5, GridRandom.Columns);
        }

        [TestMethod]
        public void TestRandomization()
        {

        }

        [TestMethod]
        public void TestEvaluation()
        {
            // Test Grid A No Percolation
            Assert.IsFalse(GridA.Evaluate());

            // Test Grid A Percolation
            GridA[2, 1] = true;
            Assert.IsTrue(GridA.Evaluate());

            // Test Grid B Percolation
            Assert.IsTrue(GridB.Evaluate());

            // Test Grid B No Percolation
            GridB[4, 2] = false;
            Assert.IsFalse(GridB.Evaluate());

            // Test Grid C Percolation
            Assert.IsTrue(GridC.Evaluate());

            // Test Grid C No Percolation
            GridC[1, 1] = false;
            Assert.IsFalse(GridC.Evaluate());
        }
    }
}
