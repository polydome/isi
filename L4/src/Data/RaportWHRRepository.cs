using System.Collections.Generic;
using System.Data;
using L4.Domain;
using MySql.Data.MySqlClient;

namespace L4.Data
{
    public class RaportWHRRepository
    {
        private const string SelectAllWhrRaports =
            "SELECT CountryName, LadderScore, Freedom, LoggedGDPPerCapita FROM whr.raport WHERE LadderScore >= ?minLadderScore AND Freedom >= ?minFreedom AND LoggedGDPPerCapita >= ?minGDPPerCapita";

        private readonly Database _database;

        public RaportWHRRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<RaportWHR> FindAll(ThresholdSettings thresholdSettings)
        {
            return _database.RetrieveData(SelectAllWhrRaports, ParseWhrRecord, new []
            {
                new MySqlParameter("minLadderScore", thresholdSettings.LadderScore),
                new MySqlParameter("minGDPPerCapita", thresholdSettings.GdpPerCapita),
                new MySqlParameter("minFreedom", thresholdSettings.FreedomOfChoice),
            });
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