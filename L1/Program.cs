using System;
using System.IO;

namespace L1
{
    internal class Program
    {
        private readonly string _path = Path.Combine("Assets", "data.xml");
        private readonly string _selectedVoivodeship;

        private Program(string selectedVoivodeship)
        {
            _selectedVoivodeship = selectedVoivodeship;
        }

        public static void Main(string[] args)
        {
            if (args.Length < 1)
                ExitWithError("Nie podano województwa.");

            new Program(args[0]).Run();
        }

        private void Run()
        {
            WarehousesDataReader[] readers =
            {
                new DomReader(_selectedVoivodeship),
                new SaxReader(_selectedVoivodeship),
                new XPathReader(_selectedVoivodeship)
            };

            foreach (var reader in readers)
                PresentWarehousesDataReader(reader);
        }

        private void PresentWarehousesDataReader(WarehousesDataReader reader)
        {
            var data = reader.ReadXmlFile(_path);

            Console.WriteLine("XML loaded with {0} approach", reader.ExtractionApproach);
            Console.WriteLine("Opole: {0} aktywnych", data.OpoleActiveCount);
            Console.WriteLine("{0}: {1} nieaktywnych, {2} aktywnych", data.Voivodeship,
                data.VoivodeshipActiveCount, data.VoivodeshipInactiveCount);
            Console.WriteLine("Najwięcej aktywnych: {0}, najwięcej nieaktywnych: {1}",
                data.VoivodeshipWithLargestActiveCount, data.VoivodeshipWithLargestInactiveCount);

            Console.WriteLine("Ranking aktywnych:");
            foreach (var entry in data.ThreeVoivodeshipsWithLargestActiveCount)
                Console.WriteLine("\t{0} ({1})", entry.Key, entry.Value);

            Console.WriteLine("Ranking nieaktywnych:");
            foreach (var entry in data.ThreeVoivodeshipsWithLargestInactiveCount)
                Console.WriteLine("\t{0} ({1})", entry.Key, entry.Value);

            Console.WriteLine("");
        }

        private static void ExitWithError(string message)
        {
            Console.Error.WriteLine(message);
            Environment.Exit(1);
        }
    }
}