using System;

namespace FlexLib.Linear
{
    public class Matrix<T>
    {
        private T[,] Data;

        public Matrix(int nrow, int ncol)
        {
            if (nrow <= 0) throw new ArgumentOutOfRangeException(nameof(nrow));
            if (ncol <= 0) throw new ArgumentOutOfRangeException(nameof(ncol));

            Data = new T[nrow, ncol];
        }

        public void Fill(int nrow, int ncol, T value)
        {
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++)
                {
                    Data[i, j] = value;
                }
            }
        }
    }
}