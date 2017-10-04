using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArchitectureTemplate.Mvc.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = @"Field Required")]
        [DisplayName(@"User")]
        public string Login { get; set; }

        [Required(ErrorMessage = @"Field Required")]
        [DisplayName(@"Passward")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName(@"Remember-Me")]
        public bool RememberMe { get; set; }

        [Required(ErrorMessage = @"Field Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = @"Your must be valid")]
        [DisplayName(@"Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = @"Field Required")]
        [DisplayName(@"Code Recover")]
        public string CodigoRecover { get; set; }

        [Required(ErrorMessage = @"Field Required")]
        [StringLength(100, ErrorMessage = @"Field must be at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = @"New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = @"Field Required")]
        [DataType(DataType.Password)]
        [Display(Name = @"Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = @"New password and confirmation does not match.")]
        public string ConfirmPassword { get; set; }
    }
}