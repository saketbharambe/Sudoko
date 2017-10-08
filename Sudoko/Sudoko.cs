using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoko
{
    internal class Sudoko
    {
        /// <summary>
        /// Data of the sudoko.
        /// </summary>
        int[][] Data = null;

        /// <summary>
        /// Candidates for each cell.
        /// </summary>
        List<int>[][] Candidates; 

        internal void Initialize(int[][] data)
        {
            Contract.Assert(data.Length == 9, "Sudoko Data needs to be of length=9");
            for (int row = 0; row < 9; row++)
            {
                Contract.Assert(data[row].Length == 9, string.Format($"Data Row={row} does not have 9 elements"));
                this.Candidates[row] = new List<int>[9];

                for (int col = 0; col < 9; col++)
                {
                    Contract.Assert(data[row][col] >= 0, string.Format($"Data[{row}][{col}] is not greater than or equal to 0"));
                    Contract.Assert(data[row][col] <= 9, string.Format($"Data[{row}][{col}] is not less than or equal to 9"));

                    this.Candidates[row][col] = new List<int>();
                    if (data[row][col] == 0)
                    {
                        this.Candidates[row][col] = Enumerable.Range(1, 9).ToList();
                    }
                }
            }

            this.Data = data;
            Validate();            
        }

        internal bool Validate()
        {
            int[] Counts = new int[9];
            for (int row = 0; row < 9; row ++)
            {
                Array.Clear(Counts, 0, 9);
                for (int j = 0; j < 9; j ++)
                {
                    Counts[this.Data[row][j]]++;
                }

                for (int val = 1; val <= 9; val ++)
                {
                    if (Counts[val] > 1)
                    {
                        return false;
                    }
                }
            }


            return true;
        }
    }
}
