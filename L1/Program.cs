using System;
using System.IO;

namespace L1
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
                ExitWithError("Nie podano województwa.");

            string path = Path.Combine("Assets", "data.xml");
            IWarehousesReader reader = new DomWarehousesReader();
            reader.LoadXmlFile(path);
            var targetVoivodeship = args[0];
            var breakdown = reader.ReadWarehousesBreakdownForVoivodeship(targetVoivodeship);

            Console.WriteLine("{0}: {1} nieaktywnych, {2} aktywnych", targetVoivodeship, breakdown.ActiveWarehousesCount,
                breakdown.InactiveWarehousesCount);
        }

        private static void ExitWithError(string message)
        {
            Console.Error.WriteLine(message);
            Environment.Exit(1);
        }
    }
}