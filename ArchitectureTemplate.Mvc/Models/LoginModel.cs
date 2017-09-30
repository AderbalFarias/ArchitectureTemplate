using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArchitectureTemplate.Mvc.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = @"Field Required")]
        [DisplayName(@"Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = @"Field Required")]
        [DisplayName(@"Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName(@"Lembrar")]
        public bool RememberMe { get; set; }

        [Required(ErrorMessage = @"Field Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = @"Preencha com um e-mail válido")]
        [DisplayName(@"E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = @"Field Required")]
        [DisplayName(@"Código Recuperação")]
        public string CodigoRecover { get; set; }

        [Required(ErrorMessage = @"Field Required")]
        [StringLength(100, ErrorMessage = @"O campo deve ter no mínimo {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = @"Nova Senha")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = @"Field Required")]
        [DataType(DataType.Password)]
        [Display(Name = @"Confirme a nova senha")]
        [Compare("NewPassword", ErrorMessage = @"A nova senha e a confimirmação não conferem.")]
        public string ConfirmPassword { get; set; }
    }
}