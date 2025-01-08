namespace SolarPowerMonitoringApi.Data.Models
{
    public class ProductionRecord
    {
        public int Id { get; set; }
        public int SolarPowerPlantId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; } // pwer generated or forecasted at this time
        public bool IsForecast { get; set; } // forecast vs real data
        public virtual SolarPowerPlant SolarPowerPlant { get; set; }
    }

}
