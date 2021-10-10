namespace L1
{
    public abstract class WarehousesDataReader
    {
        protected string Voivodeship;

        protected WarehousesDataReader(string voivodeship, string extractionApproach)
        {
            Voivodeship = voivodeship;
            ExtractionApproach = extractionApproach;
        }

        public abstract WarehousesData ReadXmlFile(string path);

        public string ExtractionApproach { get; }
    }
}