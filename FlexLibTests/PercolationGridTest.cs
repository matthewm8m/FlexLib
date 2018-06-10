using FlexLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FlexLibTests
{
    /// <summary>
    /// Tests the PercolationGrid class.
    /// </summary>
    [TestClass]
    public class PercolationGridTest
    {
        private const int COUNT_GRIDS_NEGATIVE = 8;     // Number of grids to initialize with negative dimensions
        private const int COUNT_GRIDS_VERTICAL1 = 8;    // Number of grids to initialize with one open column
        private const int COUNT_GRIDS_VERTICAL2 = 8;    // Number of grids to initialize with two half-open columns and a connector
        private const int COUNT_GRIDS_SQUIGGLE = 8;     // Number of grids to initialize with a squiggle between five columns
        private const int COUNT_GRIDS_RANDOM = 4096;    // Number of grids to initialize with random openings
        private const int GRID_MAX_ROWS = 10;           // Maximum rows of generated grids
        private const int GRID_MAX_COLS = 10;           // Maximum columns of generated grids
        private const double RANDOM_STEP = 0.1;         // Step from 0.0 to 1.0 for probability in random probability test
        private const double RANDOM_THRESHOLD = 0.10;   // Leniency for random grid generation

        private readonly Random RandomGenerator = new Random(); // Random number generator is used for generating random column tests

        /// <summary>
        /// Finds the ratio of open cells to total cells inside a grid.
        /// </summary>
        /// <param name="grid">The percolation grid to query.</param>
        /// <param name="trials">The number of trials to perform.</param>
        /// <param name="probability">The theoretical probability of an open cell.</param>
        /// <returns>The experimental probability of an open cell.</returns>
        private double FindOpenRatio(PercolationGrid grid, int trials, double probability)
        {
            double sumRatio = 0.00;

            for (int n = 0; n < trials; n++)
            {
                // Randomize grid
                grid.Randomize(probability);

                // Count open cells in grid
                int open = 0;
                for (int i = 0; i < grid.Rows; i++)
                    for (int j = 0; j < grid.Columns; j++)
                        if (grid.GetIsOpen(i, j))
                            open++;

                // Sum ratio over multiple trials
                sumRatio += (double)open / (grid.Rows * grid.Columns);
            }

            // Return average ratio
            return sumRatio / trials;
        }

        /// <summary>
        /// Tests that an exception is thrown every time a grid is initialized with negative dimensions.
        /// </summary>
        [TestMethod]
        public void TestPercolationGridNegativeDimension()
        {
            // Check that trying to make a percolation grid with negative dimensions throws an exception.
            for (int i = 0; i < COUNT_GRIDS_NEGATIVE; i++)
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                {
                    switch (RandomGenerator.Next(3))
                    {
                        case 0: // Generate negative width and height
                            new PercolationGrid(
                                -RandomGenerator.Next(1, GRID_MAX_ROWS),
                                -RandomGenerator.Next(1, GRID_MAX_COLS));
                            break;
                        case 1: // Generate negative width
                            new PercolationGrid(
                                -RandomGenerator.Next(1, GRID_MAX_ROWS),
                                +RandomGenerator.Next(1, GRID_MAX_COLS));
                            break;
                        case 2: // Generate negative height
                            new PercolationGrid(
                                +RandomGenerator.Next(1, GRID_MAX_ROWS),
                                -RandomGenerator.Next(1, GRID_MAX_COLS));
                            break;
                    }
                });
            }
        }

        /// <summary>
        /// Tests that percolation is calculated correctly for a single column.
        /// </summary>
        [TestMethod]
        public void TestPercolationGridVertical1()
        {
            // Check randomly generated grids with one column open
            for (int i = 0; i < COUNT_GRIDS_VERTICAL1; i++)
            {
                // Create a percolation grid with at least 1 row and 1 column
                PercolationGrid grid = new PercolationGrid(
                    RandomGenerator.Next(1, GRID_MAX_ROWS),
                    RandomGenerator.Next(1, GRID_MAX_COLS));

                // Generate a random open column
                int openCol = RandomGenerator.Next(0, grid.Columns);
                for (int row = 0; row < grid.Rows; row++)
                {
                    grid.SetIsOpen(row, openCol, true);
                }

                // Test that grid percolates
                Assert.IsTrue(grid.Percolates());

                // Generate a random block in the column
                int closeRow = RandomGenerator.Next(0, grid.Rows);
                grid.SetIsOpen(closeRow, openCol, false);

                // Test that grid no longer percolates
                Assert.IsFalse(grid.Percolates());

                // Remove blockage from column
                grid.SetIsOpen(closeRow, openCol, true);

                // Test that grid percolates again
                Assert.IsTrue(grid.Percolates());
            }
        }

        /// <summary>
        /// Tests that percolation is calculated correctly for two columns with a connection.
        /// </summary>
        [TestMethod]
        public void TestPercolationGridVertical2()
        {
            // Check randomly generated grids with two half-columns open with a connection
            for (int i = 0; i < COUNT_GRIDS_VERTICAL2; i++)
            {
                // Create a percolation grid with at least 3 rows and 1 column
                PercolationGrid grid = new PercolationGrid(
                    RandomGenerator.Next(3, GRID_MAX_ROWS),
                    RandomGenerator.Next(1, GRID_MAX_COLS));

                // Generate a random row for a connection not on top or bottom
                int openRow = RandomGenerator.Next(1, grid.Rows - 1);

                // Generate two random open columns
                int openCol1 = RandomGenerator.Next(0, grid.Columns);
                int openCol2 = RandomGenerator.Next(0, grid.Columns);

                // Switch columns to make them in order
                if (openCol1 > openCol2)
                {
                    int temp = openCol1;
                    openCol1 = openCol2;
                    openCol2 = temp;
                }

                // Calculate column direction and place connection
                for (int col = openCol1; col != openCol2; col++)
                {
                    grid.SetIsOpen(openRow, col, true);
                }

                // Place half-columns
                for (int row = 0; row < openRow; row++)
                {
                    grid.SetIsOpen(row, openCol1, true);
                }
                for (int row = openRow; row < grid.Rows; row++)
                {
                    grid.SetIsOpen(row, openCol2, true);
                }

                // Test that grid percolates
                Assert.IsTrue(grid.Percolates());

                // Generate a random block in the column
                int closeCol = RandomGenerator.Next(openCol1, openCol2 + 1);
                grid.SetIsOpen(openRow, closeCol, false);

                // Test that grid no longer percolates
                Assert.IsFalse(grid.Percolates());

                // Remove blockage from column
                grid.SetIsOpen(openRow, closeCol, true);

                // Test that grid percolates again
                Assert.IsTrue(grid.Percolates());
            }
        }

        /// <summary>
        /// Tests that percolation is calculated correctly for three columns with two connections.
        /// </summary>
        [TestMethod]
        public void TestPercolationGridSquiggle()
        {
            // Check randomly generated grids with a squiggle over three columns
            for (int i = 0; i < COUNT_GRIDS_VERTICAL1; i++)
            {
                // Create a percolation grid with at least 3 rows and 5 columns
                PercolationGrid grid = new PercolationGrid(
                    RandomGenerator.Next(3, GRID_MAX_ROWS),
                    RandomGenerator.Next(5, GRID_MAX_COLS));

                // Generate two random connector rows
                int openRow1 = RandomGenerator.Next(1, grid.Rows - 1);
                int openRow2 = RandomGenerator.Next(1, grid.Rows - 1);

                // Generate three random columns
                int openCol1 = RandomGenerator.Next(0, grid.Columns);
                int openCol2 = RandomGenerator.Next(0, grid.Columns);
                int openCol3 = RandomGenerator.Next(0, grid.Columns);

                // Place connections
                int colDir1 = Math.Sign(openCol2 - openCol1);
                int colDir2 = Math.Sign(openCol3 - openCol2);
                for (int col = openCol1; col != openCol2; col += colDir1)
                {
                    grid.SetIsOpen(openRow1, col, true);
                }
                for (int col = openCol2; col != openCol3; col += colDir2)
                {
                    grid.SetIsOpen(openRow2, col, true);
                }

                // Place columns
                int rowDir = Math.Sign(openRow2 - openRow1);
                for (int row = 0; row != openRow1; row++)
                {
                    grid.SetIsOpen(row, openCol1, true);
                }
                for (int row = openRow1; row != openRow2; row += rowDir)
                {
                    grid.SetIsOpen(row, openCol2, true);
                }
                for (int row = openRow2; row != grid.Rows; row++)
                {
                    grid.SetIsOpen(row, openCol3, true);
                }

                // Test that grid percolates
                Assert.IsTrue(grid.Percolates());

                // Generate a block in the first row of the first column
                grid.SetIsOpen(0, openCol1, false);

                // Test that grid no longer percolates
                Assert.IsFalse(grid.Percolates());

                // Remove blockage from column
                grid.SetIsOpen(0, openCol1, true);

                // Test that grid percolates again
                Assert.IsTrue(grid.Percolates());

                // Generate a block in the last row of the last column
                grid.SetIsOpen(grid.Rows - 1, openCol3, false);

                // Test that grid no longer percolates
                Assert.IsFalse(grid.Percolates());

                // Remove blockage from column
                grid.SetIsOpen(grid.Rows - 1, openCol3, true);

                // Test that grid percolates again
                Assert.IsTrue(grid.Percolates());
            }
        }

        /// <summary>
        /// Tests that the percolation grid randomization produces reasonable cell populations.
        /// </summary>
        [TestMethod]
        public void TestPercolationGridRandom()
        {
            // Create new grid with random dimensions
            PercolationGrid grid = new PercolationGrid(
                RandomGenerator.Next(1, GRID_MAX_ROWS),
                RandomGenerator.Next(1, GRID_MAX_COLS));

            for (double p = 0.0d; p <= 1.0d; p += RANDOM_STEP) {
                // Get randomized probabilty of open cells in grid
                double openRatio = FindOpenRatio(grid, COUNT_GRIDS_RANDOM, p);

                // Check that open ratio is within acceptable theshold
                Assert.IsTrue(openRatio >= (p - RANDOM_THRESHOLD));
                Assert.IsTrue(openRatio <= (p + RANDOM_THRESHOLD));
            }
        }
    }
}
