using System.Collections.Generic;
using System.Linq;
using L4.Data;

namespace L4.Domain
{
    public partial class CustomWorldService
    {
        private readonly CustomWorldRepository _customWorldRepository;
        private readonly DataWorldRepository _dataWorldRepository;
        private readonly RaportWHRRepository _raportWhrRepository;

        public CustomWorldService(DataWorldRepository dataWorldRepository, RaportWHRRepository raportWhrRepository,
            CustomWorldRepository customWorldRepository)
        {
            _dataWorldRepository = dataWorldRepository;
            _raportWhrRepository = raportWhrRepository;
            _customWorldRepository = customWorldRepository;
        }

        public IEnumerable<DataCustomWorld> RepopulateCustomWorldDatabase(ThresholdSettings thresholdSettings)
        {
            var countries = _dataWorldRepository.FindAll(thresholdSettings);
            var reports = _raportWhrRepository.FindAll(thresholdSettings);

            Dictionary<string, DataCustomWorld> countryNameToCustomWorld = new();

            foreach (var report in reports)
                countryNameToCustomWorld[report.Name] = DataCustomWorld.FromReport(report);

            foreach (var country in countries)
                if (countryNameToCustomWorld.ContainsKey(country.Name))
                    countryNameToCustomWorld[country.Name].FillCountryData(country);

            // Omits WHR entries with incomplete country data
            var records = countryNameToCustomWorld.Values
                .Where(country => country.Code.Length > 0)
                .ToList();
            
            _customWorldRepository.replaceAll(records);

            return records;
        }
    }
}