using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace L1
{
    public class XPathReader : WarehousesDataReader
    {
        private const string NamespacePrefix = "ns0";
        private const string NamespaceUri = "http://rejestrymedyczne.csioz.gov.pl/rhf/eksport-danych-v1.0";

        private const string ActiveWarehousesInOpole =
            "/ns0:Hurtownie/ns0:Hurtownia[@status='Aktywna' and ./ns0:Adres[@miejscowosc='Opole']]";

        private const string AllWarehouses = "/ns0:Hurtownie/ns0:Hurtownia";
        private const string WarehouseVoivodeship = "string(ns0:Adres/@wojewodztwo)";

        public XPathReader(string voivodeship) : base(voivodeship, "XPath")
        {
        }

        public override WarehousesData ReadXmlFile(string path)
        {
            var navigator = new XPathDocument(path).CreateNavigator();
            var namespaceManager = BuildNamespaceManager(navigator.NameTable);

            var allWarehouses = navigator.Select(AllWarehouses, namespaceManager);

            var voivodeshipActiveCount = new Dictionary<string, int>();
            var voivodeshipInactiveCount = new Dictionary<string, int>();

            foreach (XPathNavigator warehouse in allWarehouses)
            {
                var rawVoivodeship = warehouse.Evaluate(WarehouseVoivodeship, namespaceManager);
                var voivodeship = (rawVoivodeship as string)?.ToLower();
                var status = warehouse.GetAttribute("status", "").ToLower();

                var dictionary = status == "aktywna" ? voivodeshipActiveCount : voivodeshipInactiveCount;

                dictionary.TryGetValue(voivodeship, out var value);
                dictionary[voivodeship] = value + 1;
            }

            return new WarehousesData(
                voivodeship: Voivodeship,
                opoleActiveCount: navigator.Select(ActiveWarehousesInOpole, namespaceManager).Count,
                voivodeshipActiveCount: voivodeshipActiveCount,
                voivodeshipInactiveCount: voivodeshipInactiveCount);
        }

        private static XmlNamespaceManager BuildNamespaceManager(XmlNameTable nameTable)
        {
            var manager = new XmlNamespaceManager(nameTable);
            manager.AddNamespace(NamespacePrefix, NamespaceUri);

            return manager;
        }
    }
}