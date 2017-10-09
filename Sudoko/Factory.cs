using System;
using System.Configuration;
using System.Reflection;

namespace Sudoko
{
    internal class Factory
    {
        private const string ISudokoScannerConfigName = "ISudokoScanner";

        internal static ISudokoScanner GetISudokoScanner()
        {
            string sudokoScanner = ConfigurationManager.AppSettings.Get(ISudokoScannerConfigName);
            if (sudokoScanner == typeof(FileSudokoScanner).Name)
            {
                return new FileSudokoScanner();
            }

            return null;
        }

        internal static ISudokoPrinter GetISudokoPrinter()
        {
            return new ConsoleSudokoPrinter();
        }
    }
}