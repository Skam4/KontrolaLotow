using System.ComponentModel.DataAnnotations;

namespace KontrolaLotow.Models
{
    public class Flight
    {
        [Key]
        public int IdLotu { get; set; }

        [Required(ErrorMessage = "Numer lotu jest wymagany.")]
        public int NumerLotu { get; set; }

        [Required(ErrorMessage = "Data wylotu jest wymagana.")]
        public DateTime DataWylotu { get; set; }

        [Required(ErrorMessage = "Miejsce wylotu jest wymagane.")]
        public string MiejsceWylotu { get; set; }

        [Required(ErrorMessage = "Miejsce przylotu jest wymagane.")]
        public string MiejscePrzylotu { get; set; }

        [Required(ErrorMessage = "Typ samolotu jest wymagany.")]
        public string TypSamolotu { get; set; }
    }
}
