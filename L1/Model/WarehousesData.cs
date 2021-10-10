using System.Collections.Generic;
using System.Linq;

namespace L1.Model
{
    public class WarehousesData
    {
        private readonly Dictionary<string, int> _voivodeshipActiveCount;
        private readonly Dictionary<string, int> _voivodeshipInactiveCount;

        public WarehousesData(int opoleActiveCount, string voivodeship, Dictionary<string, int> voivodeshipActiveCount,
            Dictionary<string, int> voivodeshipInactiveCount)
        {
            OpoleActiveCount = opoleActiveCount;
            Voivodeship = voivodeship;
            _voivodeshipActiveCount = voivodeshipActiveCount;
            _voivodeshipInactiveCount = voivodeshipInactiveCount;
        }

        public WarehousesData(string voivodeship)
        {
            Voivodeship = voivodeship;
            _voivodeshipActiveCount = new Dictionary<string, int>();
            _voivodeshipInactiveCount = new Dictionary<string, int>();
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

        public int OpoleActiveCount { get; private set; }

        public IEnumerable<KeyValuePair<string, int>> ThreeVoivodeshipsWithLargestActiveCount =>
            _voivodeshipActiveCount
                .OrderByDescending(x => x.Value)
                .Take(3);

        public IEnumerable<KeyValuePair<string, int>> ThreeVoivodeshipsWithLargestInactiveCount =>
            _voivodeshipInactiveCount
                .OrderByDescending(x => x.Value)
                .Take(3);

        public void IncludeWarehouse(Warehouse warehouse)
        {
            if (warehouse.City == "opole" && warehouse.Status == "aktywna")
                OpoleActiveCount++;

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