using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexLib
{
    public class PercolationGrid
    {
        public readonly int Rows;
        public readonly int Columns;

        private bool[,] OpenData;
        private int[,] PercolateData;

        public bool this[int i, int j]
        {
            get { return OpenData[i, j]; }
            set
            {
                OpenData[i, j] = value;
                UpdateCell(i, j);
            }
        }

        public PercolationGrid(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;

            OpenData = new bool[rows, cols];
            PercolateData = new int[rows, cols];
        }

        private int PercolateValueNeighbor(int i, int j)
        {
            int percolateVal = int.MaxValue;
            if (i - 1 >= 0 && PercolateData[i - 1, j] != 0 && PercolateData[i - 1, j] < percolateVal)
                percolateVal = PercolateData[i - 1, j];
            if (i + 1 < Rows && PercolateData[i + 1, j] != 0 && PercolateData[i + 1, j] < percolateVal)
                percolateVal = PercolateData[i + 1, j];
            if (j - 1 >= 0 && PercolateData[i, j - 1] != 0 && PercolateData[i, j - 1] < percolateVal)
                percolateVal = PercolateData[i, j - 1];
            if (j + 1 < Columns && PercolateData[i, j + 1] != 0 && PercolateData[i, j + 1] < percolateVal)
                percolateVal = PercolateData[i, j + 1];
            return percolateVal == int.MaxValue ? 0 : percolateVal;
        }

        private void UpdateFilledNeighbors(int i, int j)
        {
            if (i - 1 >= 0 && OpenData[i - 1, j] && PercolateData[i - 1, j] != 0)
                UpdateCell(i - 1, j);
            if (i + 1 < Rows && OpenData[i + 1, j] && PercolateData[i + 1, j] != 0)
                UpdateCell(i + 1, j);
            if (j - 1 >= 0 && OpenData[i, j - 1] && PercolateData[i, j - 1] != 0)
                UpdateCell(i, j - 1);
            if (j + 1 < Columns && OpenData[i, j + 1] && PercolateData[i, j + 1] != 0)
                UpdateCell(i, j + 1);
        }

        private void UpdateUnfilledNeighbors(int i, int j)
        {
            if (i - 1 >= 0 && OpenData[i - 1, j] && PercolateData[i - 1, j] == 0)
                UpdateCell(i - 1, j);
            if (i + 1 < Rows && OpenData[i + 1, j] && PercolateData[i + 1, j] == 0)
                UpdateCell(i + 1, j);
            if (j - 1 >= 0 && OpenData[i, j - 1] && PercolateData[i, j - 1] == 0)
                UpdateCell(i, j - 1);
            if (j + 1 < Columns && OpenData[i, j + 1] && PercolateData[i, j + 1] == 0)
                UpdateCell(i, j + 1);
        }

        private void UpdateCell(int i, int j)
        {
            bool open = OpenData[i, j];

            if (open)
            {
                if (i == 0)
                {
                    PercolateData[i, j] = 1;
                }
            }
            else
            {
                PercolateData[i, j] = 0;
                UpdateFilledNeighbors(i, j);
            }

            int neighborVal = PercolateValueNeighbor(i, j);
            int percolateVal = PercolateData[i, j];

            if (percolateVal > 1 && (neighborVal == 0 || neighborVal > percolateVal))
            {
                PercolateData[i, j] = 0;
                UpdateFilledNeighbors(i, j);
            }
            else if (percolateVal != 0)
            {
                UpdateUnfilledNeighbors(i, j);
            }
            else if (open && neighborVal != 0)
            {
                PercolateData[i, j] = neighborVal + 1;
                UpdateUnfilledNeighbors(i, j);
            }
        }

        public bool Evaluate()
        {
            for (int j = 0; j < Columns; j++)
            {
                if (PercolateData[Rows - 1, j] != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
