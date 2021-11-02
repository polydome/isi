using System.Collections.Generic;
using System.Linq;
using System.Text;
using L4.Domain;

namespace L4.Data
{
    public class CustomWorldRepository
    {
        private readonly Database _database;

        public CustomWorldRepository(Database database)
        {
            _database = database;
        }

        public void replaceAll(IEnumerable<DataCustomWorld> newRecords)
        {
            StringBuilder commandBuilder = new();
            commandBuilder.Append("DELETE FROM custom_world.countries; ");
            commandBuilder.Append("INSERT INTO custom_world.countries VALUES");

            List<string> rows = newRecords.Select(record =>
                string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    record.Code,
                    record.Name,
                    record.SurfaceArea,
                    record.Population,
                    record.FreedomOfChoices.ToString().Replace(",", "."),
                    record.GDPperCapita.ToString().Replace(",", "."),
                    record.LadderScore.ToString().Replace(",", "."))
            ).ToList();

            commandBuilder.Append(string.Join(",", rows));
            commandBuilder.Append(';');

            string query = commandBuilder.ToString();

            _database.Execute(query);
        }
    }
}