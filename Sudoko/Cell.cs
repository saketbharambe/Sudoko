namespace Sudoko
{
    internal class Cell
    {
        /// <summary>
        /// X Coordinate.
        /// </summary>
        public int x;
        
        /// <summary>
        /// Y Coordinate.
        /// </summary>
        public int y;

        public Cell(int row, int col)
        {
            this.x = row;
            this.y = col;
        }
    }
}