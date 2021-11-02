using System.Collections.Generic;
using System.Data;
using L4.Domain;

namespace L4.Data
{
    public class RaportWHRRepository
    {
        private const string SelectAllWhrRaports =
            "SELECT CountryName, LadderScore, Freedom, LoggedGDPPerCapita FROM whr.raport";

        private readonly Database _database;

        public RaportWHRRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<RaportWHR> findAll()
        {
            return _database.RetrieveData(SelectAllWhrRaports, ParseWhrRecord);
        }

        private static RaportWHR ParseWhrRecord(IDataRecord record)
        {
            return new()
            {
                Name = record.GetString(0),
                LadderScore = record.GetFloat(1),
                FreedomOfChoices = record.GetFloat(2),
                GDPperCapita = record.GetFloat(3)
            };
        }
    }
}