namespace Sudoko
{
    internal interface ISudokoScanner
    {
        /// <summary>
        /// Scans the Puzzle and returns a 9x9 int matrix.
        /// 0 means missing number.
        /// All numbers have to be in range [0-9]
        /// </summary>
        /// <returns></returns>
        int[][] GetInitialPuzzle();
    }
}