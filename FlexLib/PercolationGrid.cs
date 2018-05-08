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

        public bool this[int i, int j]
        {
            get { return false; }
            set { }
        }

        public PercolationGrid(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
        }

        public bool Evaluate()
        {
            return false;
        }
    }
}
