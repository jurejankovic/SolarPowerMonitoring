using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SolarPowerMonitoringApi.Data.Models
{

    public class SolarPowerPlant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public double InstalledPower { get; set; }
        [Required]
        public DateTime DateOfInstallation { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public virtual ICollection<ProductionRecord> ProductionRecords { get; set; }
    }

}
