using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace L2
{
    internal class Program
    {
        public static void Main()
        {
            var jsonPath = Path.Combine("Assets", "data.json");
            var jsonString = File.ReadAllText(jsonPath);

            var busesDatabase = JsonSerializer.Deserialize<Root>(jsonString);
            var buses = busesDatabase.results;

            Console.WriteLine($"Nazwa bazy: {busesDatabase.description.title}");
            Console.WriteLine($"Liczba elementów w bazie: {busesDatabase.count}");

            var busesOwnedBySpecifiedOperatorCount =
                buses.Count(bus => Normalize(bus.operator_przewoznik) == "gdańskie autobusy i tramwaje");
            var historicBusesCount = buses.Count(bus => Normalize(bus.pojazd_zabytkowy) == "tak");
            var busesAssembledAfterYear2000Count = buses.Count(bus => int.Parse(bus.rok_produkcji) > 2000);
            var averageStandingPlacesCount = buses.Average(bus => int.Parse(bus.liczba_miejsc_stojacych));
            var averageSittingPlacesCount = buses.Average(bus => int.Parse(bus.liczba_miejsc_siedzacych));

            Console.WriteLine(
                $"Liczba autobusów przynależnych do przewoźnika Gdańskie Autobusy i Tramwaje: {busesOwnedBySpecifiedOperatorCount}");
            Console.WriteLine($"Liczba zabytkowych autobusów: {historicBusesCount}");
            Console.WriteLine($"Liczba busuów wyprodukowanych po roku 2000: {busesAssembledAfterYear2000Count}");
            Console.WriteLine($"Średnia liczba miejsc stojących w autobusach: {averageStandingPlacesCount}");
            Console.WriteLine($"Średnia liczba miejsc siedzących w autobusach: {averageSittingPlacesCount}");
            
            // run c# serializer
            Console.WriteLine("Serializacja danych z poziomu c# do JSON");
            string jsonSavePath = Path.Combine("Assets", "data2.json");
            JsonCustomSerialize.Run(buses, jsonSavePath);
            JsonPythonDeserialize.Run();
        }

        private static string Normalize(string value)
        {
            return value.ToLower().Trim();
        }
    }
}