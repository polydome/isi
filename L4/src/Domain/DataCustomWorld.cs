namespace L4.Domain
{
    public class DataCustomWorld
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public float SurfaceArea { get; set; }
        public int Population { get; set; }
        public float FreedomOfChoices { get; set; }
        public float GDPperCapita { get; set; }
        public float LadderScore { get; set; }

        public static DataCustomWorld FromCountryData(DataWorld country)
        {
            return new()
            {
                Code = country.Code,
                Name = country.Name,
                Population = country.Population,
                SurfaceArea = country.SurfaceArea
            };
        }

        public void FillWhrData(RaportWHR whr)
        {
            LadderScore = whr.LadderScore;
            FreedomOfChoices = whr.FreedomOfChoices;
            GDPperCapita = whr.GDPperCapita;
        }
    }
}