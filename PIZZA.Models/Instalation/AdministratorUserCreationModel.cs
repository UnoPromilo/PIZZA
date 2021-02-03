using System.ComponentModel.DataAnnotations;

namespace PIZZA.Models.Instalation
{
    public class AdministratorUserCreationModel
    {
        [Required]
        public string Username { get; set; } = "Administrator";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Hasło musi mieć przynajmniej 8 znaków.")]
        public string NewPassowrd { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassowrd", ErrorMessage = "Hasła nie pasują do siebie.")]

        public string RepeatPassword { get; set; }
    }
}
