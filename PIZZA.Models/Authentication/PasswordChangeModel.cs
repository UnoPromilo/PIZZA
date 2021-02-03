using System.ComponentModel.DataAnnotations;

namespace PIZZA.Models.Authentication
{
    public class PasswordChangeModel
    {
        public int ID { get; init; }

        public bool ForcePasswordChangeWhileNextLogin { get; set; } = true;

        [Required(ErrorMessage = "Pole hasło jest wymagane.")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć przynajmniej 8 znaków i nie może być dłuższe niż 100 znaków.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }
    }
}
