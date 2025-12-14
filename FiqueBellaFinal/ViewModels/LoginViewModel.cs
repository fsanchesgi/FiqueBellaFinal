using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FiqueBellaFinal.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o login")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = string.Empty;
    }
}
