using System.ComponentModel.DataAnnotations;

namespace RoRService.Models.ViewModels.AccountsViewModels
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Player")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
    }
}