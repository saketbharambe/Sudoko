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
        List<List<List<int>>> Candidates = null;

        internal void Initialize(int[][] data)
        {
            Contract.Assert(data.Length == 9, "Sudoko Data needs to be of length=9");
            this.Candidates = new List<List<List<int>>>();
            for (int row = 0; row < 9; row++)
            {
                Contract.Assert(data[row].Length == 9, string.Format($"Data Row={row} does not have 9 elements"));
                this.Candidates.Add(new List<List<int>>());

                for (int col = 0; col < 9; col++)
                {
                    Contract.Assert(data[row][col] >= 0, string.Format($"Data[{row}][{col}] is not greater than or equal to 0"));
                    Contract.Assert(data[row][col] <= 9, string.Format($"Data[{row}][{col}] is not less than or equal to 9"));

                    this.Candidates[row].Add(new List<int>());
                    if (data[row][col] == 0)
                    {
                        this.Candidates[row][col] = Enumerable.Range(1, 9).ToList();
                    }
                }
            }

            this.Data = data;
            Validate();
        }

        internal void Solve()
        {
            while (IsCompleted())
            {
                ScratchCandidates();
                ConsolidateCells();
                OnlyPossibilityCandidate();
            }
        }

        void OnlyPossibilityCandidate()
        {
            List<Cell> group = new List<Cell>();
            for (int col = 0; col < 9; col++)
            {
                group.Clear();
                for (int i = 0; i < 9; i++)
                {
                    group.Add(new Cell(i, col));
                }

                MarkMissingCandidateInTheGroup(group);
            }

            for (int row = 0; row < 9; row++)
            {
                group.Clear();
                for (int j = 0; j < 9; j++)
                {
                    group.Add(new Cell(row, j));
                }

                MarkMissingCandidateInTheGroup(group);
            }

            for (int blockRow = 0; blockRow < 9; blockRow += 3)
            {
                for (int blockCol = 0; blockCol < 9; blockCol += 3)
                {
                    group.Clear();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            group.Add(new Cell(blockRow + i, blockCol + j));
                        }
                    }

                    MarkMissingCandidateInTheGroup(group);
                }
            }
        }

        /// <summary>
        /// If a value can occur only in one of the cells of this group, mark it there.
        /// </summary>
        /// <param name="group"></param>
        void MarkMissingCandidateInTheGroup(List<Cell> group)
        {
            Contract.Assert(group.Count == 9, "A Group in sudoko has 9 cells");
            for (int val = 1; val <= 9; val ++)
            {
                bool valPresent = false;
                int numCandidates = 0;
                Cell possibleCell = null;
                foreach(Cell cell in group)
                {
                    if (this.Data[cell.x][cell.y] == val)
                    {
                        valPresent = true;
                    }

                    if (this.Data[cell.x][cell.y] == 0 && this.Candidates[cell.x][cell.y].Contains(val))
                    {
                        numCandidates++;
                        possibleCell = cell;
                    }
                }

                if (valPresent)
                {
                    continue;
                }

                if (numCandidates == 1)
                {
                    this.Data[possibleCell.x][possibleCell.y] = val;
                    this.Candidates[possibleCell.x][possibleCell.y].Clear();
                }
            }
        }

        bool ConsolidateCells()
        {
            bool progressMade = false;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.Data[i][j] == 0 && this.Candidates[i][j].Count == 1)
                    {
                        this.Data[i][j] = this.Candidates[i][j][0];
                        progressMade = true;
                    }
                }
            }

            return progressMade;
        }

        void ScratchCandidates()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.Data[i][j] != 0)
                    {
                        ScratchCandidates(i, j, this.Data[i][j]);
                    }
                }
            }
        }

        /// <summary>
        /// Removes val as candidate from respective 3 groups
        /// </summary>
        void ScratchCandidates(int row, int col, int val)
        {
            for (int i = 0; i < 9; i++)
            {
                this.Candidates[i][col].Remove(val);
            }

            for (int j = 0; j < 9; j++)
            {
                this.Candidates[row][j].Remove(val);
            }

            int blockRow = (row / 3) * 3;
            int blockCol = (col / 3) * 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.Candidates[blockRow + i][blockCol + j].Remove(val);
                }
            }
        }

        internal bool IsCompleted()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.Data[i][j] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        internal bool Validate()
        {
            int[] Counts = new int[10];
            for (int row = 0; row < 9; row++)
            {
                Array.Clear(Counts, 0, 9);
                for (int j = 0; j < 9; j++)
                {
                    Counts[this.Data[row][j]]++;
                }

                for (int val = 1; val <= 9; val++)
                {
                    if (Counts[val] > 1)
                    {
                        return false;
                    }
                }
            }

            for (int col = 0; col < 9; col++)
            {
                Array.Clear(Counts, 0, 9);
                for (int i = 0; i < 9; i++)
                {
                    Counts[this.Data[i][col]]++;
                }

                for (int val = 1; val <= 9; val++)
                {
                    if (Counts[val] > 1)
                    {
                        return false;
                    }
                }
            }

            for (int rowStart = 0; rowStart < 9; rowStart += 3)
            {
                for (int colStart = 0; colStart < 9; colStart += 3)
                {
                    Array.Clear(Counts, 0, 9);
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Counts[this.Data[rowStart + i][colStart + j]]++;
                        }
                    }

                    for (int val = 1; val <= 9; val++)
                    {
                        if (Counts[val] > 1)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
