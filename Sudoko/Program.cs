using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoko
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoko sudoko = new Sudoko();
            ISudokoScanner sudokoScanner = Factory.GetISudokoScanner();
            sudoko.Initialize(sudokoScanner.GetInitialPuzzle());
            ISudokoPrinter sudokoPrinter = Factory.GetISudokoPrinter();
            sudokoPrinter.PrintSudoko(sudoko.GetSolution());
        }
    }
}
