using System.Xml;
using L1.Model;

namespace L1.Reader
{
    public class DomReader : WarehousesDataReader
    {
        public DomReader(string voivodeship) : base(voivodeship, "DOM")
        {
        }

        public override WarehousesData ReadXmlFile(string path)
        {
            var data = new WarehousesData(Voivodeship);

            var doc = new XmlDocument();
            doc.Load(path);

            var warehouseNodes = doc.GetElementsByTagName("Hurtownia");

            foreach (XmlNode node in warehouseNodes)
            {
                var warehouse = node.ReadWarehouse();
                data.IncludeWarehouse(warehouse);
            }

            return data;
        }
    }

    internal static partial class XmlWarehouseExtension
    {
        public static Warehouse ReadWarehouse(this XmlNode node)
        {
            var voivodeship = node.FirstChild.Attributes.GetNamedItem("wojewodztwo").Value.ToLower();
            var city = node.FirstChild.Attributes.GetNamedItem("miejscowosc").Value.ToLower();
            var status = node.Attributes.GetNamedItem("status").Value.ToLower();
            return new Warehouse(voivodeship, city, status);
        }
    }
}