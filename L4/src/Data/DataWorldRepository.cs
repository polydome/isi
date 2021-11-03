using System.Collections.Generic;
using System.Data;
using L4.Domain;
using MySql.Data.MySqlClient;

namespace L4.Data
{
    public class DataWorldRepository
    {
        private const string SelectAllWorldData = "SELECT * FROM world.country WHERE SurfaceArea >= ?minSurfaceArea AND Population >= ?minPopulation";
        private readonly Database _database;

        public DataWorldRepository(Database database)
        {
            _database = database;
        }

        public IEnumerable<DataWorld> FindAll(ThresholdSettings thresholdSettings)
        {
            return _database.RetrieveData(SelectAllWorldData, ParseDataWorldRecord, new []
            {
                new MySqlParameter("minSurfaceArea", thresholdSettings.SurfaceArea),
                new MySqlParameter("minPopulation", thresholdSettings.Population),
            });
        }

        private static DataWorld ParseDataWorldRecord(IDataRecord record)
        {
            return new DataWorld
            {
                Code = record.GetString(0),
                Name = record.GetString(1),
                SurfaceArea =
                    float.Parse(record.GetString(4)),
                Population = int.Parse(record.GetString(6))
            };
        }
    }
}