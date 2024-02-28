using System.ComponentModel.DataAnnotations;

namespace BookMvc.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Nazwa użytkownika jest obowiązkowa")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Hasło jest obowiązkowe")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name ="Zapamietaj")]
        public bool RememberMe { get; set; }
    }
}
