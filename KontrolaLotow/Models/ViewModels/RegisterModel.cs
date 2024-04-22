using System.ComponentModel.DataAnnotations;

namespace KontrolaLotow.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "Nazwa użytkownika musi mieć od 4 do 25 znaków.")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Nazwa użytkownika może zawierać tylko litery i cyfry.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Login jest wymagany.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Login musi składać się z przynajmniej 6 znaków.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Hasło musi mieć co najmniej 6 znaków.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potwierdzenie hasła jest wymagane.")]
        [Compare("Password", ErrorMessage = "Wpisano dwa różne hasła.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
