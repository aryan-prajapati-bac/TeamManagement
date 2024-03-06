using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TeamManagement.Models
{
    public class User
    {
        #region Properties

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

        
        [JsonIgnore]        
        public string? Password {  get; set; }

        #endregion

    }
}
