namespace SolarPowerMonitoringApi.Data.DTOs
{
    public class SolarPowerPlantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double InstalledPower { get; set; }
        public DateTime DateOfInstallation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ICollection<ProductionRecordDto> ProductionRecords { get; set; }
    }
}