using System.Xml;

namespace L1
{
    public class DomWarehousesReader : IWarehousesReader
    {
        private readonly XmlDocument _doc = new XmlDocument();
        
        public void LoadXmlFile(string path)
        {
            _doc.Load(path);
        }

        public WarehousesBreakdown ReadWarehousesBreakdownForVoivodeship(string voivodeship)
        {
            int activeCount = 0;
            int inactiveCount = 0;
            var warehouse = _doc.GetElementsByTagName("Hurtownia");
    
            foreach(XmlNode node in warehouse)
            { 
                string nodeVoivodeship = node.FirstChild.Attributes.GetNamedItem("wojewodztwo").Value.ToLower();
                string nodeStatus = node.Attributes.GetNamedItem("status").Value.ToLower();
                
                if (nodeVoivodeship != voivodeship)
                    continue;
                
                if (nodeStatus == "nieaktywna")
                    inactiveCount++;
                else
                    activeCount++;
            }

            return new WarehousesBreakdown(activeCount, inactiveCount);
        }
    }
}