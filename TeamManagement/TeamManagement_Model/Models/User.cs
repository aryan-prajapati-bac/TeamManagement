using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace TeamManagement.Models
{
    public class User
    {
      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[6-9][0-9]{9}$", ErrorMessage = "Invalid phone number format")]
        public string ContactNumber { get; set; }

        [Key]
        [EmailAddress]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }

        [DefaultValue(0)]
        [JsonIgnore]
        public int RoleId { get; set; }

        [DefaultValue(0)]
        [JsonIgnore]
        public int Count { get; set; }
        [DefaultValue("team1234")]
        public string Password {  get; set; }



       // public string secondarymail { get; set; }
    }
}
