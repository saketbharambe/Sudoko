using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Sudoko
{
    internal class FileSudokoScanner : ISudokoScanner
    {
        /// <summary>
        /// Name of the config which stores file path
        /// </summary>
        const string InputFileConfigName = "InputFilePath";

        /// <summary>
        /// Stores the path of the input file.
        /// </summary>
        private string InputFilePath;

        public FileSudokoScanner() 
        {
            this.InputFilePath = ConfigurationManager.AppSettings.Get(InputFileConfigName);
        }

        public int[][] GetInitialPuzzle()
        {
            List<int[]> data = new List<int[]>();
            string[] lines = File.ReadAllLines(this.InputFilePath);
            foreach(string line in lines)
            {
                string[] tokens = line.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                int[] row = new int[tokens.Length];
                for (int i = 0; i < tokens.Length; i ++)
                {
                    row[i] = int.Parse(tokens[i]);
                }

                data.Add(row);
            }

            return data.ToArray();
        }
    }
}