using Microsoft.EntityFrameworkCore;
using SolarPowerMonitoringApi.Data.Models;

namespace SolarMonitoringAPI.Data
{
    public class SolarMonitoringDbContext : DbContext
    {
        public SolarMonitoringDbContext(DbContextOptions<SolarMonitoringDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SolarPowerPlant> SolarPowerPlants { get; set; }
        public DbSet<ProductionRecord> ProductionRecords { get; set; }
    }
}