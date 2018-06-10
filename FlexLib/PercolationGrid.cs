using System;

namespace FlexLib
{
    /// <summary>
    /// Implements a 2D grid of cells that flows from any cell at the top to the bottom.
    /// The system percolates if there is a path of open cells connecting the top and bottom.
    /// </summary>
    public class PercolationGrid
    {
        public readonly int Rows;       // Rows in grid
        public readonly int Columns;    // Columns in grid

        private readonly bool[,] IsOpen;    // Stores whether each cell is open or not
        private readonly int[,] IsFilled;   // Stores whether each cell connects to the top and the direction

        /// <summary>
        /// Creates a new grid with all cells initially closed.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="cols">The number of columns in the grid.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when number or rows or columns is negative.</exception>
        public PercolationGrid(int rows, int cols)
        {
            // Verify that dimensions are non-negative.
            if (rows < 0)
                throw new ArgumentOutOfRangeException("rows", rows, "The number of rows cannot be negative.");
            if (cols < 0)
                throw new ArgumentOutOfRangeException("cols", cols, "The number of columns cannot be negative.");

            // Store dimensions
            Rows = rows;
            Columns = cols;

            // Setup arrays for future use
            IsOpen = new bool[rows, cols];
            IsFilled = new int[rows, cols];
        }

        /// <summary>
        /// Gets the open state of a cell.
        /// </summary>
        /// <param name="i">The row index.</param>
        /// <param name="j">The column index.</param>
        /// <returns>Is the cell open.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if row index or column index is not in valid range.</exception>
        public bool GetIsOpen(int i, int j)
        {
            // Check to make sure row and column indices are in correct range
            if (i < 0 || i >= Rows)
                throw new ArgumentOutOfRangeException("i", i, "Row index is out of range.");
            if (j < 0 || j >= Columns)
                throw new ArgumentOutOfRangeException("j", j, "Column index is out of range.");

            return IsOpen[i, j];
        }
        /// <summary>
        /// Sets the open state of a cell.
        /// </summary>
        /// <param name="i">The row index.</param>
        /// <param name="j">The column index.</param>
        /// <param name="isOpen">Is the cell open.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if row index or column index is not in valid range.</exception>
        public void SetIsOpen(int i, int j, bool isOpen)
        {
            // Check to make sure row and column indices are in correct range
            if (i < 0 || i >= Rows)
                throw new ArgumentOutOfRangeException("i", i, "Row index is out of range.");
            if (j < 0 || j >= Columns)
                throw new ArgumentOutOfRangeException("j", j, "Column index is out of range.");

            // Change is open state and update cell
            IsOpen[i, j] = isOpen;
            UpdateCell(i, j);
        }
        
        /// <summary>
        /// Randomizes the open and closed state of all cells in the grid.
        /// </summary>
        /// <param name="probability">The probability that a cell will be open.</param>
        public void Randomize(double probability)
        {
            // Make each individual cell be randomly open or closed
            Random random = new Random();
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    IsOpen[i, j] = random.NextDouble() < probability;

            // Clear and update fill data for board after all open calculations
            UpdateGrid();
        }

        /// <summary>
        /// Tests if the grid has an open path of cells from top to bottom.
        /// </summary>
        /// <returns>Is there a open connecting path.</returns>
        public bool Percolates()
        {
            // Checks if any of the bottom cells are filled
            for (int j = 0; j < Columns; j++)
                if (IsFilled[Rows - 1, j] != 0)
                    return true;
            return false;
        }

        /// <summary>
        /// Updates the entire grid with flow.
        /// </summary>
        private void UpdateGrid()
        {
            // Reset entire grid to be unfilled
            for (int j = 0; j < Columns; j++)
                for (int i = 0; i < Rows; i++)
                    IsFilled[i, j] = 0;

            // Update only the cells connected to the top
            // All other cells will be calculated indirectly if they are connected to top
            for (int j = 0; j < Columns; j++)
                if (IsOpen[0, j])
                    UpdateCell(0, j);
        }

        /// <summary>
        /// Determines the minimum value of flow in surrounding cells.
        /// </summary>
        /// <param name="i">The cell row.</param>
        /// <param name="j">The cell column.</param>
        /// <returns>The surrounding minimum flow value.</returns>
        private int PercolateValueNeighbor(int i, int j)
        {
            // Determines the minimum flow value of all neighbors that is not zero
            int percolateVal = int.MaxValue;
            if (i - 1 >= 0 && IsFilled[i - 1, j] != 0 && IsFilled[i - 1, j] < percolateVal)
                percolateVal = IsFilled[i - 1, j];
            if (i + 1 < Rows && IsFilled[i + 1, j] != 0 && IsFilled[i + 1, j] < percolateVal)
                percolateVal = IsFilled[i + 1, j];
            if (j - 1 >= 0 && IsFilled[i, j - 1] != 0 && IsFilled[i, j - 1] < percolateVal)
                percolateVal = IsFilled[i, j - 1];
            if (j + 1 < Columns && IsFilled[i, j + 1] != 0 && IsFilled[i, j + 1] < percolateVal)
                percolateVal = IsFilled[i, j + 1];

            // Returns minimum flow value or zero if all surrounding cells are not filled
            return percolateVal == int.MaxValue ? 0 : percolateVal;
        }

        /// <summary>
        /// Updates all filled neighbors surrounding a cell.
        /// </summary>
        /// <param name="i">The cell row.</param>
        /// <param name="j">The cell column.</param>
        private void UpdateFilledNeighbors(int i, int j)
        {
            // Updates all valid, open, and filled neighbors in cardinal directions.
            if (i - 1 >= 0 && IsOpen[i - 1, j] && IsFilled[i - 1, j] > 1)
                UpdateCell(i - 1, j);
            if (i + 1 < Rows && IsOpen[i + 1, j] && IsFilled[i + 1, j] > 1)
                UpdateCell(i + 1, j);
            if (j - 1 >= 0 && IsOpen[i, j - 1] && IsFilled[i, j - 1] > 1)
                UpdateCell(i, j - 1);
            if (j + 1 < Columns && IsOpen[i, j + 1] && IsFilled[i, j + 1] > 1)
                UpdateCell(i, j + 1);
        }
        /// <summary>
        /// Updates all unfilled neighbors surrounding a cell.
        /// </summary>
        /// <param name="i">The cell row.</param>
        /// <param name="j">The cell column.</param>
        private void UpdateUnfilledNeighbors(int i, int j)
        {
            // Updates all valid, open, and unfilled neighbors in cardinal directions.
            if (i - 1 >= 0 && IsOpen[i - 1, j] && IsFilled[i - 1, j] == 0)
                UpdateCell(i - 1, j);
            if (i + 1 < Rows && IsOpen[i + 1, j] && IsFilled[i + 1, j] == 0)
                UpdateCell(i + 1, j);
            if (j - 1 >= 0 && IsOpen[i, j - 1] && IsFilled[i, j - 1] == 0)
                UpdateCell(i, j - 1);
            if (j + 1 < Columns && IsOpen[i, j + 1] && IsFilled[i, j + 1] == 0)
                UpdateCell(i, j + 1);
        }

        /// <summary>
        /// Updates a specified cell based on surrounding flow.
        /// </summary>
        /// <param name="i">The cell row.</param>
        /// <param name="j">The cell column.</param>
        private void UpdateCell(int i, int j)
        {
            // If cell is open and at top of grid, make it filled
            // If cell is closed, make it unfilled
            bool open = IsOpen[i, j];
            if (open)
            {
                if (i == 0)
                    IsFilled[i, j] = 1;
            }
            else
            {
                // Filled neighbors should be updated to unfill neighbors with no flow
                IsFilled[i, j] = 0;
                UpdateFilledNeighbors(i, j);
            }

            // Calculate the flow of neighbors
            int neighborVal = PercolateValueNeighbor(i, j);
            int percolateVal = IsFilled[i, j];

            // If cell is not at top and filled and there are neighbors with no or reliant flow, unfill cell and neighbors
            // If cell is filled, fill neighbors
            // If cell is open and has filled neighbors, fill cell and neighbors
            if (percolateVal > 1 && (neighborVal == 0 || neighborVal > percolateVal))
            {
                IsFilled[i, j] = 0;
                UpdateFilledNeighbors(i, j);
            }
            else if (percolateVal != 0)
            {
                UpdateUnfilledNeighbors(i, j);
            }
            else if (open && neighborVal != 0)
            {
                IsFilled[i, j] = neighborVal + 1;
                UpdateUnfilledNeighbors(i, j);
            }
        }
    }
}
