namespace SolarPowerMonitoringApi.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using SolarMonitoringAPI.Data;
    using SolarPowerMonitoringApi.Data.Models;

    public static class SeedData
    {
        public static async Task InitializeAsync(SolarMonitoringDbContext context)
        {
            // skip seeding if data exists
            if (context.SolarPowerPlants.Any())
            {
                return;
            }

            var random = new Random();

            // 10 new solar power plants with random data
            for (int i = 1; i <= 10; i++)
            {
                var plant = new SolarPowerPlant
                {
                    Name = $"Solar Plant {i}",
                    InstalledPower = random.Next(1000, 5000),
                    DateOfInstallation = DateTime.UtcNow.AddDays(-random.Next(100, 1000)), // random installation date
                    Latitude = Math.Round(random.NextDouble() * 90, 6),
                    Longitude = Math.Round(random.NextDouble() * 180, 6)
                };

                // random production records for the last 60 days
                plant.ProductionRecords = Enumerable.Range(0, 60).Select(offset =>
                {
                    // Start from today going back offset days
                    var recordDate = DateTime.Now.Date.AddDays(-offset);

                    // random daily production in kWh
                    return new ProductionRecord
                    {
                        IsForecast = RandomBool(),
                        SolarPowerPlant = plant,
                        SolarPowerPlantId = plant.Id,
                        Timestamp = recordDate,
                        Value = random.NextDouble() * plant.InstalledPower,
                    };
                }).ToList();

                // add the plant with its production records
                context.SolarPowerPlants.Add(plant);
            }

            // save changes to database
            await context.SaveChangesAsync();
        }

        private static bool RandomBool()
        {
            var random = new Random();
            return random.NextDouble() >= 0.5;
        }
    }
}
