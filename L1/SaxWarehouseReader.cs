using System.Xml;

namespace L1
{
    public class SaxWarehousesDataReader : WarehousesDataReader
    {
        private readonly XmlReaderSettings _settings = BuildReaderSettings();

        public SaxWarehousesDataReader(string voivodeship) : base(voivodeship, "SAX")
        {
        }

        public override WarehousesData ReadXmlFile(string path)
        {
            var data = new WarehousesData(Voivodeship);
            var reader = XmlReader.Create(path, _settings);

            reader.MoveToContent();
            while (reader.Read())
            {
                if (!reader.IsAtWarehouseNode()) continue;
                var warehouse = reader.ReadWarehouse();
                data.IncludeWarehouse(warehouse);
            }

            reader.Close();

            return data;
        }

        private static XmlReaderSettings BuildReaderSettings() => new XmlReaderSettings
        {
            IgnoreComments = true,
            IgnoreProcessingInstructions = true,
            IgnoreWhitespace = true
        };
    }

    internal static partial class XmlWarehouseExtension
    {
        public static bool IsAtWarehouseNode(this XmlReader reader) => reader.NodeType == XmlNodeType.Element &&
                                                                       reader.Name == "Hurtownia";

        public static Warehouse ReadWarehouse(this XmlReader reader)
        {
            var status = reader.GetAttribute("status")?.ToLower();
            reader.Read();
            var city = reader.GetAttribute("miejscowosc")?.ToLower();
            var voivodeship = reader.GetAttribute("wojewodztwo")?.ToLower();
            return new Warehouse(voivodeship, city, status);
        }
    }
}