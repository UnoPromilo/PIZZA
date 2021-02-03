using System.ComponentModel.DataAnnotations;

namespace PIZZA.Models.Authentication
{
    public class RegistrationModel
    {
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email musi mieć przynajmniej 5 znaków i nie może być dłuższy niż 100 znaków.", MinimumLength = 5)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole nazwa użytkownika jest wymagane.")]
        [StringLength(100, ErrorMessage = "Nazwa użytkownika musi mieć przynajmniej 5 znaków i nie może być dłuższa niż 100 znaków.", MinimumLength = 5)]
        [Display(Name = "nazwa użytkownika")]
        public string UserName { get; set; }

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
