using System.ComponentModel.DataAnnotations;

namespace KontrolaLotow.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Login jest wymagany.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Niepoprawny login.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Hasło niepoprawne.")]
        public string Password { get; set; }

        public virtual Role UserRole { get; set; }
    }
}
