namespace SolarPowerMonitoringApi.Data.DTOs
{
    public class ProductionRecordDto
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
        public bool IsForecast { get; set; }
    }
}