namespace L1
{
    public abstract class WarehousesDataReader
    {
        protected readonly string Voivodeship;

        protected WarehousesDataReader(string voivodeship, string extractionApproach)
        {
            Voivodeship = voivodeship;
            ExtractionApproach = extractionApproach;
        }

        public string ExtractionApproach { get; }

        public abstract WarehousesData ReadXmlFile(string path);
    }
}