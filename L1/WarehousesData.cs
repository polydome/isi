using System.Collections.Generic;
using System.Linq;

namespace L1
{
    public class WarehousesData
    {
        private int _opoleActiveCount;
        private Dictionary<string, int> _voivodeshipActiveCount = new Dictionary<string, int>();
        private Dictionary<string, int> _voivodeshipInactiveCount = new Dictionary<string, int>();

        public WarehousesData(string voivodeship)
        {
            Voivodeship = voivodeship;
        }

        public string Voivodeship { get; }

        public int VoivodeshipActiveCount => _voivodeshipActiveCount.ContainsKey(Voivodeship)
            ? _voivodeshipActiveCount[Voivodeship]
            : 0;

        public int VoivodeshipInactiveCount => _voivodeshipInactiveCount.ContainsKey(Voivodeship)
            ? _voivodeshipInactiveCount[Voivodeship]
            : 0;

        public string VoivodeshipWithLargestActiveCount =>
            _voivodeshipActiveCount.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

        public string VoivodeshipWithLargestInactiveCount =>
            _voivodeshipActiveCount.Aggregate((x, y) => x.Value > y.Value ? y : x).Key;

        public int OpoleActiveCount => _opoleActiveCount;

        public void IncludeWarehouse(Warehouse warehouse)
        {
            if (warehouse.City == "opole" && warehouse.Status == "aktywna")
                _opoleActiveCount++;

            if (warehouse.Status == "aktywna")
                IncrementVoivodeshipActiveCount(warehouse.Voivodeship);
            else
                IncrementVoivodeshipInactiveCount(warehouse.Voivodeship);
        }

        private void IncrementVoivodeshipActiveCount(string voivodeship)
        {
            _voivodeshipActiveCount.TryGetValue(voivodeship, out var value);
            _voivodeshipActiveCount[voivodeship] = value + 1;
        }

        private void IncrementVoivodeshipInactiveCount(string voivodeship)
        {
            _voivodeshipInactiveCount.TryGetValue(voivodeship, out var value);
            _voivodeshipInactiveCount[voivodeship] = value + 1;
        }
    }
}