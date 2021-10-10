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
            this._selectedVoivodeship = selectedVoivodeship;
        }

        public static void Main(string[] args)
        {
            if (args.Length < 1)
                ExitWithError("Nie podano województwa.");
            
            new Program(selectedVoivodeship: args[0]).Run();
        }

        private void Run()
        {
            WarehousesDataReader[] readers =
            {
                new DomWarehousesDataReader(_selectedVoivodeship),
                new SaxWarehousesDataReader(_selectedVoivodeship),
            };
            
            foreach (var reader in readers)
                PresentWarehousesDataReader(reader);
        }

        private void PresentWarehousesDataReader(WarehousesDataReader reader)
        {
            WarehousesData data = reader.ReadXmlFile(_path);
            
            Console.WriteLine("XML loaded with {0} approach", reader.ExtractionApproach);
            Console.WriteLine("Opole: {0} aktywnych", data.OpoleActiveCount);
            Console.WriteLine("{0}: {1} nieaktywnych, {2} aktywnych", data.Voivodeship,
                data.VoivodeshipActiveCount, data.VoivodeshipInactiveCount);
            Console.WriteLine("");
        }

        private static void ExitWithError(string message)
        {
            Console.Error.WriteLine(message);
            Environment.Exit(1);
        }
    }
}