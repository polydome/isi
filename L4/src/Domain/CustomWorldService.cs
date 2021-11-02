using System.Collections.Generic;
using L4.Data;

namespace L4.Domain
{
    public class CustomWorldService
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

        public IEnumerable<DataCustomWorld> RepopulateCustomWorldDatabase()
        {
            var countries = _dataWorldRepository.findAll();
            var reports = _raportWhrRepository.findAll();

            Dictionary<string, DataCustomWorld> countryNameToCustomWorld = new();

            foreach (var country in countries)
                countryNameToCustomWorld[country.Name] = DataCustomWorld.FromCountryData(country);

            foreach (var report in reports)
                if (countryNameToCustomWorld.ContainsKey(report.Name))
                    countryNameToCustomWorld[report.Name].FillWhrData(report);

            var records = countryNameToCustomWorld.Values;
            _customWorldRepository.replaceAll(records);

            return records;
        }
    }
}