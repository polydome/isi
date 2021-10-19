using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace L2
{
    public class JsonCustomSerialize
    {
        public static void Run(IEnumerable<Result> busesCollection, string jsonOutPath)
        {

            var buses = busesCollection.Select(bus => new Bus
            {
                nr_inwentarzowy = bus.nr_inwentarzowy,
                udogodnienia = bus.ExtractFacilities(),
                danePodstawowe = bus.ExtractBasics()
            });
            
            var customBusesDatabase = new BusesCondensed
            {
                buses = buses.ToArray()
            };

            var a =customBusesDatabase.buses.Where(bus => bus.udogodnienia.klimatyzacja == "nie").Select(bus => bus.danePodstawowe.rok_produkcji);
                
            var jsonOut = JsonSerializer.Serialize(customBusesDatabase);
            File.WriteAllText(jsonOutPath, jsonOut);
        }
    }

    public static class BusConverters
    {
        internal static Facilities ExtractFacilities(this Result bus) => new Facilities
        {
            biletomat = bus.biletomat,
            drzwi_pasazerskie = bus.drzwi_pasazerskie,
            klimatyzacja = bus.klimatyzacja,
            mocowanie_rowerow = bus.mocowanie_rowerow,
            monitoring = bus.monitoring,
            przyklek = bus.przyklek,
            rampa_dla_wozkow = bus.rampa_dla_wozkow,
            wysokosc_podlogi = bus.wysokosc_podlogi,
            zapowiedzi_glosowe = bus.zapowiedzi_glosowe,
            AED = bus.AED,
            USB = bus.USB
        };

        internal static Basic ExtractBasics(this Result bus) => new Basic
        {
            dlugosc = bus.dlugosc,
            liczba_miejsc_siedzacych = bus.liczba_miejsc_siedzacych,
            liczba_miejsc_stojacych = bus.liczba_miejsc_stojacych,
            marka = bus.marka,
            model = bus.model,
            operator_przewoznik = bus.operator_przewoznik,
            pojazd_dwukierunkowy = bus.pojazd_dwukierunkowy,
            rodzaj_pojazdu = bus.rodzaj_pojazdu,
            rok_produkcji = bus.rok_produkcji,
            typ_pojazdu = bus.typ_pojazdu
        };
    }
}