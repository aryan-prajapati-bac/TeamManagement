using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamManagement.Models
{
    public class Login
    {
        #region Properties
        [Required]
        [Key]
        [ForeignKey(nameof(User.Email))]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        #endregion
    }
}
