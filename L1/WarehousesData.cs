namespace L1
{
    public class WarehousesData
    {
        private int _opoleActiveCount;
        private int _voivodeshipInactiveCount;
        private int _voivodeshipActiveCount;

        public WarehousesData(string voivodeship)
        {
            Voivodeship = voivodeship;
        }

        public string Voivodeship { get; }

        public int VoivodeshipActiveCount => _voivodeshipActiveCount;

        public int VoivodeshipInactiveCount => _voivodeshipInactiveCount;

        public int OpoleActiveCount => _opoleActiveCount;

        public void IncludeWarehouse(Warehouse warehouse)
        {
            if (warehouse.City == "opole" && warehouse.Status == "aktywna")
                _opoleActiveCount++;

            if (warehouse.Voivodeship == Voivodeship)
            {
                if (warehouse.Status == "aktywna")
                    _voivodeshipActiveCount++;
                else
                    _voivodeshipInactiveCount++;
            }
        }
    }
}