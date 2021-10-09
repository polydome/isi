namespace L1
{
    public interface IWarehousesReader
    {
        void LoadXmlFile(string path);
        WarehousesBreakdown ReadWarehousesBreakdownForVoivodeship(string voivodeship);
    }
}