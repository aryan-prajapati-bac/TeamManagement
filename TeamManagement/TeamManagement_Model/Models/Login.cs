using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeamManagement.Models
{
    public class Login
    {
        [Required]
        [Key]
        [ForeignKey(nameof(User.Email))]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
