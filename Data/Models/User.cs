using System.ComponentModel.DataAnnotations;

namespace SolarPowerMonitoringApi.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }
    }

}
