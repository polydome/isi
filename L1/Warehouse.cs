namespace L1
{
    public class Warehouse
    {
        public Warehouse(string voivodeship, string city, string status)
        {
            Voivodeship = voivodeship;
            City = city;
            Status = status;
        }

        public string Voivodeship { get; }

        public string City { get; }

        public string Status { get; }
    }
}