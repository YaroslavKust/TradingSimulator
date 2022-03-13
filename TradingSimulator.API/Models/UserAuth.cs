using System.ComponentModel.DataAnnotations;

namespace TradingSimulator.API.Models
{
    public class UserAuth
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}
